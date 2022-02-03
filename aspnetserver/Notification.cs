using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace aspnetserver
{
    public class Notification
    {
        private string[] urgencies;
        public string message { get; set; }
        public Notification()
        {
            
        }

        public Notification(string m)
        {
            message = m;
        }

        public string GetUrgency(int id)
        {
            return urgencies[id];
        }
    }
}