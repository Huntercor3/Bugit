using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver
{
    public static class Login
    {
        private static SqlConnectionStringBuilder builder;

        static Login()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        public static async Task<LoginResponse> LoginAsync(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users WHERE email=" + email;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        await reader.ReadAsync();
                        IDataRecord record = (IDataRecord)reader;
                        if (record == null)
                        {
                            return new LoginResponse(false, "User does not exist.");
                        }
                        else if((string)record[7] != password)
                        {
                            return new LoginResponse(false, "Password is not valid.");
                        }
                        User u = new User((int)record[0], (string)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], new Role((int)record[6]), (string)record[7]);
                        return new LoginResponse(true, "Logged in successfully.", u);
                    }
                }
            }
        }
    }
}
