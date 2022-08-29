/*
*   ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/
--create database EF_DEMO;
use EF_DEMO;
set xact_abort on;
begin tran
if object_id('dbo.StudentCourse') is not null
	drop table dbo.StudentCourse;
if object_id('dbo.Student') is not null
	drop table dbo.Student;
if object_id('dbo.Course') is not null
	drop table dbo.Course;
if object_id('dbo.Country') is not null
	drop table dbo.Country;
if object_id('GetCoursesByStudentId') is not null
	drop proc GetCoursesByStudentId;

create table dbo.Country
(
	countryId int identity(1,1) primary key,
	name varchar(50) not null unique
);

create table dbo.Student
(
	studentId int identity(1,1) primary key,
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

--populate

insert into dbo.Country(name) values ('Portugal'), ('Ireland'),('Sweden'),('Norway');
insert into dbo.Course(name) values ('Information systems II'), ('Internet Programming'),('Concurrent programming');
set dateformat dmy;
insert into dbo.Student(name,dateBirth,sex,country) values ('John','21-01-1970','M',1),('Joe','12-07-1971','M',2),('Mary','4-05-1969','F',3), ('Bob','12-12-1970','M',1), ('Zoe','12-12-1978','F',3);
insert into dbo.StudentCourse values(1,1),(1,2),(1,3),(2,2),(2,3),(3,1),(3,3)	
go
--procs
CREATE PROCEDURE [dbo].[GetCoursesByStudentId]
        @StudentId int = null
    AS
    BEGIN
        SET NOCOUNT ON;

        -- Insert statements for procedure here
		select c.courseid, c.name 
		from studentcourse sc 
		inner join course c on c.courseid = sc.courseid		
		where sc.studentid = @StudentId 
    END
go
commit

