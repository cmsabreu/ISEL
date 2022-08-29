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
use si2;

--Script que actualiza as contas dos clientes com mais de 65  anos em 10% da sua idade
declare @id int, @idade tinyint , @bonus float, @saldo smallmoney, @nrows int

set @nrows = 0

declare cliIdosos cursor local 
for select [id],[idade] from cliente where idade > 65  
open cliIdosos

fetch next from cliIdosos into @id,@idade

while @@fetch_status = 0
begin
	set @bonus = 0.1 * @idade
	declare contas cursor local 
	for select saldo from conta where cliente=@id
	for update

	open contas
	fetch next from contas into @saldo
	while @@fetch_status  = 0
	begin 
		update conta set saldo = saldo + @bonus where current of contas
		set @nrows = @nrows+ @@rowcount
		fetch next from contas into @saldo
	end
	close contas
	deallocate contas


	fetch next from cliIdosos into @id,@idade
end

close cliIdosos
deallocate cliIdosos

print @nrows
