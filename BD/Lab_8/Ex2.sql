create view [Количество кафедр]
	as select F.FACULTY, count(P.PULPIT) AS [Количество кафедр]
	from FACULTY AS F join PULPIT AS P
	ON F.FACULTY = P.FACULTY
	group by F.FACULTY