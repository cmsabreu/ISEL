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
USE si2;
GO
if object_id('dbo.Trim') is not null
	drop function dbo.trim
go
CREATE FUNCTION dbo.Trim (@String varchar(8000))
RETURNS varchar(8000)
AS
BEGIN 
	SELECT @String = LTRIM(RTRIM(@String))
	RETURN @String
END

go

select dbo.Trim('     teste                                                  ') as a


if object_id('NotProperStrings') is not null
	drop table NotProperStrings
go

create table NotProperStrings
(
	strings varchar(30) primary key
)

insert into NotProperStrings values('  Uma'),('   String  '),('Sem                '),('Espaços')

select dbo.Trim(strings) as res
from NotProperStrings

