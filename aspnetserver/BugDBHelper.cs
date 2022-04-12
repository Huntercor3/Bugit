using Microsoft.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace aspnetserver
{
    public static class BugDBHelper
    {
        private static MySqlConnectionStringBuilder builder;

        // Old DB Connection
        /* static BugDBHelper()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        } */

        static BugDBHelper()
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

        public static async Task<int> AddBug(Bug b)
        {
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime, Archived) " +
                    "values ("
                    + b.Creator + ", '" + b.TimeCreated + "', '" + b.Description + "', '" + b.Type + "', '" + b.Status + "', '" + b.Priority + "', '" + b.EstimatedTime + "', " + b.Archived.ToString() + ")";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteScalar();
                    return 0;
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

        public static async void UpdateBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "UPDATE dbo.Bugs" +
                    ", Creator = " + b.Creator.ToString() +
                    ", TimeCreated = '" + b.TimeCreated +
                    "', Description = '" + b.Description +
                    "', Type = '" + b.Type +
                    "', Status = '" + b.Status +
                    "', Priority = '" + b.Priority +
                    "', Archived = " + b.Archived.ToString() +
                    " WHERE BugId = " + b.BugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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