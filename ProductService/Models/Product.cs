using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get;  set; }
        public String Category { get;  set; }
        public decimal Price { get;  set; }

        public Product(int id, String name, String category, decimal price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
