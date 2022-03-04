namespace aspnetserver
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }

        public LoginResponse (bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public LoginResponse(bool success, string message, User u)
        {
            Success = success;
            Message = message;
            User = u;
        }
    }
}
