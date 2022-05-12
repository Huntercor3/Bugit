using MySql.Data.MySqlClient;

namespace aspnetserver
{
    public class DBHelper
    {
        private MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

        public DBHelper()
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

        private void ExecuteCommand(string input)
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
            {
                MySqlCommand command = new MySqlCommand(input, connection);
                command.ExecuteNonQuery();
            }
        }

        public void AddUser(User u)
        {
            String sql = "INSERT INTO dbo.Users (FirstName, LastName, email, PhoneNumber, Hardware, Role) values ("
                    + u.firstName + ", " + u.lastName + ", " + u.email + ", " + u.phoneNumber + ", " + u.hardware + ", " + u.role.roleId.ToString() + ")";

            ExecuteCommand(sql);
        }

        public void RemoveUser(int id)
        {
            String sql = "DELETE FROM dbo.Users WHERE UserId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void RemoveBug(int id)
        {
            String sql = "DELETE FROM dbo.Bugs WHERE BugId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddBugComment(string c, int id)
        {
            String sql = "INSERT INTO dbo.BugComments (BugId, Comment) values ("
                + id.ToString() + ", " + c + ")";

            ExecuteCommand(sql);
        }

        public void RemoveBugComment(int id)
        {
            String sql = "DELETE FROM dbo.BugComments WHERE Id=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddProject(Project p)
        {
            String sql = "INSERT INTO dbo.Projects (ProjectName) values ("
                + p.projectName + ")";

            ExecuteCommand(sql);
        }

        public void RemoveProject(int id)
        {
            String sql = "DELETE FROM dbo.Projects WHERE ProjectId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddBugToProject(int projId, int bugId)
        {
            String sql = "INSERT INTO dbo.ProjectBugs (ProjectId, BugId) values ("
                + projId.ToString() + ", " + bugId.ToString() + ")";

            ExecuteCommand(sql);
        }

        public void RemoveBugFromProject(int id)
        {
            String sql = "DELETE FROM dbo.ProjectBugs WHERE BugId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddUserToProject(int projId, int userId)
        {
            String sql = "INSERT INTO dbo.ProjectUsers (ProjectId, UserId) values ("
                + projId.ToString() + ", " + userId.ToString() + ")";

            ExecuteCommand(sql);
        }

        public void RemoveUserFromProject(int id)
        {
            String sql = "DELETE FROM dbo.ProjectUsers WHERE UserId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddOrganization(Organization o)
        {
            String sql = "INSERT INTO dbo.Organizations (Description) values ("
                + o.organizationName + ")";

            ExecuteCommand(sql);
        }

        public void RemoveOrganization(int id)
        {
            String sql = "DELETE FROM dbo.Organizations WHERE OrganizationId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddProjectToOrganization(int orgId, int projId)
        {
            String sql = "INSERT INTO dbo.OrganizationProjects (OrganizationID, ProjectId) values ("
                + orgId.ToString() + ", " + projId.ToString() + ")";

            ExecuteCommand(sql);
        }

        public void RemoveProjectFromOrganization(int id)
        {
            String sql = "DELETE FROM dbo.OrganizationProjects WHERE ProjectId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddMessage(Message m)
        {
            String sql = "INSERT INTO dbo.Messages (SenderId, RecipientId, MessageBody) values ("
                + m.sender.ToString() + ", " + m.recipient.ToString() + ", " + m.body + ")";

            ExecuteCommand(sql);
        }

        public void RemoveMessage(int id)
        {
            String sql = "DELETE FROM dbo.Messages WHERE MessageId=" + id.ToString();

            ExecuteCommand(sql);
        }

        public void AddNotification(Notification n)
        {
            String sql = "INSERT INTO dbo.Messages (Urgency, MessageBody) values ("
                + n.urgency.ToString() + ", " + n.message + ")";

            ExecuteCommand(sql);
        }

        public void RemoveNotification(int id)
        {
            String sql = "DELETE FROM dbo.Notifications WHERE NotificationId=" + id.ToString();

            ExecuteCommand(sql);
        }
    }
}