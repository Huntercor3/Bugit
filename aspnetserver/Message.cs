using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace aspnetserver
{
    public class Message
    {
        public User sender { get; set; }
        public User recipient { get; set; }
        public string body { get; set; }
        public Message()
        {
            
        }

        public Message(User s, User r, string b)
        {
            sender = s;
            recipient = r;
            body = b;
        }
    }
}