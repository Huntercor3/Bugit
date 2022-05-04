using aspnetserver.Models;

namespace aspnetserver.Services
{
    public interface IUserService
    {
        public UserAuth CheckUserInDBO(LoginModel userLogin);

        public bool CheckUserInDBOBool(string email);
    }
}