namespace aspnetserver
{
    public class Message
    {
        public User sender { get; set; }
        public User recipient { get; set; }
        public string body { get; set; }

        public Message(User s, User r, string b)
        {
            sender = s;
            recipient = r;
            body = b;
        }
    }
}