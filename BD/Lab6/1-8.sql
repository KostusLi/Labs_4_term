use UNIVER;

--1
select AUDITORIUM.AUDITORIUM_TYPE,
	MAX(AUDITORIUM.AUDITORIUM_CAPACITY)[Максимальная вместимость],
	MIN(AUDITORIUM.AUDITORIUM_CAPACITY)[Минимальная вместимость],
	AVG(AUDITORIUM.AUDITORIUM_CAPACITY) [Средняя вместиомть],
	SUM(AUDITORIUM.AUDITORIUM_CAPACITY) [Суммарная вместимость],
	COUNT(AUDITORIUM) [Общее количество]
from AUDITORIUM inner join AUDITORIUM_TYPE
on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE
group by AUDITORIUM.AUDITORIUM_TYPE


select *
from (select case when PROGRESS.NOTE between 1 and 5 then N'оценка <=5'
		when PROGRESS.NOTE between 5 and 7 then N'оценка от 5 до 7'
		else N'оценка больше 7'
		end [Пределы оценок], COUNT(*)[Количество]
	from PROGRESS group by case
		when PROGRESS.NOTE between 1 and 5 then N'оценка <5'
		when PROGRESS.NOTE between 5 and 7 then N'оценка от 5 до 7'
		else N'оценка больше 7'
		end) AS T
order by case [Пределы оценок]
	when N'оценка <5' then 3
	when N'оценка от 5 до 7' then 2
	when N'оценка больше 7' then 1
	else 0
	end

--4
SELECT 
    F.FACULTY_NAME AS [Факультет],
    G.PROFESSION AS [Специальность],
    (YEAR(GETDATE()) - G.YEAR_FIRST) AS [Курс],
    ROUND(AVG(CAST(PR.NOTE AS FLOAT)), 2) AS [Средняя оценка]
FROM 
    PROGRESS AS PR
INNER JOIN STUDENT AS S ON PR.IDSTUDENT = S.IDSTUDENT
INNER JOIN GROUPS AS G ON S.IDGROUP = G.IDGROUP
INNER JOIN FACULTY AS F ON G.FACULTY = F.FACULTY
GROUP BY 
    F.FACULTY_NAME, 
    G.PROFESSION, 
    (YEAR(GETDATE()) - G.YEAR_FIRST)
ORDER BY 
    [Средняя оценка] DESC;


--5
SELECT 
    F.FACULTY_NAME AS [Факультет],
    G.PROFESSION AS [Специальность],
    (YEAR(GETDATE()) - G.YEAR_FIRST) AS [Курс],
    ROUND(AVG(CAST(PR.NOTE AS FLOAT)), 2) AS [Средняя оценка]
FROM 
    PROGRESS AS PR
INNER JOIN STUDENT AS S ON PR.IDSTUDENT = S.IDSTUDENT
INNER JOIN GROUPS AS G ON S.IDGROUP = G.IDGROUP
INNER JOIN FACULTY AS F ON G.FACULTY = F.FACULTY
WHERE PR.SUBJECT IN (N'БД', N'ОАиП')
GROUP BY 
    F.FACULTY_NAME, 
    G.PROFESSION, 
    (YEAR(GETDATE()) - G.YEAR_FIRST)
ORDER BY 
    [Средняя оценка] DESC;


--6
SELECT 
    G.PROFESSION AS [Специальность],
    PR.SUBJECT AS [Дисциплина],
    AVG(PR.NOTE) as [Средняя оценка]
FROM 
    PROGRESS AS PR
INNER JOIN STUDENT AS S ON PR.IDSTUDENT = S.IDSTUDENT
INNER JOIN GROUPS AS G ON S.IDGROUP = G.IDGROUP
INNER JOIN FACULTY AS F ON G.FACULTY = F.FACULTY
where F.FACULTY = N'ТОВ'
group by G.PROFESSION, PR.SUBJECT

--7
select PR.SUBJECT, COUNT(*) as [Количество студентов]
from PROGRESS AS PR
group by SUBJECT, NOTE
having NOTE IN (8, 9)
order by [Количество студентов]