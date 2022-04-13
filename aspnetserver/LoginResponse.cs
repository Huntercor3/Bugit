using Microsoft.AspNetCore.Http;

namespace aspnetserver
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public int status { get; set; }

        public LoginResponse (bool success, string message, int stat)
        {
            Success = success;
            Message = message;
            status = stat;
        }

        public LoginResponse(bool success, string message, User u, int stat)
        {
            Success = success;
            Message = message;
            User = u;
            status = stat;
        }
    }
}
