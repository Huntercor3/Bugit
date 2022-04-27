using aspnetserver.Services;
using aspnetserver.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
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

                    string domain = "purple-ground-019dc9c0f.1.azurestaticapps.net";
                    Cookie usernameCookie = new Cookie("Username", loggedInUser.EmailAddress.ToString(), "", domain);
                    Cookie userRole = new Cookie("UserRole", loggedInUser.Role.ToString(), "", domain);
                    //usernameCookie.Domain = "purple-ground-019dc9c0f.1.azurestaticapps.net";
                    //new CookieContainer().Add(cookie);
                    cookieCollection.Add(usernameCookie);
                    //cookieCollection.Add(userRole);

                    return Ok();
                }
                else
                    return BadRequest("Invalid user credentials");
            }
            return (IActionResult)Results.BadRequest("Invalid user credentials");
        }
    }
}