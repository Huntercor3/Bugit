using System.Collections.Generic;
using System;

namespace aspnetserver
{
    public class Bug
    {
        public int bugId { get; set; }
        public string software { get; set; }
        public int creator { get; set; }
        public string timeCreated { get; set; }
        public List<string> comments = new List<string>();
        public Category category;

        public Bug(int b, string s, int c, string d, Category cat)
        {
            bugId = b;
            software = s;
            creator = c;
            timeCreated = d;
            category = cat;
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