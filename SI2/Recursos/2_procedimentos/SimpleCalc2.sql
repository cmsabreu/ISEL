/*#####################################
#
#	ISEL, ND, 2017-2020
#
#   Material didático para apoio 
#   à unidade curricular de 
#   Sistemas de Informação II
#
#   Os exemplos podem não ser completos e/ou totalmente correctos
#   sendo desenvolvido com objectivos pedagógicos
#   Eventuais incorrecções são alvo de discussão
#   nas aulas. Testado SSMS v18 + SQLServer 2017
#
#######################################*/
USE master;
GO
EXEC sp_addmessage @msgnum = 60000, @severity = 16, 
   @msgtext = N'The operation ''%s'' is unknown', 
   @lang = 'us_english',
   @replace = 'replace';
GO
use si2;
set xact_abort on
GO

if object_id('dbo.SimpleCalculator ') is not null
    drop procedure dbo.SimpleCalculator 
go
create proc dbo.SimpleCalculator @p1 int , @p2 int,  @res int output, @oper char = '+'
as  
        set @res = 
    case @oper 
        when '+' then @p1 + @p2
        when '-' then @p1 - @p2
        when '*' then @p1 * @p2
        when '/' then @p1 / @p2
        else null
    end
    if ( @res is null)
    begin
        DECLARE @msg NVARCHAR(2048) = FORMATMESSAGE(60000, cast(@oper as varchar(30)));
        THROW 60000,@msg,1;
    end
    return 0
go

declare @res int
exec dbo.SimpleCalculator 10, 20, @res output
select @res
exec dbo.SimpleCalculator 10, 20, @res output, @oper=default 
select @res
exec dbo.SimpleCalculator 10, 20, @oper=default, @res=@res output
select @res
exec dbo.SimpleCalculator 10, 20, @res output,'-'
select @res
exec dbo.SimpleCalculator 10, 20, @res output,'*'
select @res
exec dbo.SimpleCalculator 100, 20,@res output,'/'
select @res

exec dbo.SimpleCalculator 100, 20,@res output,'?'
select @res
