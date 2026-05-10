use UNIVER;
go

declare @faculty nchar(10), 
        @pulpit nchar(20), 
        @teach_count int, 
        @subject nchar(10);

declare @prev_faculty nchar(10) = '';
declare @prev_pulpit nchar(20) = '';
declare @subj_list nvarchar(max) = '';

declare report_cursor cursor local static for
select 
    F.FACULTY,
    P.PULPIT,
    isnull((select count(*) from TEACHER T where T.PULPIT = P.PULPIT), 0),
    SUB.SUBJECT
from FACULTY F
left outer join PULPIT P on F.FACULTY = P.FACULTY
left outer join SUBJECT SUB on P.PULPIT = SUB.PULPIT
order by F.FACULTY, P.PULPIT;

open report_cursor;
fetch next from report_cursor into @faculty, @pulpit, @teach_count, @subject;

while @@fetch_status = 0
begin
    if @faculty <> @prev_faculty
    begin
        if @prev_pulpit <> ''
        begin
            if len(@subj_list) > 0
                print N'        Дисциплины: ' + substring(@subj_list, 1, len(@subj_list) - 1) + N'.'
            else
                print N'        Дисциплины: нет.'
        end

        print N'Факультет: ' + rtrim(@faculty);
        set @prev_faculty = @faculty;
        set @prev_pulpit = ''; 
    end

    if @pulpit <> @prev_pulpit or @prev_pulpit = ''
    begin
        if @prev_pulpit <> '' and @pulpit <> @prev_pulpit
        begin
            if len(@subj_list) > 0
                print N'        Дисциплины: ' + substring(@subj_list, 1, len(@subj_list) - 1) + N'.'
            else
                print N'        Дисциплины: нет.'
        end

        if @pulpit is not null
        begin
            print N'    Кафедра: ' + rtrim(@pulpit);
            print N'        Количество преподавателей: ' + cast(@teach_count as varchar);
        end

        set @prev_pulpit = isnull(@pulpit, '');
        set @subj_list = '';
    end

    if @subject is not null
    begin
        set @subj_list = @subj_list + rtrim(@subject) + N', ';
    end

    fetch next from report_cursor into @faculty, @pulpit, @teach_count, @subject;
end

if @prev_pulpit <> ''
begin
    if len(@subj_list) > 0
        print N'        Дисциплины: ' + substring(@subj_list, 1, len(@subj_list) - 1) + N'.'
    else
        print N'        Дисциплины: нет.'
end

close report_cursor;
deallocate report_cursor;
go