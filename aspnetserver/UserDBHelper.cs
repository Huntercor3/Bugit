using Microsoft.Data.SqlClient;
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
            };
        }

        public static int AddUser(User u)
        {
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                String sql = "INSERT INTO dbo.Users (FirstName, LastName, email, PhoneNumber, Hardware, Role, Password)" +
                    " OUTPUT INSERTED.UserId" +
                    " values ('"
                    + u.firstName + "', '" + u.lastName + "', '" + u.email + "', '" + u.phoneNumber + "', '" + u.hardware + "', '" + u.role.roleId.ToString() + "', '" + u.password + "')";

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
                            Project p = new Project((int)record[0], (string)record[1]);
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
                String sql = "SELECT FirstName, LastName FROM dbo.Users WHERE UserId=" + userId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using(MySqlDataReader reader = command.ExecuteReader())
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
    }
}
