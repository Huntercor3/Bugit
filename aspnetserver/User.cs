using System.ComponentModel.DataAnnotations;

namespace aspnetserver
{
    public class User
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string hardware { get; set; }
        public Role role { get; set; }

        public User(int u, string f, string l, string e, string p, string h, Role r, string pass)
        {
            userId = u;
            firstName = f;
            lastName = l;
            email = e;
            phoneNumber = p;
            hardware = h;
            role = r;
            password = pass;
        }
    }
}