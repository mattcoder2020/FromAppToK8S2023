using AspnetMVC.Models;
using System.Collections.Specialized;

namespace AspnetMVC.ViewModels
{
    public class ProductAndProductCategoryList
    {
        public Product Product { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
