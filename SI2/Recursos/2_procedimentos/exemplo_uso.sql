/*#####################################
#
#	ISEL, ND, 2018-2020
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
set xact_abort on

begin tran

if(object_id('dbo.getX','P') is not null)
	drop proc dbo.getX
go

if(object_id('dbo.X') is not null)
	drop table dbo.X
go

create table dbo.X(
	i int primary key,
	z char
)
insert into dbo.X values(1,'a'),(2,'b'),(3,'c')
go

create proc dbo.getX @id int=NULL, @n int=NULL OUTPUT
as
if(@id is not null)
	select * from dbo.x where i = @id
else
	select * from dbo.x

set @n = @@rowcount
return 1
go

commit

--ver resultados seguintes
declare @n1 int = 33, @ret int
exec @ret=getX 1,@n=@n1
select @n1 as no_tuplos,@ret as retorno

exec @ret=getX 1,@n=@n1 OUTPUT
select @n1 as no_tuplos,@ret as retorno

exec @ret=getX @n=@n1 OUTPUT,@id=DEFAULT
select @n1 as no_tuplos,@ret as retorno

exec @ret=getX @n=@n1 OUTPUT
select @n1 as no_tuplos,@ret as retorno
