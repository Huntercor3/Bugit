namespace aspnetserver
{
    public class Organization
    {
        public int organizationId { get; set; }
        public string organizationName { get; set; }
        private List<Project> projects = new List<Project>();
        
        public Organization(int id, string name)
        {
            organizationId = id;
            organizationName = name;
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