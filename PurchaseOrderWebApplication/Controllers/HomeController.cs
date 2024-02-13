using Microsoft.AspNetCore.Mvc;
using PurchaseOrderWebApplication.Models;
using System.Diagnostics;

namespace PurchaseOrderWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            try
            {
                _logger.LogInformation("Test log");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                throw;
            }
        }
        public IActionResult Index()
        {
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
