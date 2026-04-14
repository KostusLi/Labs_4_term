--1
declare @ch char = 'a',
	@vch varchar,
	@d datetime = getdate(),
	@t time,
	@si smallint,
	@ti tinyint,
	@n numeric(12, 5);

set @si = 5;
select @d = convert(time, GETDATE());

select @d 'Datetime';

print 'Datetime = ' + cast(@d as varchar(15));


--2
go
use UNIVER;
declare @capacity int = (select cast(sum(AUDITORIUM_CAPACITY) as int) from AUDITORIUM);

if @capacity>200 
begin
	declare @countAudit int = (select Count(*) as [Количество аудиторий] from AUDITORIUM);
	declare @avgcapacity int = (select avg(AUDITORIUM.AUDITORIUM_CAPACITY) as [Средняя вместимость] from AUDITORIUM);
	declare @countlessavg int = (select count(*) from AUDITORIUM where AUDITORIUM_CAPACITY<@avgcapacity);
	declare @percentaudit int = (@countlessavg/@countAudit)*100;
	print @countAudit;
	print @avgcapacity;
	print @countlessavg;
	print @percentaudit;
end
else if @capacity<200
	print 'Вместимость = '+cast(@capacity as varchar(7));
go

--3
go
use UNIVER;
select * from PULPIT;

print N'обработано строк последним запросом: ' + cast(@@rowcount as varchar);
print N'версия sql server: ' + @@version;
print N'id текущего процесса (spid): ' + cast(@@spid as varchar);
print N'Последняя ошибка (error): ' + cast(@@error as varchar);
print N'Имя сервера (servername): ' + cast(@@servername as varchar);
print N'Уровень вложенности транзакции (trancount): ' + cast(@@trancount as varchar);
print N'Проверка результата считывания (fetch_status): ' + cast(@@fetch_status as varchar);
print N'Уровень вложенности процедуры (nestlevel): ' + cast(@@nestlevel as varchar);

print N'округление: ' + cast(round(123.456, 2) as varchar);
print N'отбрасывание дроби (нижнее целое): ' + cast(floor(123.456) as varchar);
print N'возведение в степень (2 в кубе): ' + cast(power(2.0, 3) as varchar);
print N'логарифм: ' + cast(log(10.0) as varchar);
print N'квадратный корень из 16: ' + cast(sqrt(16.0) as varchar);
print N'экспонента: ' + cast(exp(2.0) as varchar);
print N'модуль отрицательного числа (-15): ' + cast(abs(-15) as varchar);


--4
go
use UNIVER;

declare @x int = 3, @t int = 5;
declare @z int = 0;
if(@t>@x) set @z = SIN(@t)*SIN(@t);
if(@t<@x) set @z = 4*(@t+@x);
else set @z = 1 - exp(@x-2);
print N'z = ' + cast(@z as varchar(10));


select top 10
	NAME as [Full name],
	substring(NAME, 1, charindex(' ', NAME))+
	substring(NAME, charindex(' ', NAME)+1, 1)+ '. ' +
	substring(NAME, charindex(' ', NAME, CHARINDEX(' ', NAME)+1)+1, 1)+'.' as [Abrivate name]
from STUDENT
go

select 
    name as [студент],
    bday as [дата рождения],
    (year(getdate()) - year(bday)) as [исполняется лет]
from student
where month(bday) = month(dateadd(month, 1, getdate()));

select top 1
    s.name, 
    g.idgroup as [группа], 
    p.subject as [предмет],
    datename(weekday, p.pdate) as [день недели экзамена]
from progress p
inner join student s on p.idstudent = s.idstudent
inner join groups g on s.idgroup = g.idgroup
where p.subject = N'БД';

--5
go
use UNIVER;

declare @bad_grades int = (
    select count(*) from progress p 
    inner join student s on p.idstudent = s.idstudent
    inner join groups g on s.idgroup = g.idgroup
    where g.faculty = N'ЛХ' and p.note < 4
);

if @bad_grades > 0
    print N'на факультете ЛХ есть задолженности: ' + cast(@bad_grades as varchar) + ' шт.';
else
    print N'на факультете ЛХ все сдали экзамены без двоек!';

go

--6
go
select 
    s.name as [студент],
    p.subject as [дисциплина],
    p.note as [оценка],
    case 
        when p.note >= 9 then N'отлично'
        when p.note >= 6 then N'хорошо'
        when p.note >= 4 then N'удовлетворительно'
        else N'не сдал'
    end as [результат прописью]
from progress p
inner join student s on p.idstudent = s.idstudent
inner join groups g on s.idgroup = g.idgroup
where g.faculty = N'ЛХ';
go

--7
go
use UNIVER;

create table #temp_univer 
(
    id int, 
    random_code varchar(20)
);

declare @i int = 1;
while @i <= 10
begin
    insert into #temp_univer (id, random_code)
    values (@i, N'код-' + cast(floor(rand() * 1000) as varchar));
    
    set @i = @i + 1;
end;

select * from #temp_univer;
drop table #temp_univer;


declare @students_count int = (select count(*) from student);

if @students_count > 0
begin
    print N'студенты в базе есть. завершаю работу скрипта досрочно через return.';
    return;
end;

print N'эта строка никогда не выведется, потому что скрипт убит командой return';

go


--9
go
use UNIVER;

begin try
    print N'пытаемся вставить факультет ИТ, который уже существует...';
    
    insert into faculty (faculty, faculty_name) 
    values (N'ИТ', N'дубликат');
    
    print N'запрос выполнен успешно';
end try
begin catch
    print N'--- ПЕРЕХВАТ ОШИБКИ БАЗЫ ДАННЫХ ---';
    print N'код ошибки (error_number): ' + cast(error_number() as varchar);
    print N'сообщение (error_message): ' + error_message();
    print N'строка (error_line): ' + cast(error_line() as varchar);
    print N'уровень серьезности (error_severity): ' + cast(error_severity() as varchar);
    print N'метка состояния (error_state): ' + cast(error_state() as varchar);
end catch;

go