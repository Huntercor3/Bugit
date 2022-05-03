namespace aspnetserver
{
    public class Project
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        private List<User> users = new List<User>();
        private List<Bug> bugs = new List<Bug>();
        public int Archived { get; set; }

        public Project(int id, string name, int archived)
        {
            projectId = id;
            projectName = name;
            Archived = archived;
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