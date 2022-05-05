namespace aspnetserver
{
    /// <summary>
    /// This class is going to hold all the data we need for the user during their
    /// session. We can store things like username, role, email, profile picture,
    /// etc., etc.
    /// </summary>
    public class UserAuth
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserID { get; set; }
    }
}