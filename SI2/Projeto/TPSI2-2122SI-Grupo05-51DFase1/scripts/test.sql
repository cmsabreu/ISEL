-- USE L51DG5--uncomment when using isel DB
/*
Testar trigger -> trg_checkAssetType
*/
-------------------------------------------------------------------------------------------------------------------
BEGIN TRY
BEGIN TRANSACTION
	--asset-reference = null
	DECLARE @beforeCount INT, @afterCount INT
	SELECT @beforeCount = max(id) FROM ASSET
	SET @beforeCount = @beforeCount + 1
	
	INSERT INTO ASSET (asset_name, acquisition_date, state, brand, model, location, asset_reference, manager, type) VALUES
		('asset name', '2021-05-12', 2, 'brand', 'model', 'location', NULL,123432987, 1)
		
	SELECT @afterCount = max(id) FROM ASSET
	
	IF(@beforeCount = @afterCount)	
		print 'Teste Trigger Check Asset Type : OK'
	ELSE
		print 'Teste Trigger Check Asset Type: NOK'
		
	BEGIN TRY
		--must throw an exception -> parent type != type 
		INSERT INTO ASSET (asset_name, acquisition_date, state, brand, model, location, asset_reference, manager, type)
		VALUES('teste2', '2020-01-12', 1, 'test2', 'teste', '-', 3,123432987, 1)
	END TRY  
	BEGIN CATCH 
		print 'Teste Trigger Check Asset Type: Exception OK'
	END CATCH
	ROLLBACK
END TRY  
BEGIN CATCH 
    SELECT 'Entrei no catch'
END CATCH

/*
Testar as funcionalidades de 2d a 2k, com indicação dos testes que falham
*/
-----------------------------------------------------------------------------------------------------
--ALINEA D - Testar insert
BEGIN TRY
BEGIN TRANSACTION
	EXEC dbo.insertEmployee 123456789,'Andre', 'Zilmar', '2000-03-25','Rua Teste', '9999-999', 'Lisboa', 'test', 913652336, 'teste@mail.com';
	IF NOT EXISTS(SELECT ssn FROM EMPLOYEE WHERE ssn = 765123952)
		PRINT 'Test D.1: Insert Employee: OK'
	ELSE
		PRINT 'Test D.1: Insert Employee: NOK'
	IF EXISTS(SELECT ssn FROM EMPLOYEE WHERE ssn = 123456789)
		PRINT 'Test D.1: Insert Employee: OK'
	ELSE
		PRINT 'Test D.1: Insert Employee: NOK'
	ROLLBACK
END TRY
BEGIN CATCH
	RAISERROR('Entrei no catch test D.1',16,1)
	ROLLBACK
END CATCH

-----------------------------------------------------------------------------------------------------
--ALINEA D - Testar remove
BEGIN TRY
BEGIN TRANSACTION

	INSERT INTO EMPLOYEE(ssn, f_name, l_name, birth_date, address, postal_code, city, job, phone_number, mail) VALUES
	(765123952, 'Maria', 'Joao', '1987-08-16', 'Rua da Bica', '1900-001', 'Lisboa', 'engeheiro', 987654321, 'mariajoao@gmail.com');

	EXEC dbo.removeEmployee 765123952
	IF NOT EXISTS(SELECT ssn FROM EMPLOYEE WHERE ssn = 765123952)
		print 'Teste D.2: Remover funcionário: OK'
	ELSE
		print 'Teste D.2: Remover funcionário: NOK'
	BEGIN TRY
		-- mostra que nao deixa remover um employee que esteja noutra tabela (ex: asset, ...)
		EXEC dbo.removeEmployee 123432987
		print 'Teste D.2: Remover funcionário com dependencia: NOK'
	END TRY  
	BEGIN CATCH 
		print 'Teste D.2: Remover funcionário com dependencia: OK'
	END CATCH
	ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test D.2',16,1)
	ROLLBACK
END CATCH


-----------------------------------------------------------------------------------------------------
-- ALINEA D - Testar update do EMPLOYEE

BEGIN TRY
BEGIN TRANSACTION

	EXEC dbo.updateEmployee 987654321, 'Teste', 'Teste', '1989-03-25','Rua Teste', '9999-999', 'Teste City', 'plumber',925765123, 'teste@gmail.com';
	IF EXISTS(SELECT ssn FROM EMPLOYEE WHERE f_name = 'Teste' AND l_name = 'Teste' AND ssn = 987654321)
		print 'Teste D.3: Alterar Employee: OK'
	ELSE
		print 'Teste D.3: Alterar Employee: NOK'
	ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test D.3',16,1)
	ROLLBACK
END CATCH

-----------------------------------------------------------------------------------------------------
--ALINEA E
DECLARE @team INT

EXEC @team = dbo.get_code_from_team 'c#'
IF (@team = 1)
	print 'Teste E.2: OK'
ELSE
	print 'Teste E.2: NOK'

EXEC @team = dbo.get_code_from_team 'kotlin'
IF (@team is NULL)
	print 'Teste E.3: OK'
ELSE
	print 'Teste E.3: NOK'

-----------------------------------------------------------------------------------------------------
-- ALINEA F 
BEGIN TRY
BEGIN TRANSACTION

	EXEC dbo.p_criaInter 'avaria', 10.2, '2021-11-11', NULL,NULL, 2, 'java';
	IF EXISTS(SELECT intervention_code FROM INTERVENTION WHERE price = 10.2 AND start_date = '2021-11-11' AND asset_id = 2)
		print 'Teste F: Criar Intervention: OK'
	ELSE
		print 'Teste F: Criar Intervention: NOK'
	ROLLBACK

	-- Teste que confirma que a data de intervenção deve ser superior à data de aquisicao do ativo intervencionado
	BEGIN TRY
		EXEC dbo.p_criaInter 'avaria', 10.2, '2021-07-20', NULL,NULL, 2, 'java';
		print 'Teste F: Criar Intervention com data de intervençao errada: NOK'
	END TRY  
	BEGIN CATCH 
		print 'Teste F: Criar Intervention com data de intervençao errada: OK'
	END CATCH
	ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test F',16,1)
	ROLLBACK
END CATCH

-----------------------------------------------------------------------------------------------------
--ALINEA G testar o mecanismo que permite criar uma equipa;

BEGIN TRY
	BEGIN TRANSACTION
	DECLARE @team_members_ssn TeamMembersSsn
	INSERT INTO @team_members_ssn VALUES (123432987)
	INSERT INTO @team_members_ssn VALUES (987654321)
	INSERT INTO @team_members_ssn VALUES (345654321)


	EXEC dbo.p_create_team "Londres", 123432987, @team_members_ssn

	IF EXISTS(SELECT m.n_elements FROM MAINTENANCE_TEAM m WHERE m.location = 'Londres' AND m.supervisor = 123432987)
		print 'Teste G: Criar Maintenance Team: OK'
	ELSE
		print 'Teste G: Criar Maintenance Team: NOK'

	ROLLBACK
	
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test G',16,1)
	ROLLBACK
END CATCH
-----------------------------------------------------------------------------------------------------
--ALINEA H
BEGIN TRY
	BEGIN TRANSACTION
	
		DECLARE @testDelete EmployeesToUpdate
		
        INSERT INTO @testDelete VALUES (901774115)
		INSERT INTO @testDelete VALUES (411424231)
		
		DECLARE @porto_team AS INT = 1
        DECLARE @beforeDelete INT, @afterDelete INT
        SELECT @beforeDelete = COUNT(id) FROM TEAM_MEMBER WHERE team_code = @porto_team
        
        EXEC dbo.update_team_members @porto_team, 1, @testDelete 		--flag 1 means that is to delete
        SELECT @afterDelete = COUNT(id) FROM TEAM_MEMBER WHERE team_code = @porto_team

        --apagar dois empregados da equipa do porto
        IF @beforeDelete > @afterDelete
			print 'Teste H: Delete Team: OK'
		ELSE
			print 'Teste H: Delete Team: NOK'		
    
        --adicionar dois empregados a equipa de aveiro
		DECLARE @testUpdate EmployeesToUpdate
        INSERT INTO @testUpdate VALUES (901774115)
		INSERT INTO @testUpdate VALUES (411424231)
		
		DECLARE @aveiro_team AS INT = 3
		
		DECLARE @beforeUpdate INT, @afterUpdate INT
        SELECT @beforeUpdate = COUNT(id) FROM TEAM_MEMBER WHERE team_code = @aveiro_team

		EXEC dbo.update_team_members @aveiro_team, 0, @testUpdate -- flag 0 is for update
		
		 SELECT @afterUpdate = COUNT(id) FROM TEAM_MEMBER WHERE team_code = @aveiro_team
        --apagar dois empregados da equipa do porto

        IF @beforeUpdate + 2  = @afterUpdate         --check if beforeUpodate +2 = after count
			print 'Teste H: Update Team: OK'
		ELSE
			print 'Teste H: Update Team: NOK'
			
	
        --passar uma equipa que não existe EXCEPTION TODO:
        --tentar apagar mais do que é possivel EXCEPTION TODO:
        --acrescentar com um ssn que não existe EXCEPTION TODO:
		ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test H',16,1)
	ROLLBACK
END CATCH


-----------------------------------------------------------------------------------------------------
--ALINEA I - testar função interByYear
IF ((SELECT COUNT(intervention_code) FROM dbo.interByYear(2021)) = 3)
	print 'Teste I: Function interByYear: OK'
ELSE
	print 'Teste I: Function interByYear: NOK'

-----------------------------------------------------------------------------------------------------
--ALINEA J  ('por atribuir','em analise','em execucao', 'concluido')) 
BEGIN TRY
BEGIN TRANSACTION
	
	DECLARE @intervention_code INT
	DECLARE @old_state VARCHAR(50)
	DECLARE @new_state VARCHAR(50)
	SET @intervention_code = 3

	SELECT @old_state = state FROM INTERVENTION WHERE intervention_code = @intervention_code

	EXEC dbo.updateStateIntervention @intervention_code, 'concluido', '2021-12-3'

	SELECT @new_state = state FROM INTERVENTION WHERE intervention_code = @intervention_code

	IF(@old_state != @new_state)
		print 'Teste J: Alterar Employee: OK'
	ELSE
		print 'Teste J: Alterar Employee: NOK'
	ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test J',16,1)
	ROLLBACK
END CATCH

-----------------------------------------------------------------------------------------------------
--ALINEA K
BEGIN TRY
BEGIN TRANSACTION
	-- First test to update state from 'em execucao' to 'concluido'
	UPDATE dbo.vw_summary_intervention SET intervention_state = 'concluido' WHERE intervention_code = 1
	IF EXISTS (SELECT intervention_code, state FROM dbo.INTERVENTION WHERE intervention_code = 1 AND state = 'concluido')
		PRINT 'Test K : OK'
	ELSE
		PRINT 'Test K : NOK'
	ROLLBACK
END TRY  
BEGIN CATCH
	RAISERROR('Entrei no catch test K',16,1)
	ROLLBACK
END CATCH
