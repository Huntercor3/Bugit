using aspnetserver.Models;
using MySql.Data.MySqlClient;

namespace aspnetserver.Services
{
    public class UserService : IUserService
    {
        private static MySqlConnectionStringBuilder builder;

        // Establishes connection to the database
        static UserService()
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
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                // SQL command
                string password = Encryption.Encrypt(userLogin.Password);
                String sql = "SELECT email, Role FROM dbo.Users WHERE email='" + userLogin.EmailAddress + "' AND Password='" + password + "';";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.HasRows && reader.Read())
                    {
                        user.EmailAddress = reader["email"].ToString();
                        user.Role = reader["Role"].ToString();
                    }
                    if (!reader.HasRows)
                        check = false;
                }
                catch (MySqlException ex)
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

        public bool CheckUserInDBOBool(string email)
        {
            bool check = false;

            // Runs the connection to the dbo
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                // SQL command
                String sql = "SELECT email FROM dbo.Users WHERE email='" + email + "';";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                        check = true;
                }
                catch (MySqlException ex)
                { throw; }
                finally
                { connection.Close(); }
            }
            return check;
        }
    }
}