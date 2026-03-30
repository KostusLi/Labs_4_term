use UNIVER

CREATE TABLE TIMETABLE
(
	ID_TIMETABLE int identity(1, 1) primary key,
	IDGROUP int foreign key references GROUPS(IDGROUP),
	AUDITORIUM_NAME nvarchar(20) foreign key references AUDITORIUM(AUDITORIUM),
	SUBJECT nchar(10) foreign key references SUBJECT(SUBJECT),
	TEACHER nchar(10) foreign key references TEACHER(TEACHER),
	DAYSOFWEEK nvarchar(20) default N'Понедельник' check (DAYSOFWEEK IN (N'Понедельник', N'Вторник', N'Среда', N'Четверг', N'Пятница', N'Суббота')),
	LESSON int default 2 check (LESSON between 1 and 4)
);

