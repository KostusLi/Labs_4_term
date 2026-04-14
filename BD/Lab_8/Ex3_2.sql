use UNIVER;

go
create view Аудитории
	as select *
	from AUDITORIUM
	where AUDITORIUM_TYPE = N'ЛК%'
	with check option;
go