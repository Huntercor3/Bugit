using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}