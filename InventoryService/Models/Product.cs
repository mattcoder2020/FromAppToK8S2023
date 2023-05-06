using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Product:ModelBase
    {
        public Product()
        {
            Quantity = 0;
        }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId{get; set;}
        public int Quantity { get; set; }
        public Product(int id, String name, int productCategory, decimal price)
        {
            Id = id;
            Name = name;
            ProductCategoryId = productCategory;
            Price = price;
        }
    }
}
