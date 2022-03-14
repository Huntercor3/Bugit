using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver
{
    public static class UserDBHelper
    {
        private static SqlConnectionStringBuilder builder;

        static UserDBHelper()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        public static async Task<int> AddUser(User u)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.Users (FirstName, LastName, email, PhoneNumber, Hardware, Role)" +
                    "OUTPUT INSERTED.UserId" +
                    " values ('"
                    + u.firstName + "', '" + u.lastName + "', '" + u.email + "', '" + u.phoneNumber + "', '" + u.hardware + "', '" + u.role.roleId.ToString() + "', '" + u.password + "')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public static async Task<List<Project>> GetProjectsForUser(int userId)
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Projects JOIN dbo.ProjectUsers ON dbo.Projects.ProjectId = dbo.ProjectUsers.ProjectId WHERE dbo.ProjectUsers.userId=" + userId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
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
    }
}
