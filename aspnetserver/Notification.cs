namespace aspnetserver
{
    public class Notification
    {
        private string[] urgencies;
        public string message { get; set; }
        public int urgency { get; set; }

        public Notification(string m, int u)
        {
            message = m;
            urgency = u;
        }

        public string GetUrgency(int id)
        {
            return urgencies[id];
        }
    }
}