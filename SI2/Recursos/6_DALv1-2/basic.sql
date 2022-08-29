/*#####################################
#
#	ISEL, ND, 2018-2021
#
#   Material didático para apoio 
#   à unidade curricular de 
#   Sistemas de Informação II
#
#	Os exemplos podem não ser completos e/ou totalmente correctos
#	sendo desenvolvido com objectivos pedagógicos
#	Eventuais incorrecções são alvo de discussão
#	nas aulas. Testado SSMS v18 + SQLServer 2017
#
#######################################*/

if exists (select name from sys.databases where name='DAL_DEMO')
begin
	alter database DAL_DEMO set single_user with rollback immediate;
	use master;
	drop database DAL_DEMO;
end
go

create database DAL_DEMO;
go
use DAL_DEMO;
set xact_abort on;
begin tran

	drop table if exists dbo.StudentCourse;
	drop table if exists dbo.Student;
	drop table if exists dbo.Course;

	drop table if exists dbo.Country;

create table dbo.Country
(
	countryId int identity(1,1) primary key,
	name varchar(50) not null unique
);

create table dbo.Student
(
	studentNumber int primary key,
	name nvarchar(256) not null,
	dateBirth Date,
	sex char not null,

	country int not null references dbo.Country
);

create table dbo.Course
(
	courseId int identity(1,1) primary key,
	name nvarchar(256) not null
);

create table dbo.StudentCourse
(
	studentId int references  dbo.Student,
	courseId int references dbo.Course,
	primary key(studentId,courseId)
);


commit
