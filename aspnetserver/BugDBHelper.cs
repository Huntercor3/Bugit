using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver
{
    public static class BugDBHelper
    {
        private static SqlConnectionStringBuilder builder;

        static BugDBHelper()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        public static async Task<int> AddBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime) " +
                    "OUTPUT INSERTED.BugId " +
                    "values ("
                    + b.Creator + ", " + b.TimeCreated.ToString() + ", " + b.Description + ", " + b.Type + ", " + b.Status + ", " + b.Priority + ", " + b.EstimatedTime + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public static async Task<List<string>> GetCommentsForBug(int bugId)
        {
            List<String> comments = new List<String>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.BugComments WHERE BugId=" + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            comments.Add((string)record[0]);
                        }
                    }
                }
            }
            return comments;
        }

        public static async void AddCommentToBug(int bugId, string comment)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.BugComments (BugId, Comment) " +
                    "values ("
                    + bugId.ToString() + ", " + comment + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void AddBugWithProject(Bug b, int projectId)
        {
            int bugId = await AddBug(b);
            ProjectDBHelper.AddBugToProject(projectId, bugId);
        }
    }
}
