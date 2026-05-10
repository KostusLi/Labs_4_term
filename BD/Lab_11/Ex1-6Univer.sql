
use UNIVER;

--1
declare @temp char(20), @t char(300) = ' ';
declare subjects cursor
	for select SUBJECT.SUBJECT_NAME from SUBJECT where SUBJECT.PULPIT = N'ИСиТ';

open subjects;
fetch subjects into @temp;
print N'Предметы кафедры ИСиТ';
while @@fetch_status = 0
	begin
		set @t = rtrim(@temp) + ', ' + @t;
		fetch subjects into @temp;
	end;
	print @t;
	close subjects

--2
declare subjects1 cursor local
	for select SUBJECT.SUBJECT_NAME from SUBJECT where SUBJECT.PULPIT = N'ИСиТ';
declare @temp2 char(40)
open subjects1;
fetch subjects1 into @temp2;
print '1.' + @temp2;

go
declare @temp2 char(40)
fetch subjects1 into @temp2;
print '2.' + @temp2;
go

declare subjects2 cursor global
	for select SUBJECT.SUBJECT_NAME from SUBJECT where SUBJECT.PULPIT = N'ИСиТ';
declare @temp3 char(40)
open subjects2;
fetch subjects2 into @temp3;
print '1.' + @temp3;

go
declare @temp3 char(40)
fetch subjects2 into @temp3;
print '2.' + @temp3;
go
deallocate subjects2;

close subjects2

--3
go
declare @nameshort char(10);
declare faculty cursor local static for select FACULTY.FACULTY from FACULTY;
open faculty
print N'Кол-во строк: ' + cast(@@cursor_rows as varchar(5));
insert FACULTY (FACULTY, FACULTY_NAME) values (N'РТ', N'Робототехника');
print N'Кол-во строк: ' + cast(@@cursor_rows as varchar(5));
close faculty;
go

go
declare @nameshort char(4), @t char(60) = '';
declare faculty1 cursor local dynamic for select FACULTY.FACULTY from FACULTY;
open faculty1
print N'Кол-во строк: ' + cast(@@cursor_rows as varchar(5));
insert FACULTY (FACULTY, FACULTY_NAME) values (N'hr', N'Green-hight)');
fetch faculty1 into @nameshort;
while @@FETCH_STATUS=0
begin
set @t = @nameshort + ', ' + @t;
fetch faculty1 into @nameshort;
end;
print @t;
close faculty1;
go

--4
go
declare @nameshort char(5);
declare faculty1 cursor local static scroll for select FACULTY.FACULTY from FACULTY;
open faculty1
fetch first from faculty1 into @nameshort
print 'frist word: ' + @nameshort;
fetch absolute 5 from faculty1 into @nameshort
print 'five word after start: ' + @nameshort
close faculty1
go

--5
go
declare @nameshort char(5);
declare faculty1 cursor local dynamic for select FACULTY.FACULTY from FACULTY for update;
open faculty1
fetch from faculty1 into @nameshort
delete FACULTY where current of faculty1
fetch from faculty1 into @nameshort
update FACULTY set FACULTY_NAME = 'Bob' where current of faculty1
close faculty1
go

--6
--6.1
go
use UNIVER;

declare @sub nchar(10), @id int, @dat date, @note int;
declare goodprogress cursor local for select PROGRESS.NOTE from PROGRESS for update;
open goodprogress
fetch from goodprogress into @note
while @@FETCH_STATUS = 0
begin
	if @note<4
	begin
	delete PROGRESS where current of goodprogress;
	end
fetch from goodprogress into @note
end;
close goodprogress
go

--6.2
go
use UNIVER;

declare @sub nchar(10), @id int, @dat date, @note int;
declare goodprogress cursor local for select PROGRESS.NOTE, PROGRESS.IDSTUDENT from PROGRESS for update;
open goodprogress
fetch from goodprogress into @note, @id
while @@FETCH_STATUS=0
begin
	if @id = 1000
	begin
	update PROGRESS set NOTE = NOTE + 1	where current of goodprogress;
	end
fetch from goodprogress into @note, @id
end;
close goodprogress;
go