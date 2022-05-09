using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email address is required")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }

        public string hardware { get; set; }
        public Role role = new Role(1);
        public int userId;
    }
}