using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.Database
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                string sql = @"
                    SELECT p.*, c.Name as CategoryName 
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(MapReaderToProduct(reader));
                        }
                    }
                }
            }
            return products;
        }

        public async Task AddProductAsync(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        int categoryId = await GetOrAddCategoryAsync(conn, tx, product.Category);

                        string sql = @"
                        INSERT INTO Products (Id, Title, Description, CategoryId, Price, Discount, StockQuantity, Rating, ImageData) 
                        VALUES (@Id, @Title, @Desc, @CatId, @Price, @Discount, @Stock, @Rating, @ImageData)";

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Id", product.Id);
                            cmd.Parameters.AddWithValue("@Title", product.Title ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Desc", product.Description ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CatId", categoryId);
                            cmd.Parameters.AddWithValue("@Price", product.Price);
                            cmd.Parameters.AddWithValue("@Discount", product.Discount);
                            cmd.Parameters.AddWithValue("@Stock", product.StockQuantity);
                            cmd.Parameters.AddWithValue("@Rating", product.Rating);

                            SqlParameter imageParam = new SqlParameter("@ImageData", SqlDbType.VarBinary, -1);
                            imageParam.Value = product.ImageData != null ? (object)product.ImageData : DBNull.Value;
                            cmd.Parameters.Add(imageParam);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        int categoryId = await GetOrAddCategoryAsync(conn, tx, product.Category);

                        string sql = @"
    UPDATE Products 
    SET Title = @Title, Description = @Desc, CategoryId = @CatId, 
        Price = @Price, Discount = @Discount, StockQuantity = @Stock, Rating = @Rating, ImageData = @ImageData
    WHERE Id = @Id";

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Id", product.Id);
                            cmd.Parameters.AddWithValue("@Title", product.Title ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Desc", product.Description ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@CatId", categoryId);
                            cmd.Parameters.AddWithValue("@Price", product.Price);
                            cmd.Parameters.AddWithValue("@Discount", product.Discount);
                            cmd.Parameters.AddWithValue("@Stock", product.StockQuantity);
                            cmd.Parameters.AddWithValue("@Rating", product.Rating);

                            SqlParameter imageParam = new SqlParameter("@ImageData", SqlDbType.VarBinary, -1);
                            imageParam.Value = product.ImageData != null ? (object)product.ImageData : DBNull.Value;
                            cmd.Parameters.Add(imageParam);

                            await cmd.ExecuteNonQueryAsync();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string sql = "DELETE FROM Products WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", productId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_SearchProducts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm ?? "");

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(MapReaderToProduct(reader));
                        }
                    }
                }
            }
            return products;
        }

        private async Task<int> GetOrAddCategoryAsync(SqlConnection conn, SqlTransaction tx, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) categoryName = "Без категории";

            string selectSql = "SELECT Id FROM Categories WHERE Name = @Name";
            using (SqlCommand cmd = new SqlCommand(selectSql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Name", categoryName);
                var result = await cmd.ExecuteScalarAsync();
                if (result != null) return (int)result;
            }

            string insertSql = "INSERT INTO Categories (Name) OUTPUT INSERTED.Id VALUES (@Name)";
            using (SqlCommand cmd = new SqlCommand(insertSql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Name", categoryName);
                return (int)await cmd.ExecuteScalarAsync();
            }
        }

        private Product MapReaderToProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = (Guid)reader["Id"],
                Title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : null,
                Description = reader["Description"] != DBNull.Value ? (string)reader["Description"] : null,
                Category = reader["CategoryName"] != DBNull.Value ? (string)reader["CategoryName"] : "Без категории",
                Price = (decimal)reader["Price"],
                Discount = (double)reader["Discount"],
                StockQuantity = (int)reader["StockQuantity"],
                Rating = reader["Rating"] != DBNull.Value ? (int)reader["Rating"] : 0,
                ImageData = reader["ImageData"] != DBNull.Value ? (byte[])reader["ImageData"] : null
            };
        }
    }
}