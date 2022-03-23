using aspnetserver.Models;

namespace aspnetserver.Services
{
    public interface IUserService
    {
        public UserAuth CheckUserInDBO(LoginModel userLogin);

        //public static Task<bool> CheckUserInDBOBool(string email);
    }
}