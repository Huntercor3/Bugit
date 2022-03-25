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
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime, Archived) " +
                    "OUTPUT INSERTED.BugId " +
                    "values ("
                    + b.Creator + ", '" + b.TimeCreated + "', '" + b.Description + "', '" + b.Type + "', '" + b.Status + "', '" + b.Priority + "', '" + b.EstimatedTime + "', " + b.Archived.ToString() + ")";

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
                String sql = "SELECT * FROM dbo.BugComments WHERE BugId='" + bugId.ToString() + "'";

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
                    "values ('"
                    + bugId.ToString() + "', '" + comment + "')";

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
            await ProjectDBHelper.AddBugToProject(projectId, bugId);
        }

        public static async Task<int> UpdateBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "UPDATE dbo.Bugs " +
                    "SET Creator = " + b.Creator.ToString() +
                    ", TimeCreated = '" + b.TimeCreated.ToString() +
                    "', Description = '" + b.Description.ToString() +
                    "', Type = '" + b.Type.ToString() +
                    "', Status = '" + b.Status.ToString() +
                    "', Priority = '" + b.Priority.ToString() +
                    "', EstimatedTime = '" + b.EstimatedTime.ToString() +
                    "' WHERE BugId = " + b.BugId;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                     return (int)await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void DeleteBug(int bugId)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "DELETE FROM dbo.Bugs WHERE BugId=" + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }

                String sqlTwo = "DELETE FROM dbo.ProjectBugs WHERE BugId=" + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sqlTwo, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }

                String sqlThree = "DELETE FROM dbo.BugComments WHERE BugId=" + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sqlTwo, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void ArchiveBug(int bugId, bool archive)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                int arch = 1;
                if (!archive)
                {
                    arch = 0;
                }
                String sql = "UPDATE dbo.Bugs" +
                    " SET Archived = " + arch.ToString() +
                    " WHERE ProjectId = " + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
