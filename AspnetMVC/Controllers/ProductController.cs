using AspnetMVC.Models;
using AspnetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspnetMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new ProductAndProductCategoryList();
            String returnValue = await this.productService.GetAll();
            viewmodel.Products = JsonConvert.DeserializeObject<List<Product>>(returnValue);
            returnValue = await this.productService.GetAllCategory();
            viewmodel.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(returnValue);
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> FilterBy(ProductAndProductCategoryList viewmodel)
        {
            var queryparams = new QueryParams();
            queryparams.OrderBy = viewmodel.SelectedSortOption;
            queryparams.ProductCategoryId = viewmodel.SelectedCategory;
            String returnValue = await this.productService.GetProductsByFiltration(queryparams);
            viewmodel.Products = JsonConvert.DeserializeObject<List<Product>>(returnValue);
            returnValue = await this.productService.GetAllCategory();
            viewmodel.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(returnValue);
            return View("index", viewmodel);
        }
    }
}
