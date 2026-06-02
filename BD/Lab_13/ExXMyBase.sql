use X_MyBase;
go


--1
if object_id('report_writeoffs', 'p') is not null drop procedure report_writeoffs;
go

create procedure report_writeoffs
    @dept_id tinyint = null,
    @total_acts int output
as
begin
    set nocount on;
    select 
        p.[Название подразделения] as [Отдел],
        o.[Название оборудования] as [Техника],
        s.[Причина списания] as [Причина],
        s.[Дата списания] as [Дата]
    from Списания s
    join Подразделения p on s.[ID Подразделения] = p.[ID Подразделения]
    join Оборудование o on s.[ID Оборудования] = o.[Оборудования ID]
    where 
        (@dept_id is null or s.[ID Подразделения] = @dept_id)
    order by s.[Дата списания] desc;

    set @total_acts = @@rowcount;

    declare @unique_equip int = (
        select count(distinct [ID Оборудования]) 
        from Списания 
        where (@dept_id is null or [ID Подразделения] = @dept_id)
    );
    
    return @unique_equip;
end;
go

if object_id('do_writeoff', 'p') is not null drop procedure do_writeoff;
go

--2
create procedure do_writeoff
    @equip_id tinyint,
    @dept_id tinyint,
    @emp_id tinyint,
    @reason nvarchar(40)
as
begin
    set nocount on;
    declare @current_qty tinyint;
    declare @new_writeoff_id tinyint;

    begin try
        begin tran;

        select @current_qty = Количество from Оборудование where [Оборудования ID] = @equip_id;

        if @current_qty is null
        begin
            raiserror(N'Ошибка: Оборудование с таким ID не найдено!', 11, 1);
            return -1;
        end

        if @current_qty <= 0
        begin
            raiserror(N'Ошибка: На складе больше нет этого оборудования для списания!', 11, 1);
            return -1;
        end
        update Оборудование 
        set Количество = Количество - 1 
        where [Оборудования ID] = @equip_id;

        select @new_writeoff_id = isnull(max([ID Списания]), 0) + 1 from Списания;

        insert into Списания ([ID Списания], [ID Оборудования], [ID Подразделения], [Причина списания], [Дата списания], [ID сотрудника])
        values (@new_writeoff_id, @equip_id, @dept_id, @reason, getdate(), @emp_id);

        commit tran;
        print N'УСПЕХ: Оборудование списано, акт №' + cast(@new_writeoff_id as nvarchar) + N' создан.';
        return 1;
    end try
    begin catch
        if @@trancount > 0 rollback tran;
        
        print N'--- ПРОИЗОШЛА ОШИБКА ---';
        print error_message();
        return -1;
    end catch
end;
go


declare @acts_count int;
declare @unique_count int;

exec @unique_count = report_writeoffs @dept_id = 3, @total_acts = @acts_count output;

print N'Найдено актов: ' + cast(@acts_count as varchar);
print N'Уникальных единиц техники: ' + cast(@unique_count as varchar);
print N'';

declare @status int;
exec @status = do_writeoff @equip_id = 99, @dept_id = 3, @emp_id = 1, @reason = N'Згарела!';
print N'Статус выполнения: ' + cast(@status as varchar);
print N'';

exec @status = do_writeoff @equip_id = 2, @dept_id = 3, @emp_id = 1, @reason = N'Сломалось что-то';
print N'Статус выполнения: ' + cast(@status as varchar);
go
