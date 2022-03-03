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

        public static async void AddBug(Bug b)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "INSERT INTO dbo.Bugs (Software, Creator, TimeCreated) values ("
                + b.software + ", " + b.creator + ", " + b.timeCreated.ToString() + ")";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
