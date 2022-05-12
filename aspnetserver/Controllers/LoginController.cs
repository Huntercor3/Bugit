using aspnetserver.Services;
using aspnetserver.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.AspNetCore.Mvc;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        ///  Takes user data and logs the user in or returns if the parameters don't match.
        /// </summary>
        /// <response code="200">Logs the user into the system</response>
        /// <response code="400">User entered credentials aren't valid</response>
        [HttpPost]
        public async Task<IResult> LoginUser(LoginModel user, CookieContainer cookieCollection, IUserService service)
        {
            if (!string.IsNullOrEmpty(user.EmailAddress) &&
                !string.IsNullOrEmpty(user.Password))
            {
                if (service.CheckUserInDBOBool(user.EmailAddress))
                {
                    var loggedInUser = service.CheckUserInDBO(user);
                    if (loggedInUser == null)
                        return Results.BadRequest();
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

                    return Results.Ok();
                }
                else
                    return Results.BadRequest();
            }
            return Results.BadRequest();
        }
    }
}