using System.Collections.Generic;

namespace WpfApp1.ShopApp.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}