/*
*   ISEL-ADEETC-SI2
*   ND 2017-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*
*/
use si2
go

declare  iterate_fibo cursor LOCAL FORWARD_ONLY 
for select * from fibo
open iterate_fibo

declare @curNumber int
FETCH NEXT FROM iterate_fibo 
into @curNumber
while @@FETCH_STATUS = 0
BEGIN
	print @curNumber

	FETCH NEXT FROM iterate_fibo 
	into @curNumber
END
close iterate_fibo
deallocate iterate_fibo
