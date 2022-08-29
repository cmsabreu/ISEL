/*
*   ISEL-ADEETC-SI2
*   ND 2017-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

--create table x(i int primary key,z char not null);

-- Testar nas várias combinações, trocando comentários

set xact_abort OFF 
-- set xact_abort ON 

begin try

begin transaction 

	
	begin try
	begin transaction
		insert into dbo.X(i,z) values(10,'10'),(9,'10'); 
		--raiserror ('erro grave', --MSG
		--			15, 
		--			1);

		--raiserror ('warning', --MSG
		--			5, 
		--			1);
		
		--;THROW 51000, 'Erro Grave.', 1; 
		insert into dbo.X(i,z) values(17,'17'); 
	commit 
	end try
	begin catch
		rollback
		;throw
		
	end catch


	insert into dbo.X(i,z) values(18,'18'); 
	

commit

end try
begin catch
    if (@@trancount <> 0)
		rollback
	;throw
end catch

select * from dbo.X 
