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
        public string projectName { get; set; }
        private List<User> users = new List<User>();
        private List<Bug> bugs = new List<Bug>();

        public Project(int id, string name)
        {
            projectId = id;
            projectName = name;
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