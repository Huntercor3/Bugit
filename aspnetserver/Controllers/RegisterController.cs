using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetserver
{
    public class RegisterController : Controller
    {
        public IActionResult RegisterOnPostAsync(RegisterModel userEntry)
        {
            LoginModel login = new LoginModel();
            UserService service = new UserService();
            if (!string.IsNullOrEmpty(userEntry.emailAddress) &&
                !string.IsNullOrEmpty(userEntry.password))
            {
                while (userEntry.password != null || userEntry.password.Length >= 6)
                {
                    User userToRegister = new User(userEntry.userId, userEntry.firstName, userEntry.lastName, userEntry.emailAddress, userEntry.phoneNumber, userEntry.hardware, userEntry.role, userEntry.password);
                    LoginModel loginModel = new LoginModel();
                    loginModel.EmailAddress = userEntry.emailAddress;
                    loginModel.Password = userEntry.password;
                    if (service.CheckUserInDBOBool(loginModel.EmailAddress.ToString()) == false)
                        return BadRequest("User already exists");

                    int validConditions = 0;
                    foreach (char c in userEntry.password)
                    {
                        if (c >= 'a' && c <= 'z')
                        {
                            validConditions++;
                            break;
                        }
                    }
                    foreach (char c in userEntry.password)
                    {
                        if (c >= 'A' && c <= 'Z')
                        {
                            validConditions++;
                            break;
                        }
                    }
                    if (validConditions == 0)
                        return BadRequest("Not a valid password.");
                    foreach (char c in userEntry.password)
                    {
                        if (c >= '0' && c <= '9')
                        {
                            validConditions++;
                            break;
                        }
                    }
                    if (validConditions == 1)
                        return BadRequest("Not a valid password.");
                    if (validConditions == 2)
                    {
                        char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
                        if (userEntry.password.IndexOfAny(special) == -1)
                            return BadRequest("Not a valid password.");
                    }

                    UserDBHelper.AddUser(userToRegister);

                    login.EmailAddress = userToRegister.email;
                    login.Password = userToRegister.password;

                    return Ok();
                }
            }
            return (IActionResult)Results.BadRequest("Invalid registration credentials");
        }
    }
}