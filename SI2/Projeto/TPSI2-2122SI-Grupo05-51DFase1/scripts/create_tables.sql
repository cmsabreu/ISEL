--USE L51DG5--uncomment when using isel DB
GO
IF OBJECT_ID('EMPLOYEE_SKILL') IS NOT NULL
    DROP TABLE dbo.EMPLOYEE_SKILL
IF OBJECT_ID('SKILL') IS NOT NULL
    DROP TABLE dbo.SKILL
IF OBJECT_ID('SCHEDULING') IS NOT NULL
    DROP TABLE dbo.SCHEDULING
IF OBJECT_ID('NON_PERIODIC') IS NOT NULL
    DROP TABLE dbo.NON_PERIODIC
IF OBJECT_ID('PERIODIC') IS NOT NULL
    DROP TABLE dbo.PERIODIC
IF OBJECT_ID('INTERVENTION') IS NOT NULL
    DROP TABLE dbo.INTERVENTION	
IF OBJECT_ID('TEAM_MEMBER') IS NOT NULL
    DROP TABLE dbo.TEAM_MEMBER
IF OBJECT_ID('MAINTENANCE_TEAM') IS NOT NULL
    DROP TABLE dbo.MAINTENANCE_TEAM
IF OBJECT_ID('REGISTER') IS NOT NULL
    DROP TABLE dbo.REGISTER
IF OBJECT_ID('ASSET') IS NOT NULL
    DROP TABLE dbo.ASSET
IF OBJECT_ID('TYPE') IS NOT NULL
	DROP TABLE dbo.TYPE
IF OBJECT_ID('EMPLOYEE') IS NOT NULL
    DROP TABLE dbo.EMPLOYEE
GO
BEGIN TRY  
	BEGIN TRANSACTION
	print 'Creating Tables'

		CREATE TABLE EMPLOYEE(
		ssn INT, 
		f_name VARCHAR(50) NOT NULL,
		l_name VARCHAR(50) NOT NULL,
		birth_date DATE NOT NULL,
		address VARCHAR(50) NOT NULL,
		postal_code VARCHAR(50) NOT NULL,
		city VARCHAR(50) NOT NULL,
		job VARCHAR(50) NOT NULL,
		phone_number INT NOT NULL,
		mail VARCHAR(100),
		PRIMARY KEY(ssn)

	);

	CREATE TABLE TYPE(
        id INT IDENTITY(1,1) PRIMARY KEY, 
        description VARCHAR(200)
    );
	
	CREATE TABLE ASSET(
		id INT IDENTITY(1,1) PRIMARY KEY,
		asset_name VARCHAR(30) NOT NULL,
		acquisition_date DATE NOT NULL,
		state BIT NOT NULL, -- “0” ou “1”, i.e., desactivado ou operacional
		brand VARCHAR(30),
		model VARCHAR(30),
		location VARCHAR(30) NOT NULL,
		asset_reference INT,
		manager INT,
		type INT,
		FOREIGN KEY(asset_reference) REFERENCES ASSET(id),
		FOREIGN KEY(manager) REFERENCES EMPLOYEE(ssn),
		FOREIGN KEY(type) REFERENCES TYPE(id)

	);

	CREATE TABLE REGISTER(
		asset_id INT,
		alteration_date DATE,
		price DECIMAL(6,2),
		PRIMARY KEY(asset_id, alteration_date),
		FOREIGN KEY(asset_id) REFERENCES ASSET(id)
	);

	CREATE TABLE MAINTENANCE_TEAM(
		team_code INT IDENTITY(1,1) PRIMARY KEY,
		location VARCHAR(50) NOT NULL,
		n_elements INT NOT NULL CHECK (n_elements >= 2),
		supervisor INT,
		FOREIGN KEY (supervisor) REFERENCES EMPLOYEE(ssn)
	);

	CREATE TABLE TEAM_MEMBER(
		id INT,
		team_code INT,
		PRIMARY KEY(id, team_code),
		FOREIGN KEY(id) REFERENCES EMPLOYEE(ssn),
		FOREIGN KEY(team_code) REFERENCES MAINTENANCE_TEAM(team_code)
	);	

	CREATE TABLE INTERVENTION(
        intervention_code INT IDENTITY(1,1) PRIMARY KEY, 
        description VARCHAR(50) CHECK(description IN('avaria','rutura','inspecao')),
		state VARCHAR(50) CHECK(state IN('por atribuir','em analise','em execucao', 'concluido')),
		price DECIMAL(10,2) NOT NULL,
		start_date DATE NOT NULL,
		end_date DATE,
		asset_id INT
		FOREIGN KEY(asset_id) REFERENCES ASSET(id)
    );

	CREATE TABLE PERIODIC(
		id INT PRIMARY KEY,
		frequency INT CHECK (frequency <= 12),
		FOREIGN KEY(id) REFERENCES INTERVENTION(intervention_code)
    );

	CREATE TABLE NON_PERIODIC(
		id INT PRIMARY KEY,
		FOREIGN KEY(id) REFERENCES INTERVENTION(intervention_code)
	);

	CREATE TABLE SCHEDULING(
		team_code INT,
		intervention_code INT,
		scheduling_date DATE,
		PRIMARY KEY(team_code, intervention_code),
		FOREIGN KEY(team_code) REFERENCES MAINTENANCE_TEAM(team_code),
		FOREIGN KEY(intervention_code) REFERENCES INTERVENTION(intervention_code)
	);

	CREATE TABLE SKILL(
		id INT IDENTITY(1,1) PRIMARY KEY,
		description VARCHAR(100),
    );

	CREATE TABLE EMPLOYEE_SKILL(
		employee_code INT,
		skill_code INT,
		PRIMARY KEY(employee_code, skill_code),
		FOREIGN KEY(employee_code) REFERENCES EMPLOYEE(ssn),
		FOREIGN KEY(skill_code) REFERENCES SKILL(id)
    );

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK
	RAISERROR('Erro in createTable', 16, 1)
END CATCH
