using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp1.ShopApp.Database
{
    public static class DatabaseInitializer
    {
        public static bool Initialize()
        {
            try
            {
                string masterConnectionString = ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString;
                string dbName = "ShopDB2";

                using (SqlConnection connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();

                    string checkDbQuery = $"SELECT database_id FROM sys.databases WHERE Name = '{dbName}'";
                    using (SqlCommand checkCmd = new SqlCommand(checkDbQuery, connection))
                    {
                        var result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            string createDbQuery = $"CREATE DATABASE {dbName}";
                            using (SqlCommand createCmd = new SqlCommand(createDbQuery, connection))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                            System.Threading.Thread.Sleep(1000);

                            CreateTablesAndInfrastructure();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка БД. Программа будет закрыта.\nДетали: {ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private static void CreateTablesAndInfrastructure()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string script = @"
                    -- 1. Таблица РОЛЕЙ
                    CREATE TABLE Roles (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL
                    );

                    -- 2. Таблица КАТЕГОРИЙ
                    CREATE TABLE Categories (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100) NOT NULL
                    );

                    -- 3. Таблица ПОЛЬЗОВАТЕЛЕЙ (Связь с Roles)
                    CREATE TABLE Users (
                        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                        Username NVARCHAR(50) NOT NULL UNIQUE,
                        Password NVARCHAR(50) NOT NULL,
                        RoleId INT FOREIGN KEY REFERENCES Roles(Id)
                    );

                    -- 4. Таблица ТОВАРОВ (Связь с Categories)
                    CREATE TABLE Products (
                        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                        Title NVARCHAR(100) NOT NULL,
                        Description NVARCHAR(MAX),
                        CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
                        Price DECIMAL(18,2) NOT NULL,
                        Discount FLOAT NOT NULL DEFAULT 0,
                        StockQuantity INT NOT NULL,

                        Rating INT NOT NULL DEFAULT 0,                     

                        -- Поле для графической информации (Требование П.2)
                        ImageData VARBINARY(MAX) NULL 
                    );

                    -- ЗАПОЛНЯЕМ БАЗОВЫЕ ДАННЫЕ
                    INSERT INTO Roles (Name) VALUES ('Admin'), ('Client');
                    
                    -- Вставляем твоего стартового админа (RoleId = 1 это Admin)
                    INSERT INTO Users (Username, Password, RoleId) 
                    VALUES ('Admin', 'sih', 1);

                    INSERT INTO Categories (Name) VALUES ('Техника'), ('Еда'), ('Одежда');

                    -- СОЗДАНИЕ ТРИГГЕРА (Требование П.2)
                    -- Триггер не даст сохранить товар с отрицательной ценой или остатком
                    EXEC('
                    CREATE TRIGGER trg_CheckProductData
                    ON Products
                    AFTER INSERT, UPDATE
                    AS
                    BEGIN
                        IF EXISTS (SELECT * FROM inserted WHERE Price < 0 OR StockQuantity < 0)
                        BEGIN
                            RAISERROR (''Ошибка: Цена и остаток не могут быть отрицательными!'', 16, 1);
                            ROLLBACK TRANSACTION;
                        END
                    END');

                    -- СОЗДАНИЕ ХРАНИМОЙ ПРОЦЕДУРЫ (Требование П.2)
                    -- Процедура для поиска товаров
                    EXEC('
                    CREATE PROCEDURE sp_SearchProducts
                        @SearchTerm NVARCHAR(100)
                    AS
                    BEGIN
                        SELECT p.*, c.Name as CategoryName 
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.Id
                        WHERE p.Title LIKE ''%'' + @SearchTerm + ''%''
                    END');
                ";

                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}