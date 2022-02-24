using aspnetserver.Models;

namespace aspnetserver.Services
{
    public interface IUserService
    {
        public UserAuth Get(UserLogin userLogin);
    }
}