using AspnetMVC.Models;
using AspnetMVC.Services;
using AspnetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspnetMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IBasketService basketService;

        public ProductController(IProductService productService, IBasketService basketService)
        {
            this.productService = productService;
            this.basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new ProductListAndProductCategoryList();
            String returnValue = await this.productService.GetAll();
            viewmodel.Products = JsonConvert.DeserializeObject<List<Product>>(returnValue);
            returnValue = await this.productService.GetAllCategory();
            viewmodel.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(returnValue);
            viewmodel.ProductCategories.Insert(0, new ProductCategory { Id=-1, Description = "All"});
            
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> FilterBy(ProductListAndProductCategoryList viewmodel)
        {
            var queryparams = new QueryParams();
            queryparams.OrderBy = viewmodel.SelectedSortOption;
            queryparams.ProductCategoryId = viewmodel.SelectedCategory == -1 ? null: viewmodel.SelectedCategory;
            queryparams.OrderBy = viewmodel.SelectedSortOption == "none" ? null : viewmodel.SelectedSortOption;
            String returnValue = await this.productService.GetProductsByFiltration(queryparams);
            viewmodel.Products = JsonConvert.DeserializeObject<List<Product>>(returnValue);
            returnValue = await this.productService.GetAllCategory();
            viewmodel.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(returnValue);
            viewmodel.ProductCategories.Insert(0, new ProductCategory { Id = -1, Description = "All" });
            //var basket = await basketService.GetBasket();
            //TempData["quantity"] = basketService.GetBasketQuantity(basket);
            return View("index", viewmodel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var returnValue = await this.productService.GetById(id);
            var viewmodel = new ProductAndProductCategoryList();
            viewmodel.Product = JsonConvert.DeserializeObject<Product>(returnValue);
            returnValue = await this.productService.GetAllCategory();
            viewmodel.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(returnValue);
            return View("Edit", viewmodel);
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            var returnValue = await this.productService.GetById(id);
            var product = JsonConvert.DeserializeObject<Product>(returnValue);
            var basketitem = basketService.ProductToBasketItem(product);
            var basket = await basketService.UpdateBasketItem(basketitem);
            TempData["quantity"] = basketService.GetBasketQuantity(basket);

            //
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductAndProductCategoryList viewmodel)
        {
            await this.productService.UpdateProduct(viewmodel.Product);
            var returnValue = await this.productService.GetById(viewmodel.Product.Id);
         
            var product = JsonConvert.DeserializeObject<Product>(returnValue);
            return View("View", product);
        }
    }
}
