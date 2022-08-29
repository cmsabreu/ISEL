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
set nocount on
set xact_Abort on
begin tran

drop table if exists  FIBO
create table FIBO
(
	i int primary key
)
commit
go

begin tran

declare @a int
declare @b int
declare @fib int

set @a = 0
set @b = 1
set @fib = 0

insert into fibo values(@a)

while @fib < 1000
begin
	set @fib = @a + @b
	insert into fibo values(@fib)
	set @a = @b
	set @b = @fib
end
commit
go
set xact_Abort off

