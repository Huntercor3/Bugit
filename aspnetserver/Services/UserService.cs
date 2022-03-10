using aspnetserver.Models;

using aspnetserver;
using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver.Services
{
    public class UserService : IUserService
    {
        private static SqlConnectionStringBuilder builder;

        // Establishes connection to the database
        static UserService()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        // Checks the database for our user entered credentials
        // This will be updated to have edge case checking to see if the user doesn't exist
        // or if the password isn't correct. This will also utilize password encryption down
        // the road.
        public UserAuth CheckUserInDBO(LoginModel userLogin)
        {
            // Creates new user and bool for if the user exists or not.
            UserAuth user = new UserAuth();
            bool check = true;

            // Runs the connection to the dbo
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                // SQL command
                String sql = "SELECT email, Role FROM Users WHERE email='" + userLogin.EmailAddress + "' AND Password='" + userLogin.Password + "';";

                SqlCommand cmd = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.HasRows && reader.Read())
                    {
                        user.EmailAddress = reader["email"].ToString();
                        user.Role = reader["Role"].ToString();
                    }
                    if (!reader.HasRows)
                        check = false;
                }
                catch (SqlException ex)
                { throw; }
                finally
                { connection.Close(); }
            }
            // If check is true, a user is returned for Program.cs to know what happened.
            if (check)
                return user;
            // Else returns null for Program.cs to say user was not found.
            else
                return null;
        }
    }
}