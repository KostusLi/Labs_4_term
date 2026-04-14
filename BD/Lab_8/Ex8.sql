use UNIVER;


go
create view Расписание
as select 
    IDGROUP as [Группа],
    LESSON as [Пара],
    isnull([Понедельник], N'нет') as [Понедельник], 
    isnull([Вторник], N'нет') as [Вторник], 
    isnull([Среда], N'нет') as [Среда], 
    isnull([Четверг], N'нет') as [Четверг], 
    isnull([Пятница], N'нет') as [Пятница], 
    isnull([Суббота], N'нет') as [Суббота]
from 
    (
        select IDGROUP, LESSON, DAYSOFWEEK, SUBJECT 
        from TIMETABLE
    ) as BaseData


pivot 
(
    max(SUBJECT)
    for DAYSOFWEEK
    in ([Понедельник], [Вторник], [Среда], [Четверг], [Пятница], [Суббота])
) as PivotTable;

go

select * from Расписание order by [Группа], [Пара];