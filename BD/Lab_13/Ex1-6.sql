USE UNIVER;

--1
go
create procedure PSUBJECTS
as
begin
	select SUBJECT.SUBJECT as N'код',
		SUBJECT.SUBJECT_NAME as N'дисциплина',
		SUBJECT.PULPIT as N'кафедра'
	from SUBJECT;
	return;
end;
go

--2
go
go
ALTER procedure PSUBJECTS @p nchar(20), @c int output
as
begin
	select SUBJECT.SUBJECT as N'код',
		SUBJECT.SUBJECT_NAME as N'дисциплина',
		SUBJECT.PULPIT as N'кафедра'
	from SUBJECT
	where SUBJECT.PULPIT = @p;
	set @c = @@ROWCOUNT;
	
	declare @total int = (select count(*) from subject);
    return @total;
end;
go

declare @k int;
declare @r int, @p nchar(20);
exec @k=PSUBJECTS @p=N'ИСиТ', @c=@r output;
print N'Кол-во предметов' + cast(@r as varchar(4))
go

--3
go
create table #SUBJECT
(
	SUBJECT nchar(10),
	SUBJECT_NAME nvarchar(100),
	PULPIT nchar(20)
)

go
ALTER procedure PSUBJECTS @p nchar(20)
as
begin
	select SUBJECT.SUBJECT as N'код',
		SUBJECT.SUBJECT_NAME as N'дисциплина',
		SUBJECT.PULPIT as N'кафедра'
	from SUBJECT
	where SUBJECT.PULPIT = @p;
	return;
end;
go

declare @p nchar(20);
insert #SUBJECT exec PSUBJECTS @p=N'ЭТиМ'
select * from #SUBJECT;
go

--4
go
go
create procedure PAUDITORIUM_INSERT @a nvarchar(20), @n nvarchar(50), @c int, @t nvarchar(10)
as begin
	begin try
		insert into AUDITORIUM values (@a, @n, @t, @c);
		return 1;
	end try
	begin catch
		print N'Номер ошибки: ' + cast(error_number() as varchar(14));
		print N'Серьзеность ошибки: ' + cast(error_severity() as varchar(14));
		print N'Текст ошибки: ' + error_message();
		return -1;
	end catch
end;
go

declare @a nvarchar(20), @n nvarchar(50), @c int, @t nvarchar(10);

exec PAUDITORIUM_INSERT @a = N'221-1', @n=N'221-1', @c=110, @t=N'ЛБ-К';
exec PAUDITORIUM_INSERT @a = N'220-1', @n=N'220-1', @c=110, @t=N'ЛК-А';
go

--5
go
create procedure subject_report
    @p nchar(10)
as
begin
    declare @subject_list nvarchar(max) = N'';
    declare @sub nchar(10);
    declare @counter int = 0;

    if not exists (select * from pulpit where pulpit = @p)
    begin
        raiserror(N'ошибка в параметрах: такой кафедры не существует!', 11, 1);
        return -1;
    end

    declare cur cursor local for select subject from subject where pulpit = @p;
    open cur;
    fetch from cur into @sub;

    while @@fetch_status = 0
    begin
        set @subject_list = @subject_list + rtrim(@sub) + N', ';
        set @counter = @counter + 1;
        fetch from cur into @sub;
    end
    close cur;
    deallocate cur;

    print N'Дисциплины кафедры ' + rtrim(@p) + N': ' + @subject_list;

    return @counter;
end;
go

declare @rc int;
begin try
    exec @rc = subject_report @p = N'ИСиТ';
    print N'возвращено дисциплин: ' + cast(@rc as varchar);
    exec @rc = subject_report @p = N'НЛО';
end try
begin catch
    print N'перехвачена ошибка: ';
    print error_message();
end catch;

--6
go

if object_id('pauditorium_insertx', 'p') is not null drop procedure pauditorium_insertx;
go

create procedure pauditorium_insertx
    @a varchar(20),
    @n varchar(50),
    @c int,
    @t varchar(10),
    @tn varchar(50)
as
begin
    declare @rc int = 1;

    begin try
        set transaction isolation level serializable;
        begin tran;

        insert into auditorium_type (auditorium_type, auditorium_typename)
        values (@t, @tn);

        exec @rc = pauditorium_insert @a, @n, @c, @t;

        if @rc = -1
        begin
            raiserror(N'ошибка во вложенной процедуре', 11, 1);
        end

        commit tran;
        return 1;
    end try
    begin catch
        print N'номер ошибки: ' + cast(error_number() as varchar);
        print N'сообщение: ' + error_message();

        if @@trancount > 0 rollback tran;

        return -1;
    end catch
end;
go

declare @res int;
exec @res = pauditorium_insertx @a = N'888-8', @n = N'Супер-Аудитория', @c = 100, @t = N'СУПЕР', @tn = N'Супер класс';
print N'результат сложной вставки: ' + cast(@res as varchar);

delete from auditorium where auditorium = N'888-8';
delete from auditorium_type where auditorium_type = N'СУПЕР';
go