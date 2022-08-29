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
use si2
if OBJECT_ID('Random') is not null
    drop function Random

if OBJECT_ID('vRandomNumber') is not null
    drop view vRandomNumber
GO
CREATE VIEW dbo.vRandomNumber
WITH SCHEMABINDING
AS
SELECT RAND() AS RandomNumber
GO

CREATE FUNCTION dbo.Random()
RETURNS NUMERIC(18, 10)
WITH SCHEMABINDING 
AS
BEGIN
    RETURN (SELECT TOP 1 RandomNumber FROM dbo.vRandomNumber)
END
GO

select *,dbo.Random() as R from dbo.cliente 
select *,rand() as R from dbo.cliente

--Verificar se a função é deterministica
SELECT OBJECTPROPERTYEX(OBJECT_ID('dbo.Random'), 'IsDeterministic')