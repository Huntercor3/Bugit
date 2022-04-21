using aspnetserver.Services;
using aspnetserver.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginUser(LoginModel user)
        {
            IUserService service = new UserService();
            if (!string.IsNullOrEmpty(user.EmailAddress) &&
                !string.IsNullOrEmpty(user.Password))
            {
                if (service.CheckUserInDBOBool(user.EmailAddress))
                {
                    var loggedInUser = service.CheckUserInDBO(user);
                    if (loggedInUser == null)
                        return BadRequest("Invalid user credentials");
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                    new Claim(ClaimTypes.Role, loggedInUser.Role)
                };
                    var identity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                    return Ok();
                }
                else
                    return BadRequest("Invalid user credentials");
            }
            return BadRequest("Invalid user credentials");
        }
    }
}
