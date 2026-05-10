--1
go
use UNIVER;
SELECT * 
FROM sys.indexes 
WHERE name like N'PK%';

create table #explre (
    tind int, 
    tfield varchar(100)
);

set nocount on;
declare @i int = 1;
while @i <= 1500
begin
    insert into #explre (tind, tfield) 
    values (@i, replicate(N'строка ', 2) + cast(@i as varchar));
    set @i = @i + 1;
end;

select * from #explre where tind between 500 and 1000 order by tind;

create clustered index #explre_cl on #explre(tind asc);

select * from #explre where tind between 500 and 1000 order by tind;

drop table #explre;
go

--2
use UNIVER;
go

create table #ex (
    tkey int,
    cc int identity(1, 1),
    tf varchar(100)
);

set nocount on; 
declare @i int = 1;
while @i <= 20000
begin
    insert into #ex (tkey, tf) values (floor(30000 * rand()), replicate(N'строка ', 3));
    set @i = @i + 1;
end;

select * from #ex where tkey > 1500 and cc < 4500;

create index #ex_nonclu on #ex(tkey, cc);

select * from #ex where tkey > 1500 and cc < 4500;

select * from #ex where tkey = 556 and cc > 3;

drop table #ex;
go


--3
use UNIVER;
go

create table #ex2 (
    tkey int,
    cc int identity(1, 1),
    tf varchar(100)
);

set nocount on; 
declare @i int = 1;
while @i <= 10000
begin
    insert into #ex2 (tkey, tf) values (floor(10000 * rand()), N'тест');
    set @i = @i + 1;
end;

select cc from #ex2 where tkey > 5000;

create nonclustered index #ex2_tkey_x on #ex2(tkey) include (cc);

select cc from #ex2 where tkey > 5000;

drop table #ex2;
go


--4
use UNIVER;
go

create table #ex3 (tkey int, tf varchar(100));

set nocount on; 
declare @i int = 1;
while @i <= 10000
begin
    insert into #ex3 (tkey, tf) values (floor(25000 * rand()), N'данные');
    set @i = @i + 1;
end;

select tkey from #ex3 where tkey between 5000 and 19999;
select tkey from #ex3 where tkey > 15000 and tkey < 20000;


create index #ex3_where on #ex3(tkey) where (tkey >= 15000 and tkey < 20000);


select tkey from #ex3 where tkey between 5000 and 19999;

select tkey from #ex3 where tkey > 15000 and tkey < 20000;

drop table #ex3;
go


--5
use UNIVER;
go

create table #ex4 (tkey int, tf varchar(100));
create index #idx_ex4 on #ex4(tkey);

set nocount on; 
declare @i int = 1;
while @i <= 10000
begin
    insert into #ex4 (tkey, tf) values (floor(30000 * rand()), replicate(N'хлам ', 5));
    set @i = @i + 1;
end;

select 
    name as [Индекс], 
    avg_fragmentation_in_percent as [Фрагментация (%)]
from sys.dm_db_index_physical_stats(db_id('tempdb'), object_id('tempdb..#ex4'), null, null, 'detailed') as ss
join tempdb.sys.indexes as ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id
where name is not null;

alter index #idx_ex4 on #ex4 reorganize;

select name, avg_fragmentation_in_percent from sys.dm_db_index_physical_stats(db_id('tempdb'), object_id('tempdb..#ex4'), null, null, 'detailed') ss join tempdb.sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id where name is not null;

alter index #idx_ex4 on #ex4 rebuild with (online = off);

select name, avg_fragmentation_in_percent from sys.dm_db_index_physical_stats(db_id('tempdb'), object_id('tempdb..#ex4'), null, null, 'detailed') ss join tempdb.sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id where name is not null;


drop index #idx_ex4 on #ex4;
go

--6
go
create index #idx_ex4 on #ex4(tkey) with (fillfactor = 65);

declare @j int = 1;
while @j <= 5000
begin
    insert into #ex4 (tkey, tf) values (floor(30000 * rand()), N'новые данные');
    set @j = @j + 1;
end;

select name, avg_fragmentation_in_percent from sys.dm_db_index_physical_stats(db_id('tempdb'), object_id('tempdb..#ex4'), null, null, 'detailed') ss join tempdb.sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id where name is not null;

drop table #ex4;
go