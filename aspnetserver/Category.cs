namespace aspnetserver
{
    public class Category
    {
        private string[] categories;
        public Category()
        {
            
        }

        public string GetCategory(int id)
        {
            return categories[id];
        }
    }
}