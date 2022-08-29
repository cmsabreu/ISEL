USE L51DG5--uncomment when using isel DB

-- O tipo do activo de topo da hierarquia tem de ser igual ao(s) tipo(s) do(s) activo(s) “filho(s)” 
GO
IF OBJECT_ID(N'dbo.trg_checkAssetType', N'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_checkAssetType;
GO
CREATE TRIGGER trg_checkAssetType
ON dbo.ASSET 
INSTEAD OF INSERT
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION

	--if paretnt != null -> get type of parent -> if type of son is different throw exception 
	DECLARE @parent_id INT, @son_type INT, @parent_type INT

	SELECT @parent_id = asset_reference, @son_type = type FROM INSERTED

	IF (@parent_id IS NOT NULL)
	BEGIN
    	SELECT  @parent_type = asset.type FROM ASSET asset WHERE id = @parent_id
    	
    	IF @parent_type != @son_type
    		THROW 51000, 'Asset type must be the same type as the parent asset', 1
    	
	END

    INSERT INTO ASSET(asset_name, acquisition_date, state, brand, model, location, asset_reference, manager, type) 
    SELECT  asset_name, acquisition_date, state, brand, model, location, asset_reference, manager, type
    FROM INSERTED
COMMIT


/*ALINEA (d)
 Mecanismo que permite Inserir, remover e atualizar informação de 
 uma pessoa. Não podem ser usados insert, update e delete directos
*/

-- inserir--------------------------------------------------------------------------------------
GO
IF object_id('dbo.insertEmployee','P') IS NOT NULL
    DROP PROCEDURE dbo.insertEmployee 
GO
CREATE PROC insertEmployee (@ssn int,  @f_name varchar(50), @l_name varchar(50), @birth_date DATETIME,
		@address VARCHAR(50),	@postal_code VARCHAR(50), @city VARCHAR(50),
		@job VARCHAR(50),	@phone_number INT,	@mail VARCHAR(100))
AS
BEGIN
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED
  BEGIN TRANSACTION

    IF NOT EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @ssn)
    BEGIN
      INSERT INTO EMPLOYEE(ssn, f_name, l_name, birth_date, address, postal_code, city, job, phone_number, mail) 
	  VALUES(@ssn,  @f_name, @l_name, @birth_date, @address, @postal_code, @city, @job,	@phone_number,	@mail)
    END
   ELSE
		THROW 51000, 'Error! Employee can not be created!!', 1

  COMMIT
END;
-- d)remover--------------------------------------------------------------------------------------

GO
IF object_id('dbo.removeEmployee','P') IS NOT NULL
    DROP PROCEDURE dbo.removeEmployee 
GO

CREATE PROC removeEmployee (@ssn INT)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ --evitar lost updates
	BEGIN TRANSACTION

    -- check employee on MAINTENANCE_TEAM
    IF EXISTS (SELECT supervisor FROM MAINTENANCE_TEAM WHERE supervisor = @ssn )
        THROW 51000, 'Error! Employee can not be deleted!!', 1

    -- check employee on ASSET
    IF EXISTS (SELECT manager FROM ASSET WHERE manager = @ssn )
        THROW 51000, 'Error! Employee can not be deleted!!', 1

    -- check employee on TEAM_MEMBER
    IF EXISTS (SELECT id FROM TEAM_MEMBER WHERE id = @ssn )
        THROW 51000, 'Error! Employee can not be deleted!!', 1

    -- delete from EMPLOYEE_SKILL
    IF EXISTS (SELECT employee_code FROM EMPLOYEE_SKILL WHERE employee_code = @ssn )
        DELETE FROM EMPLOYEE_SKILL WHERE employee_code = @ssn

	-- delete from EMPLOYEE
    IF EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @ssn )
        DELETE FROM EMPLOYEE WHERE ssn = @ssn
  
    COMMIT
END

--d) atualizar --------------------------------------------------------------------------------------

GO
IF object_id('dbo.updateEmployee','P') IS NOT NULL
    DROP PROCEDURE dbo.updateEmployee 
GO

CREATE PROC updateEmployee (@ssn int,  @f_name varchar(50), @l_name varchar(50), @birth_date DATETIME,
		@address VARCHAR(50),	@postal_code VARCHAR(50), @city VARCHAR(50),
		@job VARCHAR(50),	@phone_number INT,	@mail VARCHAR(100))
AS
BEGIN
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED
  BEGIN TRANSACTION

    IF EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @ssn)
    BEGIN
      UPDATE EMPLOYEE
      SET f_name = @f_name, l_name = @l_name, birth_date = @birth_date,
						address = @address, postal_code = @postal_code, 
						city = @city, job = @job, phone_number = @phone_number,mail = @mail
      WHERE ssn = @ssn;
    END;

  COMMIT
END;

/*
ALINEA (e) 
Obter o codigo de uma equipa livre, dada uma descrição de intervenção, capaz de resolver o problema.
 Em caso de haver várias equipas deve escolher-se a que teve uma intervenção atribuida à mais tempo
*/--------------------------------------------------------------------------------------

GO
IF object_id('dbo.get_code_from_team') IS NOT NULL
    DROP FUNCTION dbo.get_code_from_team 
GO
-- esta funcao retorna NULL caso nao encontre uma equipa
CREATE FUNCTION dbo.get_code_from_team(@description VARCHAR(50))
RETURNS @toReturn TABLE(
						team_code INT,
						location VARCHAR(50),
						n_elements INT,
						supervisor INT
						)
AS
BEGIN
	DECLARE @ret INT

	DECLARE @members TABLE (ssn INT);
	-- find members suitable for intervention
	INSERT INTO @members (ssn) SELECT es.employee_code
	FROM EMPLOYEE_SKILL es JOIN SKILL s ON es.skill_code = s.id
	WHERE description = @description

	-- encontrar uma equipa com os membros anteriormente calculados
	DECLARE @team_code TABLE (team_code INT);
	INSERT INTO @team_code (team_code)
	SELECT DISTINCT team_code FROM TEAM_MEMBER tm JOIN @members m ON tm.id = m.ssn

	DECLARE @count INT
	SELECT @count = COUNT(team_code) FROM @team_code

	IF (@count = 0)
		SET @ret = NULL
	IF (@count = 1)
		SELECT @ret = team_code FROM @team_code
	ELSE
	BEGIN
		SELECT TOP 1 @ret = t.team_code
		FROM @team_code t
		LEFT JOIN SCHEDULING s ON t.team_code = s.team_code
		ORDER BY s.scheduling_date ASC
	END
	INSERT INTO @toReturn
    SELECT team_code, location, n_elements, supervisor
    FROM MAINTENANCE_TEAM
	WHERE team_code = @ret

	RETURN
END

/*
ALINEA (f) Criar o procedimento p criaInter que permite criar uma intervenção
*/--------------------------------------------------------------------------------------
GO

IF object_id('dbo.p_criaInter','P') IS NOT NULL
    DROP PROCEDURE dbo.p_criaInter 
GO

CREATE PROC p_criaInter (@description VARCHAR(50), @price DECIMAL(10,2), @startDate DATETIME, @endDate AS DATETIME = NULL, @frequency INT, @asset_id INT, @skillDescription VARCHAR(50))
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRANSACTION

	-- A data de intervenção deve ser superior à data de aquisicao do ativo intervencionado
	DECLARE @asset_date DATETIME
	SELECT @asset_date = acquisition_date FROM ASSET WHERE id = @asset_id

	IF (@startDate < @asset_date)
		THROW 51000, 'Error! Cannot create intervention', 1

	DECLARE @freeTeam INT
	SELECT @freeTeam = team_code FROM dbo.get_code_from_team (@skillDescription)

	DECLARE @intervention_code INT

	IF (@freeTeam is NULL)
		INSERT INTO INTERVENTION(description, state, price, start_date, end_date, asset_id) VALUES
			(@description, 'por atribuir', @price, @startDate, @endDate, @asset_id)
	ELSE
	BEGIN
		INSERT INTO INTERVENTION(description, state, price, start_date, end_date, asset_id) VALUES
			(@description, 'em analise', @price, @startDate, @endDate, @asset_id)

			SELECT @intervention_code = SCOPE_IDENTITY()

		INSERT INTO SCHEDULING (team_code, intervention_code, scheduling_date) VALUES
			(@freeTeam, @intervention_code, @startDate)
	END

	IF(@frequency = NULL)
		INSERT INTO PERIODIC(id, frequency) VALUES(@intervention_code, @frequency)
	ELSE
		INSERT INTO NON_PERIODIC(id) VALUES(@intervention_code)

    COMMIT
END

/*
(g) implementar o mecanismo que permite criar uma equipa;
*/--------------------------------------------------------------------------------------
GO
  IF object_id('dbo.p_create_team','P') IS NOT NULL
      DROP PROCEDURE dbo.p_create_team 
GO
  DROP TYPE IF EXISTS dbo.TeamMembersSsn
GO
  CREATE TYPE TeamMembersSsn AS TABLE
  (
      idx INT PRIMARY KEY,
      ssn_team_member INT UNIQUE
  )
GO
CREATE PROC p_create_team (@location VARCHAR(100), @ssn_supervisor INT, @team_members_ssn TeamMembersSsn READONLY)
AS
BEGIN
  SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
  BEGIN TRANSACTION
    --check if ssn supervisor is a valid ssn of an employee
    IF NOT EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @ssn_supervisor)	
        THROW 51000, 'Error! SSN of the supervisor not valid, incorrent or invalid!!', 1

    DECLARE @n_of_team_members int
    SELECT @n_of_team_members = max(idx) FROM @team_members_ssn
    --number of team member >= 2? if not throw exception
    IF (@n_of_team_members < 2)	
		THROW 51000, 'A team must have at least 2 elements', 1

	-- criar uma nova equipa
    INSERT INTO MAINTENANCE_TEAM(location,n_elements,supervisor) VALUES( @location, @n_of_team_members, @ssn_supervisor)

	DECLARE @team_code int
	SELECT @team_code = SCOPE_IDENTITY()

	-- inserir os elementos da equipa

    DECLARE @i INT = 1
    DECLARE @curr_ssn_team_member INT

    WHILE @i <= @n_of_team_members
      BEGIN
	    SELECT @curr_ssn_team_member = t.ssn_team_member FROM @team_members_ssn t WHERE t.idx = @i
		-- Uma pessoa so pode pertencer a uma equipe.
		IF EXISTS (SELECT id FROM TEAM_MEMBER WHERE id = @curr_ssn_team_member)
			THROW 51000, 'Invalid SSN, that ssn is already on a team', 1
        IF NOT EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @curr_ssn_team_member)	
	        THROW 51000, 'Invalid SSN, that ssn doesnt belong to Employees table', 1

        INSERT INTO TEAM_MEMBER(id,team_code)VALUES(@curr_ssn_team_member, @team_code)
	    SET @i = @i + 1
      END
  
    COMMIT
END

/*
(h)Actualizar (adicionar ou remover) os elementos de uma equipe e associar as respectivas competencias;
*/--------------------------------------------------------------------------------------
GO
IF object_id('dbo.update_team_members','P') IS NOT NULL
  DROP PROCEDURE dbo.update_team_members 
GO
  DROP TYPE IF EXISTS dbo.EmployeesToUpdate
GO
CREATE TYPE EmployeesToUpdate AS TABLE
(
	idx INT PRIMARY KEY,
	id INT
)
GO
CREATE PROC update_team_members (@team_code INT,  @toDelete BIT, @team_members_ssn EmployeesToUpdate READONLY)
AS
BEGIN
   	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ 
    BEGIN TRANSACTION
        IF NOT EXISTS (SELECT team_code FROM MAINTENANCE_TEAM WHERE team_code = @team_code)
        	THROW 51000, 'Team doesnt exists!!', 1

        DECLARE @length_of_table INT, @curr_ssn INT, @n_elements_team INT
        DECLARE @i INT = 1
        SELECT @length_of_table = max(idx) FROM @team_members_ssn

        SELECT @n_elements_team = n_elements FROM MAINTENANCE_TEAM WHERE team_code = @team_code
        --se e para apagar temos de garantir que não vai ficar com menos de 2 elementos
        IF @toDelete = 1 AND ((@n_elements_team - @length_of_table) < 2)
			THROW 51000, 'team must have at least 2 elements', 1
            
        WHILE @i <= @length_of_table
        BEGIN
            SELECT @curr_ssn = id FROM @team_members_ssn WHERE idx = @i
            
            IF NOT EXISTS (SELECT ssn FROM EMPLOYEE WHERE ssn = @curr_ssn)	
                THROW 51000, 'Invalid SSN, that ssn doenst belong to Employees table', 1
        
		    IF @toDelete = 1
            BEGIN
                IF NOT EXISTS (SELECT id FROM TEAM_MEMBER WHERE id = @curr_ssn AND team_code = @team_code)	
					THROW 51000, 'Cannot delete that employee from team', 1

                DELETE FROM TEAM_MEMBER WHERE id = @curr_ssn AND team_code = @team_code
                SET @n_elements_team = @n_elements_team - 1
            END
            ELSE
            BEGIN
                INSERT INTO TEAM_MEMBER(id, team_code) VALUES(@curr_ssn,@team_code)
                SET @n_elements_team = @n_elements_team + 1
            END
            SET @i = @i + 1
        END
        
        UPDATE MAINTENANCE_TEAM SET n_elements = @n_elements_team WHERE team_code = @team_code
    
    COMMIT
END
/*(i) Criar uma funcao para produzir a listagem (codigo, descricao) das intervencoes de um determinado ano;
*/--------------------------------------------------------------------------------------

GO
IF object_id('dbo.interByYear') IS NOT NULL
    DROP FUNCTION dbo.interByYear
GO
CREATE FUNCTION dbo.interByYear(@year INT)
RETURNS @toReturn TABLE(
						intervention_code INT,
						description VARCHAR(50),
						state VARCHAR(50),
						price DECIMAL(10,2),
						start_date DATETIME,
						end_date DATETIME,
						asset_id INT

						)
AS
BEGIN
    INSERT INTO @toReturn
    SELECT intervention_code, description, state, price, start_date, end_date, asset_id
    FROM INTERVENTION 
    WHERE YEAR(start_date) = @year
	RETURN
END


/*(j) Actualizar o estado de uma intervencao;
*/--------------------------------------------------------------------------------------	
GO
IF object_id('dbo.updateStateIntervention','P') IS NOT NULL
    DROP PROCEDURE dbo.updateStateIntervention 
GO
CREATE PROC updateStateIntervention (@intervention_code INT,@state VARCHAR(50), @endDate DATETIME)
AS
BEGIN
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED
  BEGIN TRANSACTION
    --check if intervention exits
    IF NOT EXISTS (SELECT intervention_code FROM INTERVENTION WHERE intervention_code = @intervention_code)	
    	THROW 51000, 'Invalid intervention code, that intervention code doenst exists', 1

	UPDATE INTERVENTION SET state = @state
	WHERE intervention_code = @intervention_code;
	IF(@state = 'concluido')
	BEGIN
		UPDATE INTERVENTION SET end_date = @endDate
		WHERE intervention_code = @intervention_code;
	END
  COMMIT
END

/*(k) Criar uma vista que mostre o resumo das intervencoes (atributos de intervenca e ativo), que possibilite a alteracao 
	do estado de uma ou mais intervencoes;
*/--------------------------------------------------------------------------------------
GO
IF object_id('dbo.vw_summary_intervention','V') IS NOT NULL
	DROP VIEW dbo.vw_summary_intervention 
GO

CREATE VIEW dbo.vw_summary_intervention
AS 
SELECT i.intervention_code, i.description AS intervention_description, i.state AS intervention_state, 
		i.price AS intervention_price,i.start_date AS intervention_start_date, i.end_date,i.asset_id,
		a.brand AS asset_brand, a.acquisition_date AS asset_acquisition_date, 
		a.asset_name, a.asset_reference, a.location AS asset_location, a.manager AS asset_manager, 
		a.model AS asset_model, a.state AS asset_state, a.type AS asset_type
FROM INTERVENTION i
JOIN ASSET a ON i.asset_id = a.id



GO
IF OBJECT_ID(N'dbo.trg_updateInterState', N'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_updateInterState;
GO

CREATE TRIGGER trg_updateInterState
ON dbo.vw_summary_intervention
INSTEAD OF UPDATE
AS
BEGIN TRY
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION
		DECLARE @state VARCHAR(50)
		DECLARE @intervention INT
		SELECT @state = intervention_state, @intervention = intervention_code FROM INSERTED

		IF UPDATE (intervention_state)
			EXEC dbo.updateStateIntervention @intervention ,@state, NULL

	    COMMIT
	END TRY
	BEGIN CATCH
		SELECT ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;
		ROLLBACK
END CATCH
