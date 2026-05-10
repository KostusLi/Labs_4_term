use X_MyBase;
go

select 
    [Название оборудования], 
    Количество 
from Оборудование 
where [Тип оборудования] = N'Машина';


create nonclustered index idx_equip_type_cover 
on Оборудование([Тип оборудования]) 
include ([Название оборудования], Количество);

select 
    [Название оборудования], 
    Количество 
from Оборудование with (index(idx_equip_type_cover))
where [Тип оборудования] = N'Машина';
go


select 
    [ID Списания], 
    [Дата списания] 
from Списания 
where [Причина списания] = N'Поломка';


create nonclustered index idx_writeoff_broken 
on Списания([Причина списания]) 
where [Причина списания] = N'Поломка';


select 
    [ID Списания], 
    [Дата списания] 
from Списания with (index(idx_writeoff_broken))
where [Причина списания] = N'Поломка';
go