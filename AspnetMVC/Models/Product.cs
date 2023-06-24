using Common.Model;

namespace AspnetMVC.Models
{
    public class Product:ModelBase
    {
        public Product()
        { }
        public String Name { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory{get; set;}
        public Product(int id, String name, int productCategoryId, decimal price)
        {
            Id = id;
            Name = name;
            ProductCategoryId = productCategoryId;
            Price = price;
        }
    }
}
