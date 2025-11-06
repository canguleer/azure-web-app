using System.Diagnostics;
using azure_web_app.Models;
using azure_web_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace azure_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productService = new ProductService();
        }

        public IActionResult Index()
        {
            // Demonstrate ToList() vs AsEnumerable()
            _logger.LogInformation("=== ToList() Example ===");
            var productsWithToList = _productService.GetAvailableProductsWithToList();
            _logger.LogInformation($"Products retrieved with ToList(): {productsWithToList.Count}");
            
            _logger.LogInformation("=== AsEnumerable() Example ===");
            var productsWithAsEnumerable = _productService.GetAvailableProductsWithAsEnumerable();
            _logger.LogInformation($"Products retrieved with AsEnumerable(): {productsWithAsEnumerable.Count()}");
            
            _logger.LogInformation("=== Multiple Enumeration Example (ToList) ===");
            var stats = _productService.GetProductStatsWithToList();
            _logger.LogInformation($"Stats - Count: {stats.count}, Total: {stats.total}");
            
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
