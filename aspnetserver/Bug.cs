using System.Collections.Generic;
using System;

namespace aspnetserver
{
    public class Bug
    {
        public int bugId { get; set; }
        public string software { get; set; }
        public int creator { get; set; }
        public DateTime timeCreated { get; set; }
        public List<string> comments = new List<string>();
        public Category category;
        public string description { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int priority { get; set; }

        public Bug(int b, string s, int c, DateTime d, Category cat, string desc, int t, int stat, int prio)
        {
            bugId = b;
            software = s;
            creator = c;
            timeCreated = d;
            category = cat;
            description = desc;
            type = t;
            status = stat;
            priority = prio;
        }

        public void AddComment(string s)
        {
            comments.Add(s);
        }

        public void RemoveComment(int pos)
        {
            comments.RemoveAt(pos);
        }
    }
}