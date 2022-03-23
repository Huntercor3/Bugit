using aspnetserver.Services;
using aspnetserver.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> LoginOnPostAsync(LoginModel user)
        {
            IUserService service = new UserService();
            if (!string.IsNullOrEmpty(user.EmailAddress) &&
                !string.IsNullOrEmpty(user.Password))
            {
                var loggedInUser = service.CheckUserInDBO(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                    new Claim(ClaimTypes.Role, loggedInUser.Role)
                };
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                return (IActionResult)Results.Ok(/*tokenString*/);
            }
            return (IActionResult)Results.BadRequest("Invalid user credentials");
        }

        //public async Task<IActionResult> LoginOnPostAsync(LoginModel user)
        //{
        //    IUserService service = new UserService();
        //    if (!string.IsNullOrEmpty(user.EmailAddress) &&
        //        !string.IsNullOrEmpty(user.Password))
        //    {
        //        var loggedInUser = service.CheckUserInDBO(user);
        //        if (loggedInUser is null) return (IActionResult)Results.NotFound("User not found or password incorrect");

        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress.ToString()),
        //            new Claim(ClaimTypes.Role, loggedInUser.Role.ToString())
        //        };

        //        var claimsIdentity = new ClaimsIdentity(
        //            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var authProperties = new AuthenticationProperties
        //        {
        //            AllowRefresh = true,
        //            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        //            IsPersistent = true,
        //            IssuedUtc = DateTimeOffset.UtcNow,
        //            RedirectUri = "/nomatch"
        //        };

        //        await HttpContext.SignInAsync
        //            (
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            authProperties).Wait();

        //        return (IActionResult)Results.Ok(/*tokenString*/);
        //    }

        //     Something failed. Redisplay the form.
        //    return (IActionResult)Results.BadRequest("Invalid user credentials");
        //}
    }
}