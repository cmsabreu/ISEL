--USE L51DG5--uncomment when using isel DB
BEGIN TRY
BEGIN TRANSACTION 
    PRINT 'Inserting data into tables';

   
   	INSERT INTO EMPLOYEE(ssn, f_name, l_name, birth_date, address, postal_code, city, job, phone_number, mail) VALUES
   	
		(123432987, 'Maria', 'Silva', '1994-09-03', 'Rua cor de rosa', '1200-381', 'Lisboa', 'engenheiro de software', 965432121, 'mariasilva@gmail.com'),
        (987654321, 'João', 'Santos', '1989-03-25','Rua Augusta', '1100-048', 'Lisboa', 'canalizador',925765123, 'joaosantos@gmail.com'),
        (345654321, 'Pedro', 'Ferreira', '1990-07-16', 'Rua da Bica', '1200-054', 'Lisboa', 'carpinteiro', 917865498, 'pedroferreira@gmail.com'),
		---novos equipa porto
		(944138611, 'Pedro', 'Silva', '1974-09-03', 'Praceta Silva', '1200-381', 'Porto', 'CEO', 57728464, 'pedrosilva@gmail.com'), --supervisor
        (60368458, 'Bernardo', 'Santos', '1989-03-25','Avenida Maria Augusta', '1100-048', 'Braga', 'CFO',329136103, 'bernardosantos@gmail.com'),
        (764639549, 'Cátia', 'Correia', '1990-07-16', 'Rua da Picante', '1200-054', 'Porto', 'CTO', 500716907, 'cFerreira@gmail.com'),
		(901774115, 'Patricia', 'Lopes', '1994-09-03', 'Rua Lopes Carvalho', '1200-381', 'Vila Nova de gaia', 'developer', 599471652, 'plopes@gmail.com'),
        (411424231, 'Joana', 'Santos', '1989-03-25','Rua Mota', '1100-048', 'Porto', 'developer',271752120, 'joaosantos@gmail.com'),
		--equipa lisboa
        (995990519, 'Kevin', 'Ferreira', '1977-07-16', 'Rua de Arroios', '1200-054', 'Lisboa', 'Senior', 386439106, 'kevin88@gmail.com'), --supervisor
		(479000596, 'Joana', 'Silva', '1994-09-03', 'Rua Santos de rosa', '1200-381', 'Lisboa', 'junior', 635425595, 'joannasasa@gmail.com'),
        (787247844, 'Susana', 'Santos', '1989-03-25','Avenida Liberdade Augusta', '1100-048', 'Lisboa', 'developer',835895512, 'susy@gmail.com'),
		-- equipa aveiro
		(932863514, 'Lucas', 'Afonso', '1990-09-03', 'Praceta de Aveiro', '1200-381', 'Aveiro', 'Chefe canalização', 218162782, 'lucaslo@gmail.com'), --supervisor
		(806989791, 'Margarida', 'Mira', '1994-09-03', 'Rua do comercio', '1200-381', 'Aveiro', 'Canalizador', 818020686, 'lolmargarida@gmail.com');

	INSERT INTO TYPE(description) VALUES
		('Jardins'),('Manter a agua a circular'), ('Eletrico'), ('Informatica');

	INSERT INTO ASSET(asset_name, acquisition_date, state, brand, model, location, asset_reference, manager, type) VALUES
		('piscina', '2021-07-27', 1, 'intex', 'semi enterradas', 'Lisboa', NULL,123432987, 1),
        ('bomba de agua', '2021-07-27', 1, 'espa', 'VG 400AS', 'Lisboa', 1,123432987, 1), 
        ('aspirador', '2020-10-12', 0, 'dyson', 'V12 slim', 'Porto', NULL,345654321, 3);

	INSERT INTO REGISTER(asset_id, alteration_date, price) VALUES
		(1, '2021-07-27', 1500),
		(1, '2021-10-15', 1479),
		(3, '2020-10-12', 400),
		(3, '2021-06-16', 389);

	INSERT INTO MAINTENANCE_TEAM(location, n_elements,supervisor) VALUES
		('Porto', 5,944138611),('Lisboa', 3,995990519),('Aveiro', 2,932863514);
	
	INSERT INTO TEAM_MEMBER(id, team_code) VALUES
		(944138611,1), (60368458,1), (764639549,1), (901774115,1), (411424231,1),
		(995990519,2), (479000596,2), (787247844,2),
		(932863514,3), (806989791,3);


	INSERT INTO INTERVENTION(description, state, price, start_date, end_date, asset_id) VALUES
        ('avaria', 'em execucao', 104.99, '2021-11-13', NULL, 1),
        ('rutura', 'concluido', 457.65, '2021-08-12', '2021-09-12', 2),
        ('inspecao', 'em execucao', 34.99, '2021-10-11', NULL, 3),
		('avaria', 'concluido', 128.95, '2020-07-21', '2020-12-09', 3);
	
	INSERT INTO PERIODIC(id, frequency) VALUES
		(3, 2),(1, 2);

	INSERT INTO NON_PERIODIC(id) VALUES
		(2);

	INSERT INTO SCHEDULING(team_code, intervention_code, scheduling_date) VALUES
        (1, 2, '2021-08-13'),
        (2, 3, '2021-10-12');

	INSERT INTO SKILL(description) VALUES
		('desentupimento industrial'),('serrador'),('java'), ('sql server'), ('c#'),('canalização');


	INSERT INTO EMPLOYEE_SKILL(employee_code, skill_code) VALUES
		(944138611, 3), (60368458, 4), (764639549, 6), (901774115, 5),(411424231, 4), 
		(995990519,3), (479000596, 3), (787247844, 4),
		(932863514, 6),(806989791, 1),
		(987654321, 1),(345654321, 2),(123432987, 3);


    COMMIT TRANSACTION; 
END TRY  
BEGIN CATCH 
    SELECT 'Entrei no catch'  
    ROLLBACK
    raiserror('Erro',16,1)
END CATCH