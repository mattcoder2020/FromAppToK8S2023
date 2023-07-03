using AspnetMVC.Models;
using AspnetMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspnetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBasketService basketService;

        public HomeController(ILogger<HomeController> logger, IBasketService basketService)
        {
            _logger = logger;
            this.basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await basketService.GetBasket();
            TempData["quantity"] = basketService.GetBasketQuantity(basket);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}