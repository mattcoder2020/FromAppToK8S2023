using Common.Model;
using System;
using System.Collections.Generic;

namespace OrderService.Models
{
    public class Order: ModelBase
    {
        public Order()
        {
            
        }
        //create a order class in which aggregate product object in a list
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       
        public decimal Total { get; set; }
        public decimal Fax { get; set; }
        public string PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
