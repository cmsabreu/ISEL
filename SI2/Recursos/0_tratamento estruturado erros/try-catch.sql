/*
*   ISEL-ADEETC-SI2
*   ND 2017-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos
*
*/
set tran isolation level read committed
begin try
	begin tran
	-- Testar com as hipoteses abaixo
	insert into dbo.x values (0,'b')
	--raiserror ('Mensagem de aviso',5,1)
	--raiserror ('Mensagem de erro',16,2)
	--raiserror ('Mensagem de erro grave',21,1) WITH LOG
	--;throw 51000,'Mensagem de erro',2;
	SELECT @@TRANCOUNT
	commit
	
end try
begin catch
	if (@@TRANCOUNT >0)
		rollback
	;throw	--Experimentar com e sem

end catch

select @@TRANCOUNT
