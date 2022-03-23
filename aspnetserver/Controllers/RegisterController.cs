using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetserver
{
    public class RegisterController : Controller
    {
        public async Task<IActionResult> RegisterOnPostAsync(RegisterModel userEntry)
        {
            LoginModel login = new LoginModel();
            UserService service = new UserService();
            if (!string.IsNullOrEmpty(userEntry.emailAddress) &&
                !string.IsNullOrEmpty(userEntry.password))
            {
                while (userEntry.password != null || userEntry.password.Length != 6)
                {
                    User userToRegister = new User(userEntry.userId, userEntry.firstName, userEntry.lastName, userEntry.emailAddress, userEntry.phoneNumber, userEntry.hardware, userEntry.role, userEntry.password);
                    LoginModel loginModel = new LoginModel();
                    loginModel.EmailAddress = userEntry.emailAddress;
                    loginModel.Password = userEntry.password;
                    if (service.CheckUserInDBOBool(loginModel.EmailAddress.ToString()))
                        return (IActionResult)Results.BadRequest("User already exists");

                    await UserDBHelper.AddUser(userToRegister);

                    login.EmailAddress = userToRegister.email;
                    login.Password = userToRegister.password;

                    return (IActionResult)Results.Ok();
                }
            }
            return (IActionResult)Results.BadRequest("Invalid registration credentials");
        }
    }
}