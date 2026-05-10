use UNIVER;

--1
declare @c char = 'g';
set implicit_transactions on
insert into AUDITORIUM_TYPE(AUDITORIUM_TYPE, AUDITORIUM_TYPENAME)
values(N'ЛК-Б', N'Борцовский зал');
update AUDITORIUM_TYPE set AUDITORIUM_TYPENAME = N'bambambam' where AUDITORIUM_TYPENAME = N'Борцовский зал';
if @c='g' commit
else rollback
set implicit_transactions off

--2
begin try
	begin tran
		delete AUDITORIUM_TYPE where AUDITORIUM_TYPENAME = N'bambambam';
		insert AUDITORIUM_TYPE values(N'ЛК-Б', N'Борцовский зал');
		insert AUDITORIUM_TYPE values(N'ЛК-Б', N'Борцовский зал');
	commit tran
end try
begin catch
	print 'Error: ' + cast(error_number() as varchar(5));
	if @@TRANCOUNT>0 rollback tran;
end catch

--3
declare @point varchar(6);
begin try
	begin tran
		delete AUDITORIUM_TYPE where AUDITORIUM_TYPENAME = N'Борцовский зал';
		set @point = 'p1'; save tran @point;
		insert AUDITORIUM_TYPE values(N'ЛК-Б', N'Борцовский зал');
		set @point = 'p2'; save tran @point;
		insert AUDITORIUM_TYPE values(N'ЛК-Б', N'Борцовский зал');
end try
begin catch
	print 'Error: ' + cast(error_number() as varchar(5));
	if @@TRANCOUNT>0
	begin
		print 'End point: ' + @point;
		rollback tran @point;
		commit tran;
	end
end catch
