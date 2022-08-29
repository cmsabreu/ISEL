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
use triggersdb
go
if  exists (select * from sys.triggers where name = 'trg_detect')
	drop trigger trg_detect 
go
create trigger trg_detect on dbo.cliente 
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
print 'trigger for ' + @type
go