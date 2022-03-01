namespace aspnetserver
{
    public class Category
    {
        public int catId { get; set; }
        private string[] categories;
        public Category(int c)
        {
            catId = c;
        }

        public string GetCategory(int id)
        {
            return categories[id];
        }
    }
}