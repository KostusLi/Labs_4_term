use UNIVER;

--1.1
drop function dbo.COUNT_STUDENTS;

go
create function COUNT_STUDENTS(@faculty varchar(20)) returns int 
as
begin
declare @c int;
set @c = (select count(S.NAME)
    from FACULTY as F inner join GROUPS as G
    on F.FACULTY = G.FACULTY inner join STUDENT as S
    on G.IDGROUP = S.IDGROUP
where F.FACULTY=@faculty);
return @c
end;
go



--1.2
go
alter function COUNT_STUDENTS(@faculty varchar(20), @prof varchar(20)) returns int 
as
begin
declare @c int;
set @c = (select count(S.NAME)
    from FACULTY as F inner join GROUPS as G
    on F.FACULTY = G.FACULTY inner join STUDENT as S
    on G.IDGROUP = S.IDGROUP
where F.FACULTY=@faculty and G.PROFESSION=@prof);
return @c
end;
go

--2
drop function dbo.FSUBJECTS;

go
create function FSUBJECTS(@p nvarchar(20)) returns nvarchar(300)
as begin
declare @c nvarchar(300) = N'';
declare @temp nvarchar(30);
declare discipline cursor local
for select SUBJECT.SUBJECT from SUBJECT where SUBJECT.PULPIT=@p;
open discipline;
fetch discipline into @temp
while @@FETCH_STATUS=0
begin
set @c = @c+@temp;
fetch discipline into @temp;
end;
return @c;
end;
go

select PULPIT, dbo.FSUBJECTS(PULPIT) as [Дисциплины] from PULPIT;

--3
drop function ffacpul;

go
create function ffacpul(@fac varchar(20), @pul varchar(20)) 
returns table
as
return (
    select f.faculty, p.pulpit
    from faculty f left outer join pulpit p on f.faculty = p.faculty
    where f.faculty = isnull(@fac, f.faculty)
      and p.pulpit = isnull(@pul, p.pulpit)
);
go

select * from dbo.ffacpul(null, null);
select * from dbo.ffacpul(N'ИДиП', null);
select * from dbo.ffacpul(null, N'ИСиТ');
select * from dbo.ffacpul(N'ИТ', N'ИСиТ');

--4
drop function dbo.FCTEACHER;

go
create function FCTEACHER(@p nchar(20)) returns int
as
begin

    declare @c int;
    if @p is null
    begin
        set @c = (select count(TEACHER.TEACHER)
            from TEACHER);
        return @c;
    end
    
    set @c = (select count(TEACHER.TEACHER)
        from TEACHER
        where TEACHER.PULPIT = @p);

    return @c;

end;
go

select PULPIT, dbo.FCTEACHER(PULPIT)
from PULPIT;

select dbo.FCTEACHER(NULL) as [Всего преподавателей];


--5
go
create function count_pulpits(@f varchar(20)) returns int
as
begin
    return (select count(*) from pulpit where faculty = @f);
end;
go

create function count_groups(@f varchar(20)) returns int
as
begin
    return (select count(*) from groups where faculty = @f);
end;
go

create function count_professions(@f varchar(20)) returns int
as
begin
    return (select count(*) from profession where faculty = @f);
end;
go

go

if object_id('faculty_report', 'tf') is not null drop function faculty_report;
go

create function faculty_report(@c int) 
returns @fr table ( 
    [Факультет] varchar(50), 
    [Количество кафедр] int, 
    [Количество групп] int, 
    [Количество студентов] int, 
    [Количество специальностей] int 
)
as 
begin 
    declare cc cursor static for 
    select faculty from faculty 
    where dbo.count_students(faculty, default) > @c; 
    
    declare @f varchar(30);
    open cc;  
    fetch cc into @f;
    
    while @@fetch_status = 0
    begin
        insert into @fr values( 
            @f,  
            dbo.count_pulpits(@f),
            dbo.count_groups(@f),
            dbo.count_students(@f, default),
            dbo.count_professions(@f)
        ); 
        
        fetch cc into @f;  
    end;   
    
    close cc; 
    deallocate cc;
    
    return; 
end;
go

select * from dbo.faculty_report(0);
go


--6
go

if object_id('print_reportx', 'p') is not null drop procedure print_reportx;
go

create procedure print_reportx
    @f nchar(10) = null,
    @p nchar(10) = null
as
begin
    set nocount on;

    if @f is null and @p is not null
    begin
        select @f = faculty from pulpit where pulpit = @p;
        
        if @f is null
        begin
            raiserror(N'Ошибка в параметрах: Кафедра не найдена!', 11, 1);
            return -1;
        end
    end

    declare @cur_faculty nchar(10), 
            @cur_pulpit nchar(20), 
            @teach_count int, 
            @subj_list nchar(300);

    declare @prev_faculty nchar(10) = '';
    declare @pulpit_counter int = 0;

    declare report_cursor cursor local static for
    select 
        faculty, 
        pulpit, 
        dbo.fcteacher(pulpit),
        dbo.fsubjects(pulpit)
    from dbo.ffacpul(@f, @p)
    order by faculty, pulpit;

    open report_cursor;
    fetch next from report_cursor into @cur_faculty, @cur_pulpit, @teach_count, @subj_list;

    while @@fetch_status = 0
    begin
        if @cur_faculty <> @prev_faculty
        begin
            print N'Факультет: ' + rtrim(@cur_faculty);
            set @prev_faculty = @cur_faculty;
        end

        if @cur_pulpit is not null
        begin
            print N'Кафедра: ' + rtrim(@cur_pulpit);
            
            print N'Количество преподавателей: ' + cast(isnull(@teach_count, 0) as varchar);
            
            if len(isnull(@subj_list, '')) > 0
                print N'Дисциплины: ' + rtrim(@subj_list) + N'.'
            else
                print N'Дисциплины: нет.'

            set @pulpit_counter = @pulpit_counter + 1;
        end

        fetch next from report_cursor into @cur_faculty, @cur_pulpit, @teach_count, @subj_list;
    end

    close report_cursor;
    deallocate report_cursor;

    return @pulpit_counter;
end;
go


declare @rc1 int, @rc2 int;

print N'Старая процедура:';
exec @rc1 = print_report @f = N'ИЭФ';

print N'';

print N'Новая процедура:';
exec @rc2 = print_reportx @f = N'ИЭФ';

print N'Итог:';
if @rc1 = @rc2
    print N'Одинаково: ' + cast(@rc1 as varchar);
else 
    print N'Не одинаково';
go