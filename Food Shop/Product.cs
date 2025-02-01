using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Shop
{
    public class Product
    {
        public static List<Product> products = new List<Product>();
        public ulong Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public string Country_of_origin { get; set; }
        public string Description { get; set; }
    }
}
