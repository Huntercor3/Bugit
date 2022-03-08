using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver
{
    public static class ProjectDBHelper
    {
        private static SqlConnectionStringBuilder builder;

        static ProjectDBHelper()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        public static async Task<int> AddNewProject(string projectName)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.Projects (ProjectName)" +
                    "OUTPUT INSERTED.ProjectId" +
                    " values ("
                    + projectName + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public static async void AddUserToProject(int projectId, User u)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.ProjectUsers (ProjectId, UserId) values ("
                + projectId + ", " + u.userId.ToString() + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void AddBugToProject(int projectId, int bugId)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.ProjectBugs (ProjectId, BugId) values ("
                + projectId + ", " + bugId.ToString() + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<User>> GetUsersInProject(int projectId)
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users JOIN dbo.ProjectUsers WHERE ProjectId=" + projectId.ToString() + " AND dbo.Users.UserId = dbo.ProjectUsers.UserId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
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
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs JOIN dbo.ProjectBugs WHERE ProjectId=" + projectId.ToString() + " AND dbo.Bugs.BugId = dbo.ProjectUsers.BugId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (string)record[1], (int)record[2], (DateTime)record[3], new Category((int)record[6]), (string)record[7], (int)record[8], (int)record[9], (int)record[10]);
                            bugs.Add(b);
                        }
                    }
                }
            }
            return bugs;
        }
    }
}