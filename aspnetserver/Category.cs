namespace aspnetserver
{
    public class Category
    {
        private string[] categories;
        public int categoryId { get; set; }
        public Category(int c)
        {
            categoryId = c;
        }

        public string GetCategory(int id)
        {
            return categories[categoryId];
        }
    }
}