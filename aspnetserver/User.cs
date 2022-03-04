using System.ComponentModel.DataAnnotations;

namespace aspnetserver
{
    public class User
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Your password must be between {2} and {1} characters.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string password { get; set; }

        public string phoneNumber { get; set; }
        public string hardware { get; set; }
        public Role role { get; set; }
        public string password { get; set; }

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