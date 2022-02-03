using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace aspnetserver
{
    public class Role
    {
        private string[] roles;
        public Role()
        {
            
        }

        public string GetRole(int id)
        {
            return roles[id];
        }
    }
}