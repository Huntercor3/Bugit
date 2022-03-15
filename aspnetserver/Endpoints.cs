using Microsoft.Data.SqlClient;
using System.Data;

namespace aspnetserver
{
    public static class Endpoints
    {
        private static SqlConnectionStringBuilder builder;

        public static void Init()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "bugit-server.database.windows.net";
            builder.UserID = "bugit";
            builder.Password = "CSBS@2201";
            builder.InitialCatalog = "bugit-server";
        }
<<<<<<< HEAD

        public static async Task<List<User>> GetUsers()
=======
       /* public static async Task<List<User>> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            User u = new User((int)record[0], (string)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], new Role((int)record[6]));
                            users.Add(u);
                        }
                    }
                }
            }
            return users;
        }
       */
        public static async Task<List<Bug>> GetAllBugs()
        {
            List<Bug> bugs = new List<Bug>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (string)record[1], (int)record[2], (string)record[3], new Category((int)record[4]));
                            bugs.Add(b);
                        }
                    }
                }
            }
            return bugs;
        }

        public static async Task<List<Project>> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Projects";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Project p = new Project((int)record[0], (string)record[1]);
                            projects.Add(p);
                        }
                    }
                }
            }
            return projects;
        }

        public static async Task<List<Organization>> GetAllOrganizations()
        {
            List<Organization> organizations = new List<Organization>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Organizations";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Organization o = new Organization((int)record[0], (string)record[1]);
                            organizations.Add(o);
                        }
                    }
                }
            }
            return organizations;
        }
/*
        public static async Task<List<User>> GetUsersInProject(int projectId)
>>>>>>> origin/EndpointsRemastered
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Users";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            User u = new User((int)record[0], (string)record[1], (string)record[2], (string)record[3], (string)record[4], (string)record[5], new Role((int)record[6]), (string)record[7]);
                            users.Add(u);
                        }
                    }
                }
            }
            return users;
        }
<<<<<<< HEAD
=======
*/
        public static async Task<List<Bug>> GetBugsInProject(int projectId)
        {
            List<Bug> bugs = new List<Bug>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Bugs JOIN dbo.ProjectBugs WHERE ProjectId=" + projectId.ToString() + " AND dbo.Bugs.BugId = dbo.ProjectUsers.BugId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Bug b = new Bug((int)record[0], (string)record[1], (int)record[2], (string)record[3], new Category((int)record[6]));
                            bugs.Add(b);
                        }
                    }
                }
            }
            return bugs;
        }

        public static async Task<List<Project>> GetProjectInOrganization(int organizationId)
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.Projects JOIN dbo.OrganizationProjects WHERE OrganizationId=" + organizationId.ToString() + " AND dbo.Projects.ProjectId = dbo.OrganizationProjects.ProjectId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            IDataRecord record = (IDataRecord)reader;
                            Project p = new Project((int)record[0], (string)record[1]);
                            projects.Add(p);
                        }
                    }
                }
            }
            return projects;
        }

        public static async Task<List<String>> GetCommentsForBug(int bugId)
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
>>>>>>> origin/EndpointsRemastered
    }
}