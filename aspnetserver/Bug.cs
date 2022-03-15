using System.Collections.Generic;
using System;

namespace aspnetserver
{
    public class Bug
    {
<<<<<<< HEAD
        public int BugId { get; set; }
        public int Creator { get; set; }
        public string TimeCreated { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string EstimatedTime { get; set; }

        public Bug(int bugId, int creator, string timeCreated, string description, string type, string status, string priority, string estimatedTime)
=======
        public int bugId { get; set; }
        public string software { get; set; }
        public int creator { get; set; }
        public string timeCreated { get; set; }
        public List<string> comments = new List<string>();
        public Category category;

        public Bug(int b, string s, int c, string d, Category cat)
>>>>>>> origin/EndpointsRemastered
        {
            BugId = bugId;
            Creator = creator;
            TimeCreated = timeCreated;
            Description = description;
            Type = type;
            Status = status;
            Priority = priority;
            EstimatedTime = estimatedTime;
        }
    }
}