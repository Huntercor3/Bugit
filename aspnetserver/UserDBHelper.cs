using System.Data;
using MySql.Data.MySqlClient;

namespace aspnetserver
{
    public static class UserDBHelper
    {
        private static MySqlConnectionStringBuilder builder;

        static UserDBHelper()
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = "34.67.3.72",
                UserID = "root",
                Password = "CSBS@2201"
                // This is for if we remove `dbo.` in our functions
                //Database = "dbo"
            };
        }

        public static int AddUser(User u)
        {
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                String sql = "INSERT INTO dbo.Users (FirstName, LastName, email, PhoneNumber, Hardware, Role, Password)" +
                    " values ('"
                    + u.firstName + "', '" + u.lastName + "', '" + u.email + "', '" + u.phoneNumber + "', '" + u.hardware + "', '" + u.role.roleId.ToString() + "', '" + Encryption.Encrypt(u.password) + "')";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteScalar();
                }

                String sql2 = "SELECT LAST_INSERT_ID();";
                using (var command2 = new MySqlCommand(sql2, connection))
                {
                    String id = command2.ExecuteScalar().ToString();
                    return int.Parse(id);
                }
            }
        }

        public static async void UpdateUser(User u)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "UPDATE dbo.Users" +
                    "SET FirstName = '" + u.firstName +
                    "', Lastname = '" + u.lastName +
                    "', email = '" + u.email +
                    "', PhoneNumber = '" + u.phoneNumber +
                    "', Hardware = '" + u.hardware +
                    "', Role = " + u.role.roleId.ToString() +
                    ", Password = '" + u.password +
                    "' WHERE UserId = " + u.userId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<Project>> GetProjectsForUser(int userId)
        {
            List<Project> projects = new List<Project>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Projects JOIN dbo.ProjectUsers ON dbo.Projects.ProjectId = dbo.ProjectUsers.ProjectId WHERE dbo.ProjectUsers.userId=" + userId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Project p = new Project((int)record[0], (string)record[1], (int)record[2]);
                            projects.Add(p);
                        }
                    }
                }
            }
            return projects;
        }

        public static async Task<String> GetUserName(int userId)
        {
            String name = "";
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users WHERE UserId=" + userId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }

                String sqlTwo = "DELETE FROM dbo.ProjectUsers WHERE UserId=" + userId.ToString();

                using (MySqlCommand command = new MySqlCommand(sqlTwo, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            name = record[0] + " " + record[1];
                        }
                    }
                }
            }
            return name;
        }

        public static async Task<int> GetUserId(string firstName, string lastName)
        {
            int userId = 0;
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users WHERE FirstName='" + firstName + "' AND LastName='" + lastName + "';";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            userId = (int)reader[0];
                        }
                    }
                }
            }
            return userId;
        }
    }
}