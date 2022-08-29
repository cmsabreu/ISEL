/*#####################################
#
#	ISEL, ND, 2017-2020
#
#   Material didático para apoio 
#   à unidade curricular de 
#   Sistemas de Informação II
#
#	Os exemplos podem não ser completos e/ou totalmente correctos
#	sendo desenvolvido com objectivos pedagógicos
#	Eventuais incorrecções são alvo de discussão
#	nas aulas.
#
#######################################*/
SELECT definition, execute_as_principal_id, 
is_recompiled, uses_ansi_nulls, uses_quoted_identifier
FROM  sys.sql_modules m INNER JOIN sys.objects o 
ON m.object_id = o.object_id
WHERE o.type = 'P'
