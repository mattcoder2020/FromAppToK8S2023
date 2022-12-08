using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class Product:ModelBase
    {
        public String Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public ProductCategory ProductCategory{get; set;}
        public Product()
        { }
        public Product(int id, String name, int category, decimal price)
        {
            Id = id;
            Name = name;
            CategoryId = category;
            Price = price;
        }
    }
}
