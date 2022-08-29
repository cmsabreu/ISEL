IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[part]') AND type in (N'U'))
drop table part;

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vendor]') AND type in (N'U'))
drop table vendor;

create table Vendor
(
    id int primary key not null,
    name varchar(50) not null
);

create table Part
(
    id int not null, 
    descr varchar(15) not null,
    vendorid int references Vendor(id) not null,
    primary key(id,vendorid)    
);

insert into Vendor values(1,'Vendor 1');
insert into Vendor values(2,'Vendor 2');
insert into Vendor values(3,'Vendor 3');


insert into Part values(1,'Part 1',1);
insert into Part values(2,'Part 2',1);

insert into Part values(1,'Part 1',2);
insert into Part values(2,'Part 2',2);