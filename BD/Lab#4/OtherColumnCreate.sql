USE UNIVER;
GO

IF OBJECT_ID('PROGRESS', 'U') IS NOT NULL DROP TABLE PROGRESS;
IF OBJECT_ID('STUDENT', 'U') IS NOT NULL DROP TABLE STUDENT;
IF OBJECT_ID('GROUPS', 'U') IS NOT NULL DROP TABLE GROUPS;
IF OBJECT_ID('SUBJECT', 'U') IS NOT NULL DROP TABLE SUBJECT;
IF OBJECT_ID('TEACHER', 'U') IS NOT NULL DROP TABLE TEACHER;
IF OBJECT_ID('PULPIT', 'U') IS NOT NULL DROP TABLE PULPIT;
IF OBJECT_ID('PROFESSION', 'U') IS NOT NULL DROP TABLE PROFESSION;
IF OBJECT_ID('FACULTY', 'U') IS NOT NULL DROP TABLE FACULTY;
GO

CREATE TABLE FACULTY 
( 
    FACULTY nchar(10) CONSTRAINT PK_FACULTY PRIMARY KEY,
    FACULTY_NAME nvarchar(50) DEFAULT N'???'
);

INSERT INTO FACULTY (FACULTY, FACULTY_NAME) VALUES 
(N'ТТЛП', N'Технологии и техника лесной промышленности'),
(N'ТОВ', N'Технологии органических веществ'),
(N'ХТиТ', N'Химические технологии и техника'),
(N'ИЭФ', N'Инженерно-экономический'),
(N'ЛХ', N'Лесохозяйственный'),
(N'ИДиП', N'Издательское дело и полиграфия'),
(N'ИТ', N'Информационных технологий');
GO

CREATE TABLE PROFESSION 
( 
    PROFESSION nchar(20) CONSTRAINT PK_PROFESSION PRIMARY KEY,
    FACULTY nchar(10) CONSTRAINT FK_PROFESSION_FACULTY FOREIGN KEY REFERENCES FACULTY(FACULTY),
    PROFESSION_NAME nvarchar(100), 
    QUALIFICATION nvarchar(50)
); 

INSERT INTO PROFESSION(PROFESSION, FACULTY, PROFESSION_NAME, QUALIFICATION) VALUES 
(N'1-36 06 01', N'ИДиП', N'Полиграфическое оборудование и системы', N'инженер-электромеханик'),
(N'1-36 07 01', N'ХТиТ', N'Машины и аппараты химических производств', N'инженер-механик'),
(N'1-40 01 02', N'ИТ', N'Информационные системы и технологии', N'инженер-программист-системотехник'),
(N'1-46 01 01', N'ТТЛП', N'Лесоинженерное дело', N'инженер-технолог'),
(N'1-47 01 01', N'ИДиП', N'Издательское дело', N'редактор-технолог'),
(N'1-48 01 02', N'ТОВ', N'Химическая технология орг. веществ', N'инженер-химик-технолог'),
(N'1-48 01 05', N'ТОВ', N'Химическая технология переработки древесины', N'инженер-химик-технолог'),
(N'1-75 01 01', N'ЛХ', N'Лесное хозяйство', N'инженер лесного хозяйства'),
(N'1-75 02 01', N'ЛХ', N'Садово-парковое строительство', N'инженер садово-паркового стр.'),
(N'1-89 02 02', N'ЛХ', N'Туризм и природопользование', N'специалист в сфере туризма'),
(N'1-25 01 07', N'ИЭФ', N'Экономика и управление на предприятии', N'экономист-менеджер'),
(N'1-25 01 08', N'ИЭФ', N'Бухгалтерский учет, анализ и аудит', N'экономист'),
(N'1-36 05 01', N'ТТЛП', N'Машины и оборудование лесного комплекса', N'инженер-механик');
GO

CREATE TABLE PULPIT 
( 
    PULPIT nchar(20) CONSTRAINT PK_PULPIT PRIMARY KEY,
    PULPIT_NAME nvarchar(100), 
    FACULTY nchar(10) CONSTRAINT FK_PULPIT_FACULTY FOREIGN KEY REFERENCES FACULTY(FACULTY)
);

INSERT INTO PULPIT (PULPIT, PULPIT_NAME, FACULTY) VALUES 
(N'ИСиТ', N'Информационных систем и технологий', N'ИТ'),
(N'ЛВ', N'Лесоводства', N'ЛХ'),
(N'ЛУ', N'Лесоустройства', N'ЛХ'),
(N'ЛЗиДВ', N'Лесозащиты и древесиноведения', N'ЛХ'),
(N'ТЛ', N'Транспорта леса', N'ТТЛП'),
(N'ЛМиЛЗ', N'Лесных машин и технологии лесозаготовок', N'ТТЛП'),
(N'ТДП', N'Технологий деревообрабатывающих производств', N'ТТЛП'),
(N'ОХ', N'Органической химии', N'ТОВ'),
(N'ХПД', N'Химической переработки древесины', N'ТОВ'),
(N'ТНХСиППМ', N'Технологии нефтехимического синтеза', N'ТОВ'),
(N'ЭТиМ', N'Экономической теории и маркетинга', N'ИЭФ'),
(N'ПОиСОИ', N'Полиграфического оборудования', N'ИДиП'),
(N'МиЭП', N'Менеджмента и экономики природопользования', N'ИЭФ');
GO

CREATE TABLE TEACHER
( 
    TEACHER nchar(10) CONSTRAINT PK_TEACHER PRIMARY KEY,
    TEACHER_NAME nvarchar(100), 
    GENDER nchar(1) CONSTRAINT CHK_GENDER CHECK (GENDER IN (N'м', N'ж')),
    PULPIT nchar(20) CONSTRAINT FK_TEACHER_PULPIT FOREIGN KEY REFERENCES PULPIT(PULPIT)
);

INSERT INTO TEACHER (TEACHER, TEACHER_NAME, GENDER, PULPIT) VALUES 
(N'СМЛВ', N'Смелов Владимир Владиславович', N'м', N'ИСиТ'),
(N'УРБ', N'Урбанович Павел Павлович', N'м', N'ИСиТ'),
(N'ГРН', N'Гурин Николай Иванович', N'м', N'ИСиТ'),
(N'ЖЛК', N'Жиляк Надежда Александровна', N'ж', N'ИСиТ'),
(N'МРЗ', N'Мороз Елена Станиславовна', N'ж', N'ИСиТ'),
(N'БРТШВЧ', N'Барташевич Святослав Александрович', N'м', N'ПОиСОИ'),
(N'АРС', N'Арсентьев Виталий Арсентьевич', N'м', N'ПОиСОИ'),
(N'НВРВ', N'Неверов Александр Васильевич', N'м', N'МиЭП'),
(N'РВКЧ', N'Ровкач Андрей Иванович', N'м', N'ЛВ'),
(N'ЧРН', N'Чернова Анна Викторовна', N'ж', N'ХПД'),
(N'МХВ', N'Мохов Михаил Сергеевич', N'м', N'ПОиСОИ');
GO

CREATE TABLE SUBJECT
( 
    SUBJECT nchar(10) CONSTRAINT PK_SUBJECT PRIMARY KEY, 
    SUBJECT_NAME nvarchar(100) UNIQUE,
    PULPIT nchar(20) CONSTRAINT FK_SUBJECT_PULPIT FOREIGN KEY REFERENCES PULPIT(PULPIT)
);

INSERT INTO SUBJECT (SUBJECT, SUBJECT_NAME, PULPIT) VALUES 
(N'СУБД', N'Системы управления базами данных', N'ИСиТ'),
(N'БД', N'Базы данных', N'ИСиТ'),
(N'ИНФ', N'Информационные технологии', N'ИСиТ'),
(N'ОАиП', N'Основы алгоритмизации и программирования', N'ИСиТ'),
(N'ПЗ', N'Представление знаний в компьютерных системах', N'ИСиТ'),
(N'ПСП', N'Программирование сетевых приложений', N'ИСиТ'),
(N'ПИС', N'Проектирование информационных систем', N'ИСиТ'),
(N'КГ', N'Компьютерная геометрия', N'ИСиТ'),
(N'ТиОЛ', N'Технология и оборудование лесозаготовок', N'ЛМиЛЗ'),
(N'ТРИ', N'Технология резиновых изделий', N'ТНХСиППМ'),
(N'ЭП', N'Экономика природопользования', N'МиЭП'),
(N'ЭТ', N'Экономическая теория', N'ЭТиМ');
GO

CREATE TABLE GROUPS 
( 
    IDGROUP int IDENTITY(1,1) CONSTRAINT PK_GROUPS PRIMARY KEY, 
    FACULTY nchar(10) CONSTRAINT FK_GROUPS_FACULTY FOREIGN KEY REFERENCES FACULTY(FACULTY), 
    PROFESSION nchar(20) CONSTRAINT FK_GROUPS_PROFESSION FOREIGN KEY REFERENCES PROFESSION(PROFESSION),
    YEAR_FIRST smallint
);

SET IDENTITY_INSERT GROUPS ON;
INSERT INTO GROUPS (IDGROUP, FACULTY, PROFESSION, YEAR_FIRST) VALUES 
(22, N'ЛХ', N'1-75 02 01', 2011),
(23, N'ЛХ', N'1-89 02 02', 2012),
(24, N'ЛХ', N'1-89 02 02', 2011),
(25, N'ТТЛП', N'1-36 05 01', 2013),
(26, N'ТТЛП', N'1-36 05 01', 2012),
(27, N'ТТЛП', N'1-46 01 01', 2012),
(28, N'ИЭФ', N'1-25 01 07', 2013),
(29, N'ИЭФ', N'1-25 01 07', 2012);
SET IDENTITY_INSERT GROUPS OFF;
GO

CREATE TABLE STUDENT 
( 
    IDSTUDENT int IDENTITY(1000,1) CONSTRAINT PK_STUDENT PRIMARY KEY,
    IDGROUP int CONSTRAINT FK_STUDENT_GROUPS FOREIGN KEY REFERENCES GROUPS(IDGROUP), 
    NAME nvarchar(100), 
    BDAY date,
    STAMP timestamp,
    INFO xml,
    FOTO varbinary(max)
);

SET IDENTITY_INSERT STUDENT ON;
INSERT INTO STUDENT (IDSTUDENT, IDGROUP, NAME, BDAY) VALUES 
(1000, 22, N'Пугач Михаил Трофимович', '1996-01-12'),
(1001, 23, N'Авдеев Николай Иванович', '1996-07-19'),
(1002, 24, N'Белова Елена Степановна', '1996-05-22'),
(1003, 25, N'Вилков Андрей Петрович', '1996-12-08'),
(1004, 26, N'Грушин Леонид Николаевич', '1995-11-11'),
(1005, 27, N'Дунаев Дмитрий Михайлович', '1996-08-24'),
(1006, 28, N'Клуни Иван Владиславович', '1996-09-15'),
(1007, 29, N'Крылов Олег Павлович', '1996-10-16'),
(1008, 22, N'Иванов Иван Иванович', '1995-05-05'),
(1010, 23, N'Петров Петр Петрович', '1996-02-28'),
(1013, 24, N'Сидоров Сидор Сидорович', '1995-03-15'),
(1014, 25, N'Алексеев Алексей Алексеевич', '1996-04-10');
SET IDENTITY_INSERT STUDENT OFF;
GO

CREATE TABLE PROGRESS
( 
    SUBJECT nchar(10) CONSTRAINT FK_PROGRESS_SUBJECT FOREIGN KEY REFERENCES SUBJECT(SUBJECT), 
    IDSTUDENT int CONSTRAINT FK_PROGRESS_STUDENT FOREIGN KEY REFERENCES STUDENT(IDSTUDENT), 
    PDATE date, 
    NOTE int CONSTRAINT CHK_NOTE CHECK (NOTE BETWEEN 1 AND 10)
);

INSERT INTO PROGRESS (SUBJECT, IDSTUDENT, PDATE, NOTE) VALUES 
(N'ОАиП', 1000, '2014-01-12', 4),
(N'ОАиП', 1001, '2014-01-19', 5),
(N'ОАиП', 1003, '2014-01-08', 9),
(N'БД', 1008, '2014-01-11', 8),
(N'БД', 1010, '2014-01-15', 4),
(N'СУБД', 1013, '2014-01-16', 7),
(N'СУБД', 1014, '2014-01-27', 6);
GO