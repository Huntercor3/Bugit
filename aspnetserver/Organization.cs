using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace aspnetserver
{
    public class Organization
    {
        public int organizationId { get; set; }
        private List<Project> projects;
        public Organization()
        {
            
        }
        
        public Organization(int id)
        {
            organizationId = id;
        }

        public void AddProject(Project p)
        {
            projects.Add(p);
        }

        public void RemoveProject(int pos)
        {
            projects.RemoveAt(pos);
        }
    }
}