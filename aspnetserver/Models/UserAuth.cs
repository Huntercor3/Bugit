namespace aspnetserver
{
    public class UserAuth
    {
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string JWTString { get; set; }
    }
}