using aspnetserver.Models;

namespace aspnetserver.Services
{
    public class UserService : IUserService
    {
        public UserAuth Get(UserLogin userLogin)
        {
            UserAuth user = TEMPLocalUserRepo.Users.FirstOrDefault(o =>
                o.EmailAddress.Equals(userLogin.EmailAddress, StringComparison.OrdinalIgnoreCase) &&
                o.Password.Equals(userLogin.Password));

            return user;
        }

        public List<UserAuth> ListUsers()
        {
            var users = TEMPLocalUserRepo.Users;
            return users;
        }
    }
}