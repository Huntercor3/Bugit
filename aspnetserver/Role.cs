namespace aspnetserver
{
    public class Role
    {

        private string[] roles = { "Developer", "Admin", "Owner" };
        public int roleId { get; set; }
        public Role(int id)
        {
            roleId = id;
        }

        public string GetRoleName()
        {
            return roles[roleId];
        }

        public bool isDeveloper()
        {
            return roleId == 0;
        }

        public bool isAdmin()
        {
            return roleId == 1;
        }

        public bool isOwner()
        {
            return roleId == 2;
        }
    }
}