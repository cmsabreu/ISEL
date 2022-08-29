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
go
use triggersdb
go
if  exists (select * from sys.triggers where name = 'trg_historicocli')
	drop trigger trg_historicocli 
go
create trigger trg_historicocli
on cliente 
after update,delete
as
set nocount on
insert into historicocliente([nome],[oldmorada]) select * from deleted
go

select * from cliente
select * from historicocliente

update cliente set morada='morada 1_1' where nome='user1'
delete from cliente where nome = 'user1'

select * from cliente
select * from historicocliente

