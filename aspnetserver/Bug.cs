using System.Collections.Generic;
using System;

namespace aspnetserver
{
    public class Bug
    {
        public int bugId { get; set; }
        public int creator { get; set; }
        public DateTime timeCreated { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string estimatedTime { get; set; }

        public Bug(int b, int c, DateTime d, string desc, string t, string stat, string prio, string e)
        {
            bugId = b;
            creator = c;
            timeCreated = d;
            description = desc;
            type = t;
            status = stat;
            priority = prio;
            estimatedTime = e;
        }
    }
}