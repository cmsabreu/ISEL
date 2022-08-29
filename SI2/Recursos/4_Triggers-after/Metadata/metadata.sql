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
if  exists (select * from sys.server_triggers where name = 'trg_audit_ddl_logins')
	drop trigger trg_audit_ddl_logins on ALL SERVER
go
CREATE TRIGGER trg_audit_ddl_logins ON ALL SERVER
  FOR DDL_LOGIN_EVENTS
AS
Print 'DDL_LOGIN_EVENTS' 
go
SELECT name, s.type_desc SQL_or_CLR, 
is_disabled, e.type_desc FiringEvents, e.is_First, e.is_Last
FROM sys.server_triggers s
INNER JOIN sys.server_trigger_events  e ON
s.object_id = e.object_id

go

SELECT t.name, m.Definition
FROM sys.server_triggers AS t
INNER JOIN sys.server_sql_modules m ON
t.object_id = m.object_id


SELECT t.name, m.Definition
FROM sys.triggers AS t
INNER JOIN sys.sql_modules m ON
t.object_id = m.object_id


