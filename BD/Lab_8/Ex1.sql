create view Преподаватель_1
	as select top 20 *
	from TEACHER
	order by TEACHER.PULPIT
