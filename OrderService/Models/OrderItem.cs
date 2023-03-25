using System;

namespace OrderService.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int id, String name, int productCategory, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            ProductCategory = productCategory;
            Price = price;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public int ProductCategory { get; set; }
      
    }
}