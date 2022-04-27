using aspnetserver.Services;
using aspnetserver.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult LoginUser(LoginModel user, CookieContainer cookieCollection)
        {
            IUserService service = new UserService();
            if (!string.IsNullOrEmpty(user.EmailAddress) &&
                !string.IsNullOrEmpty(user.Password))
            {
                if (service.CheckUserInDBOBool(user.EmailAddress))
                {
                    var loggedInUser = service.CheckUserInDBO(user);
                    if (loggedInUser == null)
                        return BadRequest();
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                    new Claim(ClaimTypes.Role, loggedInUser.Role)
                };
                    var identity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    string domain = "purple-ground-019dc9c0f.1.azurestaticapps.net";
                    Cookie usernameCookie = new Cookie("Username", loggedInUser.EmailAddress.ToString(), "", domain);
                    Cookie userRole = new Cookie("UserRole", loggedInUser.Role.ToString(), "", domain);

                    cookieCollection.Add(usernameCookie);
                    cookieCollection.Add(userRole);

                    return Ok();
                }
                else
                    return BadRequest();
            }
            return BadRequest();
        }
    }
}