using Microsoft.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace aspnetserver
{
    public static class ProjectDBHelper
    {
        private static MySqlConnectionStringBuilder builder;

        static ProjectDBHelper()
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

        public static int AddNewProject(string projectName)
        {
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                String sql = "INSERT INTO dbo.Projects (ProjectName)" +
                    " values ('"
                    + projectName + "')";

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

        public static void UpdateProject(Project p)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "UPDATE dbo.Projects" +
                    " SET ProjectName = '" + p.projectName +
                    "', Archived = " + p.Archived.ToString() +
                    " WHERE ProjectId = " + p.projectId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddUserToProject(int projectId, User u)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.ProjectUsers (ProjectId, UserId) values ("
                + projectId.ToString() + ", " + u.userId.ToString() + ")";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddBugToProject(int projectId, int bugId)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.ProjectBugs (ProjectId, BugId) values ("
                + projectId.ToString() + ", " + bugId.ToString() + ")";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static async Task<List<User>> GetUsersInProject(int projectId)
        {
            List<User> users = new List<User>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users JOIN dbo.ProjectUsers ON dbo.Users.UserId = dbo.ProjectUsers.UserId WHERE dbo.ProjectBugs.ProjectId=" + projectId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            User u = new User((int)record[0], (string)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], new Role((int)record[6]), (string)record[7]);
                            users.Add(u);
                        }
                    }
                }
            }
            return users;
        }

        public static async Task<List<Bug>> GetBugsInProject(int projectId)
        {
            List<Bug> bugs = new List<Bug>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs JOIN dbo.ProjectBugs ON dbo.Bugs.BugId = dbo.ProjectBugs.BugId WHERE dbo.ProjectBugs.ProjectId=" + projectId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (int)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], (string)record[6], (string)record[7], (int)record[8]);
                            bugs.Add(b);
                        }
                    }
                }
            }
            return bugs;
        }

        public static async void DeleteProject(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "DELETE FROM dbo.Projects WHERE BugId=" + projectId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }

                String sqlTwo = "DELETE FROM dbo.ProjectBugs WHERE ProjectId=" + projectId.ToString();

                using (SqlCommand command = new SqlCommand(sqlTwo, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }

                String sqlThree = "DELETE FROM dbo.ProjectUsers WHERE ProjectId=" + projectId.ToString();

                using (SqlCommand command = new SqlCommand(sqlTwo, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void ArchiveProject(int projectId, bool archive)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                int arch = 1;
                if (!archive)
                {
                    arch = 0;
                }
                String sql = "UPDATE dbo.Projects" +
                    " SET Archived = " + arch.ToString() +
                    " WHERE ProjectId = " + projectId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}