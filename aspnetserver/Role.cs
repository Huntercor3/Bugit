namespace aspnetserver
{
    public class Role
    {
        private string[] roles;
        public int roleId { get; set; }
        public Role(int id)
        {
            roleId = id;
        }

        public string GetRole()
        {
            return roles[roleId];
        }
    }
}