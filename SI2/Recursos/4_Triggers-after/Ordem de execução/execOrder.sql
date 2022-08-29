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
if db_id('triggersdb') is null
	create database triggersdb;
go
use triggersdb;

/* exemplo sobre controlo de triggers*/
if object_id('dbo.t3') is not null
  drop table dbo.t3;
go

create table dbo.t3
(
  keycol  int not null primary key,
  datacol int not null
);
go

/* trigger que apenas funciona para um tuplo*/
create trigger trg_exect1 on t3 
after insert,update,delete
as
declare @type as nvarchar(10)
set nocount on

if exists(select * from inserted) 
begin 
	if exists(select * from deleted)
		set @type = 'update'
	else 
		set @type = 'insert'		
end		
else
	set @type = 'delete'
print 't1 trigger for ' + @type
go
create trigger trg_exect2 on t3 
after insert,update,delete
as
declare @type as nvarchar(10)
set nocount on

if exists(select * from inserted) 
begin 
	if exists(select * from deleted)
		set @type = 'update'
	else 
		set @type = 'insert'		
end		
else
	set @type = 'delete'		
print 't2 trigger for ' + @type
go
create trigger trg_exect3 on t3 
after insert,update,delete
as
declare @type as nvarchar(10)
set nocount on

if exists(select * from inserted) 
begin 
	if exists(select * from deleted)
		set @type = 'update'
	else 
		set @type = 'insert'		
end		
else
	set @type = 'delete'		
print 't3 trigger for ' + @type
go

create trigger trg_exect4 on t3 
after insert,update,delete
as
declare @type as nvarchar(10)
set nocount on

if exists(select * from inserted) 
begin 
	if exists(select * from deleted)
		set @type = 'update'
	else 
		set @type = 'insert'		
end		
else
	set @type = 'delete'		
print 't4 trigger for ' + @type
go
insert into dbo.t3(keycol, datacol) values(1, 10);

update dbo.t3 set datacol=20

delete from dbo.t3

-- T3, ?...,?,T1
exec sp_settriggerorder @triggername = 'trg_exect3', @order = 'first', @stmttype = 'insert'
exec sp_settriggerorder @triggername = 'trg_exect1', @order = 'last', @stmttype = 'insert'
go
print 't3 fisrt, t1 last for insert'
insert into dbo.t3(keycol, datacol) values(1, 10);

update dbo.t3 set datacol=20

delete from dbo.t3
go

--??,??,??
exec sp_settriggerorder @triggername = 'trg_exect3', @order = 'none', @stmttype = 'insert'
exec sp_settriggerorder @triggername = 'trg_exect1', @order = 'none', @stmttype = 'insert'
go
exec sp_settriggerorder @triggername = 'trg_exect1', @order = 'first', @stmttype = 'insert'
go

--t1,...,t4
print 't1 fisrt, t4 last for insert'
insert into dbo.t3(keycol, datacol) values(1, 10);

update dbo.t3 set datacol=20

delete from dbo.t3

