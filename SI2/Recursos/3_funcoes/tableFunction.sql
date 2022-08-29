/*#####################################
#
#	ISEL, ND, 2019-2021
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
use si2;
go

if OBJECT_ID('dbo.ListaCliente') is not null
    drop function dbo.ListaCliente

if OBJECT_ID('dbo.ListaCliente2') is not null
    drop function dbo.ListaCliente2

if OBJECT_ID('dbo.Cliente') is not null
    drop table dbo.Cliente 
GO  

create table dbo.Cliente
(
    id int identity(1,1) primary key,
    nome varchar(30) not null
)

insert into dbo.Cliente values('João'),('Maria'),('Martim'),('Matilde');



GO
create function dbo.ListaCliente(@nome varchar(50))
returns TABLE
WITH SCHEMABINDING
as
--BEGIN
   return (select id,nome from dbo.cliente where nome=@nome)   
--END   
go    

create function dbo.ListaCliente2(@nome varchar(50))
returns @ret table (i int, j varchar(10))
WITH SCHEMABINDING
as
BEGIN
   insert into @ret select id,nome from dbo.cliente where nome=@nome   
   return
END   
go    


select * from dbo.ListaCliente('João');
select * from dbo.ListaCliente(dbo.trim('Matilde   '));


--Verificar se a função é deterministica
SELECT OBJECTPROPERTYEX(OBJECT_ID('dbo.ListaCliente'), 'IsDeterministic')
SELECT OBJECTPROPERTYEX(OBJECT_ID('dbo.ListaCliente2'), 'IsDeterministic')