/*#####################################
#
#	ISEL, ND, 2018-2021
#
#   Material didático para apoio 
#   à unidade curricular de 
#   Sistemas de Informação II
#
#	Os exemplos podem não ser completos e/ou totalmente correctos
#	sendo desenvolvido com objectivos pedagógicos
#	Eventuais incorrecções são alvo de discussão
#	nas aulas. Testado SSMS v18 + SQLServer 2017
#
#######################################*/
IF DB_ID('TRIGGERSDB') IS NULL
	CREATE DATABASE TRIGGERSDB;
GO
use triggersdb
go
if object_id('dbo.cliente') is not null
	drop table dbo.cliente
go
create table [dbo].[cliente](
	[nome] [varchar](50) not null primary key,
	[morada] [varchar](100) not null
)
go

use [triggersdb]
go
if object_id('dbo.historicocliente') is not null
	drop table dbo.historicocliente
go
create table [dbo].[historicocliente](
	[id] [int] identity(1,1) not null primary key,
	[changed] [datetime] not null default (getdate()),
	[nome] [varchar](50) not null,
	[oldmorada] [varchar](100) not null
)

set nocount on
declare @ntuples int
set @nTuples = 100
while @nTuples > 0
begin
	insert into cliente(nome,morada) values('User'+cast(@nTuples as varchar),
					cast(cast(rand()*7 as int) + 1 as varchar)+' avenida, nº '+ cast(cast(rand()*500 as int) +1 as varchar))
	set @nTuples = @nTuples - 1	
end
