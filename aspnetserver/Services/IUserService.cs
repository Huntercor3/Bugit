using aspnetserver.Models;

namespace aspnetserver.Services
{
    public interface IUserService
    {
<<<<<<< HEAD
        public UserAuth Get(UserLogin userLogin);

        public List<UserAuth> ListUsers();
=======
        public UserAuth CheckUserInDBO(UserLogin userLogin);
>>>>>>> Feature-Login/Reg-Backend
    }
}