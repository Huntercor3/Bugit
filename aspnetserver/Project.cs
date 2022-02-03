using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace aspnetserver
{
    public class Project
    {
        public int projectId { get; set; }
        private List<User> users;
        private List<Bug> bugs;
        public Project()
        {
            
        }
        public Project(int id)
        {
            projectId = id;
        }

        public void AddUser(User u)
        {
            users.Add(u);
        }

        public void RemoveUser(int pos)
        {
            users.RemoveAt(pos);
        }

        public void AddBug(Bug b)
        {
            bugs.Add(b);
        }

        public void RemoveBug(int pos)
        {
            bugs.RemoveAt(pos);
        }
    }
}