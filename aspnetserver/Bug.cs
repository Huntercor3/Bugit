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
        public int type { get; set; }
        public int status { get; set; }
        public int priority { get; set; }
        public string estimatedTime { get; set; }

        public Bug(int b, int c, DateTime d, string desc, int t, int stat, int prio, string e)
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