using aspnetserver.Models;
using aspnetserver;
using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver.Services
{
    public class UserService : IUserService
    {
        private static SqlConnectionStringBuilder builder;

        static UserService()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }

        public UserAuth Get(UserLogin userLogin)
        {
            UserAuth user = new UserAuth();
            bool check = true;
            /*
            UserAuth user = TEMPLocalUserRepo.Users.FirstOrDefault(o =>
                o.EmailAddress.Equals(userLogin.EmailAddress, StringComparison.OrdinalIgnoreCase) &&
                o.Password.Equals(userLogin.Password));
            */
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
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
            if (check)
                return user;
            else
                return null;
        }

        public List<UserAuth> ListUsers()
        {
            var users = TEMPLocalUserRepo.Users;
            return users;
        }
    }
}