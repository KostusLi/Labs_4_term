using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.Database
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string sql = @"
                    SELECT u.Id, u.Username, u.Password, r.Name as RoleName 
                    FROM Users u 
                    JOIN Roles r ON u.RoleId = r.Id 
                    WHERE u.Username = @User AND u.Password = @Pass";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = (Guid)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Role = (string)reader["RoleName"] == "Admin" ? Role.Admin : Role.Client
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string sql = "SELECT COUNT(1) FROM Users WHERE Username = @User";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        public async Task AddUserAsync(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                int roleId = 2;
                using (SqlCommand cmdRole = new SqlCommand("SELECT Id FROM Roles WHERE Name = 'Client'", conn))
                {
                    var res = await cmdRole.ExecuteScalarAsync();
                    if (res != null) roleId = (int)res;
                }

                string sql = "INSERT INTO Users (Id, Username, Password, RoleId) VALUES (@Id, @User, @Pass, @RoleId)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@User", user.Username);
                    cmd.Parameters.AddWithValue("@Pass", user.Password);
                    cmd.Parameters.AddWithValue("@RoleId", roleId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string sql = "UPDATE Users SET Username = @User, Password = @Pass WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@User", user.Username);
                    cmd.Parameters.AddWithValue("@Pass", user.Password);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}