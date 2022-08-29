if object_id('Cliente') is not null
	drop table Cliente
go
create table Cliente
(
  id int primary key identity(1,1),
  nome varchar(50) not null,
  apelido varchar(50) not null,
  idade tinyint not null  
);
go
insert into Cliente(nome,apelido,idade) values('José','Filipe',20)
insert into Cliente(nome,apelido,idade) values('Paulo','Carvalho',30)
