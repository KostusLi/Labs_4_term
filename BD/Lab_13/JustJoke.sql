use UNIVER;

--3
go
create procedure CountStudent 
as
begin
declare @c int;
set @c = (select count(S.NAME)
    from FACULTY as F inner join GROUPS as G
    on F.FACULTY = G.FACULTY inner join STUDENT as S
    on G.IDGROUP = S.IDGROUP inner join PROGRESS as P
    on S.IDSTUDENT = P.IDSTUDENT
where F.FACULTY=N'ЛХ');

    while @c>0
    begin
        print N'HUI';
        set @c=@c-1;
    end
end;
go

exec CountStudent;

drop procedure CountStudent;