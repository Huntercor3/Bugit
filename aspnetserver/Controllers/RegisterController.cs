using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetserver
{
    public class RegisterController : Controller
    {
        public async Task<IResult> RegisterUser(RegisterModel userEntry, IUserService service)
        {
            LoginModel login = new LoginModel();

            if (!string.IsNullOrEmpty(userEntry.emailAddress) &&
                !string.IsNullOrEmpty(userEntry.password))
            {
                while (userEntry.password != null || userEntry.password.Length >= 6)
                {
                    User userToRegister = new User(userEntry.userId, userEntry.firstName, userEntry.lastName, userEntry.emailAddress, userEntry.phoneNumber, userEntry.hardware, userEntry.role, userEntry.password);
                    LoginModel loginModel = new LoginModel();
                    loginModel.EmailAddress = userEntry.emailAddress;
                    loginModel.Password = userEntry.password;
                    if (service.CheckUserInDBOBool(loginModel.EmailAddress.ToString()) == true)
                        return Results.BadRequest();

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
                        return Results.BadRequest();
                    foreach (char c in userEntry.password)
                    {
                        if (c >= '0' && c <= '9')
                        {
                            validConditions++;
                            break;
                        }
                    }
                    if (validConditions == 1)
                        return Results.BadRequest();
                    if (validConditions == 2)
                    {
                        char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
                        if (userEntry.password.IndexOfAny(special) == -1)
                            return Results.BadRequest();
                    }

                    UserDBHelper.AddUser(userToRegister);

                    login.EmailAddress = userToRegister.email;
                    login.Password = userToRegister.password;

                    return Results.Ok();
                }
            }
            return Results.BadRequest();
        }
    }
}