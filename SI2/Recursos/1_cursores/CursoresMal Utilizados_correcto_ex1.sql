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
go
--Script que actualiza as contas dos clientes com mais de 65  anos em 10% da sua idade
update conta set saldo = saldo + (select 0.10*idade from cliente where cliente.id = conta.cliente)
where cliente in (select [id] from cliente where idade > 65)
select @@rowcount
