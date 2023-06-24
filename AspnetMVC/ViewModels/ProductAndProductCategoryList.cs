using AspnetMVC.Models;
using System.Collections.Specialized;

namespace AspnetMVC.ViewModels
{
    public class ProductAndProductCategoryList
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public NameValueCollection sortOptions
        {
            get
            {
                var v = new NameValueCollection();
                v.Add("Price from low to high", value: "price_asc");
                v.Add("Price from high to low", value: "price_desc");
                v.Add("Name", value: "name");
                return v;
            }
        }
        public string SelectedSortOption { get; set; }
        public string SelectedOrderByOption { get; set; }


    }
}
