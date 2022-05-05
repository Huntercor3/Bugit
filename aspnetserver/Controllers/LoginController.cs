using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace aspnetserver.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<int> LoginUser(LoginModel user, CookieContainer cookieContainer)
        {
            IUserService service = new UserService();

            // Checks to make sure there is data being entered
            if (!string.IsNullOrEmpty(user.EmailAddress) &&
                !string.IsNullOrEmpty(user.Password))
            {
                // Checks to see if the username is valid
                if (service.CheckUserInDBOBool(user.EmailAddress))
                {
                    // Checks username and password
                    var loggedInUser = service.CheckUserInDBO(user);
                    // Returns a fail if there is not a match for the username and password
                    if (loggedInUser == null)
                        return 400;

                    // Sets the domain for the cookies
                    string domain = "purple-ground-019dc9c0f.1.azurestaticapps.net";

                    // Creates the cookies
                    Cookie usernameCookie = new Cookie("Username", loggedInUser.EmailAddress, "", domain),
                        userRole = new Cookie("UserRole", loggedInUser.Role, "", domain),
                        userFirstName = new Cookie("UserFirstName", loggedInUser.FirstName, "", domain),
                        userLastName = new Cookie("UserLastName", loggedInUser.LastName, "", domain);

                    // Sets expire time on the cookies
                    DateTime dateTime = DateTime.Now;
                    usernameCookie.Expires = dateTime.AddMinutes(5);
                    userRole.Expires = dateTime.AddMinutes(5);
                    userFirstName.Expires = dateTime.AddMinutes(5);
                    userLastName.Expires = dateTime.AddMinutes(5);

                    // Sets the cookies to discard after they expire
                    usernameCookie.Discard = true;
                    userRole.Discard = true;
                    userFirstName.Discard = true;
                    userLastName.Discard = true;

                    // Adds the cookies to the cookieContainer
                    cookieContainer.Add(usernameCookie);
                    cookieContainer.Add(userRole);
                    cookieContainer.Add(userFirstName);
                    cookieContainer.Add(userLastName);

                    // Returns a success
                    return 200;
                }
            }
            // Returns a false if there is no entered user data or it is not a valid username
            return 400;
        }
    }
}