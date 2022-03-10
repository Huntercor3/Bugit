using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email/username is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}