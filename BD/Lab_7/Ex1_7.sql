use UNIVER;

--1
select F.FACULTY, G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(P.NOTE AS FLOAT)), 2) [Средняя оценка]
from FACULTY as F inner join GROUPS as G
ON F.FACULTY = G.FACULTY inner join STUDENT As S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS AS P
ON S.IDSTUDENT = P.IDSTUDENT
where F.FACULTY = N'ЛХ'
group by rollup (F.FACULTY, G.PROFESSION, P.SUBJECT)

--2
select F.FACULTY, G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(P.NOTE AS FLOAT)), 2) [Средняя оценка]
from FACULTY as F inner join GROUPS as G
ON F.FACULTY = G.FACULTY inner join STUDENT As S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS AS P
ON S.IDSTUDENT = P.IDSTUDENT
where F.FACULTY = N'ЛХ'
group by cube (F.FACULTY, G.PROFESSION, P.SUBJECT)

--3.0
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ЛХ'
Group by G.PROFESSION, P.SUBJECT


select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ИЭФ'
Group by G.PROFESSION, P.SUBJECT

--3.1
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ЛХ'
Group by G.PROFESSION, P.SUBJECT
union all
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ИЭФ'
Group by G.PROFESSION, P.SUBJECT

--3.2
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ЛХ'
Group by G.PROFESSION, P.SUBJECT
union
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ИЭФ'
Group by G.PROFESSION, P.SUBJECT

--4
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ЛХ'
Group by G.PROFESSION, P.SUBJECT
intersect
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ИЭФ'
Group by G.PROFESSION, P.SUBJECT

--5
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ЛХ'
Group by G.PROFESSION, P.SUBJECT
except
select G.PROFESSION, P.SUBJECT, ROUND(AVG(CAST(p.NOTE as float)), 2) [Средняя оценка]
from GROUPS as G inner join STUDENT as S
ON G.IDGROUP = S.IDGROUP inner join PROGRESS as P
ON S.IDSTUDENT = P.IDSTUDENT
where G.FACULTY = N'ИЭФ'
Group by G.PROFESSION, P.SUBJECT

--7.1
select 
    case when grouping(G.FACULTY) = 1 then N'ИТОГО ПО УНИВЕРСИТЕТУ' 
         else G.FACULTY 
    end as [Факультет],

    case when grouping(G.IDGROUP) = 1 and grouping(G.FACULTY) = 0 then N'Итого по факультету' 
         else cast(G.IDGROUP as nvarchar) 
    end as [Группа],

    count(S.IDSTUDENT) as [Количество студентов]
from 
    GROUPS as G
left join 
    STUDENT as S on G.IDGROUP = S.IDGROUP
group by 
    rollup (G.FACULTY, G.IDGROUP);


--7.2
select 
    isnull(T.AUDITORIUM_TYPENAME, N'ВСЕГО ПО УНИВЕРСИТЕТУ') as [Тип аудитории],
    
    count(A.AUDITORIUM) as [Количество аудиторий],
    sum(A.AUDITORIUM_CAPACITY) as [Суммарная вместимость]
from 
    AUDITORIUM as A
inner join 
    AUDITORIUM_TYPE as T on A.AUDITORIUM_TYPE = T.AUDITORIUM_TYPE
group by 
    rollup (T.AUDITORIUM_TYPENAME);