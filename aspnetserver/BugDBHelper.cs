using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace aspnetserver
{
    public static class BugDBHelper
    {
        private static MySqlConnectionStringBuilder builder;

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

        public static async Task<List<Bug>> GetAllBugs()
        {
            List<Bug> bugs = new List<Bug>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (int)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], (string)record[6], (string)record[7]);
                            bugs.Add(b);
                        }
                    }
                }
            }
            return bugs;
        }

        public static async Task<Bug> GetBugByID(int _inputID)
        {
            //List<Bug> bugs = new List<Bug>();
            Bug bug = null;
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs WHERE BugID=" + _inputID + ";";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            bug = new Bug((int)record[0], (int)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], (string)record[6], (string)record[7]);
                        }
                    }
                }
            }
            return bug;
        }

        public static async Task<int> AddBug(Bug b)
        {
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime) " +
                    "values ("
                    + b.Creator + ", '" + b.TimeCreated + "', '" + b.Description + "', '" + b.Type + "', '" + b.Status + "', '" + b.Priority + "', '" + b.EstimatedTime + "')";

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

        public static async Task<List<string>> GetCommentsForBug(int bugId)
        {
            List<String> comments = new List<String>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.BugComments WHERE BugId='" + bugId.ToString() + "'";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
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
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.BugComments (BugId, Comment) " +
                    "values ('"
                    + bugId.ToString() + "', '" + comment + "')";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
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

        public static async Task<int> UpdateBug(Bug b)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "UPDATE dbo.Bugs" +
                " SET Creator = " + b.Creator.ToString() +
                ", Description = '" + b.Description +
                "', Type = '" + b.Type.ToString() +
                "', Status = '" + b.Status.ToString() +
                "', Priority = '" + b.Priority.ToString() +
                "', EstimatedTime = '" + b.EstimatedTime +
                "' WHERE BugId = " + b.BugId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    return (int)await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void DeleteBug(int bugId)
        {
            List<Bug> bugs = new List<Bug>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                String sql = "DELETE FROM dbo.Bugs WHERE BugId=" + bugId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (int)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], (string)record[6], (string)record[7]);
                            bugs.Add(b);
                        }
                    }
                }
            }
        }

        /* public static async void ArchiveBug(int bugId, bool archive)
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
        */
    }
}