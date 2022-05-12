using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                // Checks to see if the username is valid
                if (service.CheckUserInDBOBool(user.EmailAddress))
                {
                    // Checks username and password
                    var loggedInUser = service.CheckUserInDBO(user);
                    // Returns a fail if there is not a match for the username and password
                    if (loggedInUser == null)
                        return Results.BadRequest();
                    // Sets the domain for the cookies
                    string domain = "purple-ground-019dc9c0f.1.azurestaticapps.net";

                    // Creates the cookies
                    Cookie usernameCookie = new Cookie("Username", loggedInUser.EmailAddress, "", domain),
                        userRole = new Cookie("UserRole", loggedInUser.Role, "", domain),
                        userFirstName = new Cookie("UserFirstName", loggedInUser.FirstName, "", domain),
                        userLastName = new Cookie("UserLastName", loggedInUser.LastName, "", domain),
                        userId = new Cookie("UserId", loggedInUser.UserID.ToString(), "", domain);

                    // Sets expire time on the cookies
                    DateTime dateTime = DateTime.Now;
                    usernameCookie.Expires = dateTime.AddMinutes(5);
                    userRole.Expires = dateTime.AddMinutes(5);
                    userFirstName.Expires = dateTime.AddMinutes(5);
                    userLastName.Expires = dateTime.AddMinutes(5);
                    userId.Expires = dateTime.AddMinutes(5);

                    // Sets the cookies to discard after they expire
                    usernameCookie.Discard = true;
                    userRole.Discard = true;
                    userFirstName.Discard = true;
                    userLastName.Discard = true;
                    userId.Discard = true;

                    // Adds the cookies to the cookieContainer
                    cookieCollection.Add(usernameCookie);
                    cookieCollection.Add(userRole);
                    cookieCollection.Add(userFirstName);
                    cookieCollection.Add(userLastName);
                    cookieCollection.Add(userId);

                    // Returns a success
                    return Results.Ok();
                }
                // Returns a false if there is no entered user data or it is not a valid username
                return Results.BadRequest();
            }
            else
                return Results.BadRequest();
        }
    }
}