use UNIVER;



drop view Дисциплины;

go
create view Дисциплины
	as select top 100 *
	from SUBJECT
	order by SUBJECT.SUBJECT desc
go

go
	alter view [Количество кафедр] with schemabinding
		as select F.FACULTY [Факультет], count(P.PULPIT) [Число кафедр]
		from dbo.FACULTY as F join dbo.PULPIT as P
		on F.FACULTY = P.FACULTY
		group by F.FACULTY
go