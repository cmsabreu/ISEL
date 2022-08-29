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

set xact_abort on
if object_id('##log') is not null
	drop table ##log
go
create table ##log
(
	[id] int primary key,
	[init] datetime  not null,
	[end]  datetime  not null
)
go
begin tran
-- Implementação 1

declare @numb int, @v int, @cont int,@acc real,@value int

set @numb = 0
set @acc = 0
set @v = 0
set @cont = 0
set @value = 2

declare @t1 datetime
set @t1 = getdate()

declare c cursor local
for select idade from Cliente

open c

fetch next from c into @v

while @@fetch_status =0
begin
	if @numb = 0
	begin
		set @acc = @acc + @v
		set @cont = @cont +1
	end

	set @numb = (@numb + 1) % @value
	fetch next from c into @v
end

insert into ##log values(1, @t1, getdate())
commit

begin tran
-- Implementação 2
	declare @t2 datetime
	set @t2 = getdate()
	select avg(idade) from cliente

	insert into ##log values(2, @t2, getdate())
commit

set xact_abort off
select datediff(ms,[init],[end]) as processedIn
from ##log
