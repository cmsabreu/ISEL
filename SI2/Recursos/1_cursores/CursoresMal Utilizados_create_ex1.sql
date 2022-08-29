/*
*   ISEL-ADEETC-SI2
*   ND 2017-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*
*/
--remove SI2 db is exists
/*
use master;
ALTER DATABASE SI2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE SI2 ;
GO
CReate database si2;
GO*/
use si2;
go

SET xact_abort ON
BEGIN TRAN

if object_id('dbo.conta') is not null
	drop table dbo.conta
go
if object_id('cliente') is not null
	drop table cliente
go
 
create table [dbo].[cliente]
(
	id int identity primary key,
	nome varchar(100) not null,
	idade tinyint not null check (idade between 0 and 120),
	[data] datetime not null default getdate()
)

create table [dbo].[conta]
(
	id int identity,
	descricao varchar(100) not null,
	saldo smallmoney not null,
	cliente int not null references cliente,
	updated datetime not null default getdate()
)
go

/*
	script t-sql que permita popular a tabela, com valores aleatórios, 
	de forma a conseguir inserir 100 000 (cem mil) registos. preste principal atençãoo à 
	idade, que deve ter uma distribuição aleatória.
*/

declare @idx int
set @idx = 0
set nocount on
-- variáveis auxiliares
declare @name varchar(10)
declare @age int

while @idx < 100000
begin
	set @name = 'Cliente ' + cast(@idx as varchar(6))
	set @age = rand() * 120 + 1
	insert cliente (nome, idade) values (@name,	@age)
	set @idx = @idx + 1
end

go

declare @idx int,@saldo smallmoney,@ncontas int
set @idx = 0
set @ncontas = 3

set nocount on

-- variáveis auxiliares
declare @descricao varchar(15), @nc int, @cli int

--criar 1000 contas
while @idx < 1000
begin
	--numero de contas a criar para o mesmo cliente
	set @nc = rand() * @ncontas +1
	set @cli = rand()* (select max([id]) from cliente)+1
	while @nc> 0
	begin 	
		set @saldo = rand() * 10000
		set @descricao = 'Conta ' + cast(@idx as varchar(6))
		insert conta(descricao, saldo, cliente) values (@descricao,	@saldo, @cli)
		set @idx = @idx + 1
		set @nc = @nc - 1
	end
end
COMMIT
SET xact_abort off

select * from cliente
select * from conta
