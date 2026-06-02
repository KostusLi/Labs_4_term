USE UNIVER;
GO

IF OBJECT_ID('PRINT_REPORT', 'P') IS NOT NULL DROP PROCEDURE PRINT_REPORT;
GO

CREATE PROCEDURE PRINT_REPORT
    @f CHAR(10) = NULL,
    @p CHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @f IS NULL AND @p IS NOT NULL
    BEGIN
        SELECT @f = FACULTY FROM PULPIT WHERE PULPIT = @p;
        
        IF @f IS NULL
        BEGIN
            RAISERROR(N'Ошибка в параметрах: Кафедра не найдена!', 11, 1);
            RETURN -1;
        END
    END

    DECLARE @cur_faculty NCHAR(10), 
            @cur_pulpit NCHAR(20), 
            @teach_count INT, 
            @subject NCHAR(10);

    DECLARE @prev_faculty NCHAR(10) = '';
    DECLARE @prev_pulpit NCHAR(20) = '';
    DECLARE @subj_list NVARCHAR(MAX) = '';
    DECLARE @pulpit_counter INT = 0;

    DECLARE report_cursor CURSOR LOCAL STATIC FOR
    SELECT 
        F.FACULTY,
        P.PULPIT,
        ISNULL((SELECT COUNT(*) FROM TEACHER T WHERE T.PULPIT = P.PULPIT), 0),
        SUB.SUBJECT
    FROM FACULTY F
    LEFT JOIN PULPIT P ON F.FACULTY = P.FACULTY
    LEFT JOIN SUBJECT SUB ON P.PULPIT = SUB.PULPIT
    WHERE 
        (@f IS NULL OR F.FACULTY = @f)
        AND 
        (@p IS NULL OR P.PULPIT = @p)
    ORDER BY F.FACULTY, P.PULPIT;

    OPEN report_cursor;
    FETCH NEXT FROM report_cursor INTO @cur_faculty, @cur_pulpit, @teach_count, @subject;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @cur_faculty <> @prev_faculty
        BEGIN
            IF @prev_pulpit <> ''
            BEGIN
                IF LEN(@subj_list) > 0
                    PRINT N'        Дисциплины: ' + SUBSTRING(@subj_list, 1, LEN(@subj_list) - 1) + N'.'
                ELSE
                    PRINT N'        Дисциплины: нет.'
            END

            PRINT N'Факультет: ' + RTRIM(@cur_faculty);
            SET @prev_faculty = @cur_faculty;
            SET @prev_pulpit = ''; 
        END

        IF @cur_pulpit <> @prev_pulpit OR @prev_pulpit = ''
        BEGIN
            IF @prev_pulpit <> '' AND @cur_pulpit <> @prev_pulpit
            BEGIN
                IF LEN(@subj_list) > 0
                    PRINT N'        Дисциплины: ' + SUBSTRING(@subj_list, 1, LEN(@subj_list) - 1) + N'.'
                ELSE
                    PRINT N'        Дисциплины: нет.'
            END

            IF @cur_pulpit IS NOT NULL
            BEGIN
                PRINT N'    Кафедра: ' + RTRIM(@cur_pulpit);
                PRINT N'        Количество преподавателей: ' + CAST(@teach_count AS VARCHAR);
                SET @pulpit_counter = @pulpit_counter + 1;
            END

            SET @prev_pulpit = ISNULL(@cur_pulpit, '');
            SET @subj_list = ''; 
        END

        IF @subject IS NOT NULL
        BEGIN
            SET @subj_list = @subj_list + RTRIM(@subject) + N', ';
        END

        FETCH NEXT FROM report_cursor INTO @cur_faculty, @cur_pulpit, @teach_count, @subject;
    END

    IF @prev_pulpit <> ''
    BEGIN
        IF LEN(@subj_list) > 0
            PRINT N'Дисциплины: ' + SUBSTRING(@subj_list, 1, LEN(@subj_list) - 1) + N'.'
        ELSE
            PRINT N'Дисциплины: нет.'
    END

    CLOSE report_cursor;
    DEALLOCATE report_cursor;

    RETURN @pulpit_counter;
END;
GO


DECLARE @rc INT;

PRINT N'ТЕСТ 1: ИДиП';
EXEC @rc = PRINT_REPORT @f = N'ИДиП';
PRINT N'[Количество выведенных кафедр: ' + CAST(@rc AS VARCHAR) + ']';

PRINT N'ТЕСТ 2: ИСиТ';
EXEC @rc = PRINT_REPORT @p = N'ИСиТ';
PRINT N'[Количество выведенных кафедр: ' + CAST(@rc AS VARCHAR) + ']';

PRINT N'ТЕСТ 3: TRY/CATCH';
BEGIN TRY
    EXEC @rc = PRINT_REPORT @p = N'ХЗ';
END TRY
BEGIN CATCH
    PRINT N'ОШИБКА ПЕРЕХВАЧЕНА В БЛОКЕ CATCH!';
    PRINT ERROR_MESSAGE();
END CATCH;
GO