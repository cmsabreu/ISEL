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
use triggersdb;
set xact_abort on;
begin tran
if object_id('c') is not null
	drop table c;
if object_id('d') is not null
	drop table d;	

go	
create table C
(
	x int primary key,
	c char
)

create table d
(
	x int primary key,
	d char
)
go

if object_id('dbo.recC') is not null
	drop trigger dbo.recC
if object_id('dbo.recD') is not null
	drop trigger dbo.recD	
go

create trigger recC
on dbo.C
after insert
as 
-- After a first execution, re-run uncommont the next two lines
	/*if exists(select * from dbo.d where x>15)
		return
    */
	insert into dbo.d(x,d) select coalesce(max(x),0)+1,'x' from dbo.d
go

create trigger recD
on dbo.D
after insert
as 
-- After a first execution, re-run uncomment the next two lines
	/*if exists(select * from dbo.c where x>15)
		return
	*/
	insert into dbo.C(x,c) select coalesce(max(x),0)+1,'y' from dbo.C
go
commit
insert into C select 1,'a'
insert into D select max(x)+1,'b' from dbo.d

select * from C
select * from D


--execute the folowing lines and re-run all over again
/*
USE master
GO
EXEC sp_configure 'nested triggers', 1 -- 0 –Disable, 1-Enable
RECONFIGURE WITH OVERRIDE
*/



