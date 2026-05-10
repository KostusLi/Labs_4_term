use X_MyBase;
go

declare @equip_name nvarchar(30), 
        @result_string nvarchar(max) = N'';

declare equip_cursor cursor local 
for select [Название оборудования] from Оборудование where [Тип оборудования] = N'Машина';

open equip_cursor;

fetch equip_cursor into @equip_name;
print N'--- Машины на заводе ---';
while @@fetch_status = 0
begin
    set @result_string = @result_string + rtrim(@equip_name) + N', ';
    fetch equip_cursor into @equip_name;
end;

print @result_string;

close equip_cursor;
deallocate equip_cursor;
go


go
declare @emp_name nvarchar(20);

declare emp_scroll_cursor cursor local scroll 
for select Фамилия from Сотрудники;

open emp_scroll_cursor;

print N'--- Навигация по сотрудникам ---';

fetch first from emp_scroll_cursor into @emp_name;
print N'Первый в списке: ' + @emp_name;

fetch last from emp_scroll_cursor into @emp_name;
print N'Последний в списке: ' + @emp_name;

fetch absolute 2 from emp_scroll_cursor into @emp_name;
print N'Второй в списке: ' + @emp_name;

fetch prior from emp_scroll_cursor into @emp_name;
print N'Шаг назад: ' + @emp_name;

close emp_scroll_cursor;
deallocate emp_scroll_cursor;
go


go
select * into #TempEquip from Оборудование;

declare @qty tinyint;
declare @equip_id tinyint;

declare update_cursor cursor local 
for select [Оборудования ID], Количество from #TempEquip for update;

open update_cursor;
fetch from update_cursor into @equip_id, @qty;

while @@fetch_status = 0
begin
    if @qty < 3
    begin
        update #TempEquip 
        set Количество = Количество + 5 
        where current of update_cursor;
        
        print N'Дозаказали оборудование ID: ' + cast(@equip_id as nvarchar);
    end

    fetch from update_cursor into @equip_id, @qty;
end;

close update_cursor;
deallocate update_cursor;

select * from #TempEquip;
drop table #TempEquip;
go

use X_MyBase;
go

select * into #TempWriteoffs from Списания;

declare @writeoff_date datetime;
declare @writeoff_id tinyint;
declare delete_cursor cursor local 
for select [ID Списания], [Дата списания] from #TempWriteoffs for update;

open delete_cursor;

fetch from delete_cursor into @writeoff_id, @writeoff_date;

while @@fetch_status = 0
begin
    if year(@writeoff_date) = 2022
    begin
        delete #TempWriteoffs where current of delete_cursor;
        
        print N'Удален старый акт списания ID: ' + cast(@writeoff_id as nvarchar);
    end

    fetch from delete_cursor into @writeoff_id, @writeoff_date;
end;

close delete_cursor;
deallocate delete_cursor;

print N'--- Оставшиеся акты списания (без 2022 года) ---';
select * from #TempWriteoffs;

drop table #TempWriteoffs;
go