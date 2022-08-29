/*
*   ISEL-ADEETC-SI2
*   ND 2015-2016
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

use SI2;
select * from Account;
select * from xferContext;

declare @id int,@state int
set @id = 1 

exec @state=xfer @id,1,2,50.0
select @state
select * from Account;
select * from xferContext;

set @id = 2 
exec @state=xfer @id,1,2,75.0
select @state
select * from Account;
select * from xferContext;
