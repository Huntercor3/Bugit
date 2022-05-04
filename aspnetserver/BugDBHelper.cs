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

<<<<<<< HEAD
=======
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

>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c
        public static async Task<int> AddBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
<<<<<<< HEAD
<<<<<<< HEAD
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime) " +
                    "OUTPUT INSERTED.BugId " +
                    "values ("
                    + b.Creator + ", " + b.TimeCreated.ToString() + ", " + b.Description + ", " + b.Type + ", " + b.Status + ", " + b.Priority + ", " + b.EstimatedTime + ")";
=======
                String sql = "INSERT INTO dbo.Bugs (Software, Creator, TimeCreated) " +
                    "OUTPUT INSERTED.BugId" +
                    "values ('"
                    + b.software + "', '" + b.creator + "', '" + b.timeCreated.ToString() + "')";
>>>>>>> origin/EndpointsRemastered
=======
                connection.Open();
                String sql = "INSERT INTO dbo.Bugs (Creator, TimeCreated, Description, Type, Status, Priority, EstimatedTime) " +
                    "values ("
                    + b.Creator + ", '" + b.TimeCreated + "', '" + b.Description + "', '" + b.Type + "', '" + b.Status + "', '" + b.Priority + "', '" + b.EstimatedTime + "')";
>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c

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
<<<<<<< HEAD
            await ProjectDBHelper.AddBugToProject(projectId, bugId);
=======
            ProjectDBHelper.AddBugToProject(projectId, bugId);
>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c
        }

        public static async void UpdateBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
<<<<<<< HEAD
                String sql = "UPDATE dbo.Bugs" + 
                    "SET Software = " + b.software +
                    ", Creator = " + b.creator.ToString() +
                    ", TimeCreated = " + b.timeCreated.ToString() +
                    ", Description = " + b.description +
                    ", Type = " + b.type.ToString() +
                    ", Status = " + b.status.ToString() +
                    ", Priority = " + b.priority.ToString() +
                    " WHERE BugId = " + b.bugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
=======
                String sql = "UPDATE dbo.Bugs" +
                " SET Creator = " + b.Creator.ToString() +
                ", Description = '" + b.Description +
                "', Type = '" + b.Type.ToString() +
                "', Status = '" + b.Status.ToString() +
                "', Priority = '" + b.Priority.ToString() +
                "', EstimatedTime = '" + b.EstimatedTime +
                "' WHERE BugId = " + b.BugId.ToString();

                using (MySqlCommand command = new MySqlCommand(sql, connection))
>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async void DeleteBug(int bugId)
        {
<<<<<<< HEAD
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
=======
            List<Bug> bugs = new List<Bug>();
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c
            {
                String sql = "DELETE FROM dbo.Bugs WHERE BugId=" + bugId.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
<<<<<<< HEAD
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
=======
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (int)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], (string)record[6], (string)record[7]);
                            bugs.Add(b);
                        }
                    }
>>>>>>> e61fd9a3e09cfafcc982ca26d732fe1318241e2c
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
