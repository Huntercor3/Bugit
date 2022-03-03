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

        public static async void AddUser(User u)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.Users (FirstName, LastName, email, PhoneNumber, Hardware, Role) values ("
                    + u.firstName + ", " + u.lastName + ", " + u.email + ", " + u.phoneNumber + ", " + u.hardware + ", " + u.role.roleId.ToString() + "," + u.password + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<Project>> GetProjectsForUser(int userId)
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Projects JOIN dbo.ProjectUsers WHERE UserId=" + userId.ToString() + " AND dbo.Projects.ProjectId = dbo.ProjectUsers.ProjectId";

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
