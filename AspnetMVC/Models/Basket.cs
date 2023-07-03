namespace AspnetMVC.Models
{
   


    public class Basket
    {
        public Basket()
        {
            if (items == null)
            {
                items = new List<BasketItem>();
            }
        }

        public List<BasketItem> items { get; set; }
        public int subtotal { get; set; }
        public int shipment { get; set; }
        public int total { get; set; }
        public string basketid { get; set; }
    }


    public class BasketItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string productCategory { get; set; }
        public int productCategoryId { get; set; }
        public int quantity { get; set; }
    }

    public class ProductCategoryForBasket
    {
        public string description { get; set; }
        public int id { get; set; }
    }
}
