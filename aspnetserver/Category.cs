using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

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