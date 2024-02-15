using Microsoft.AspNetCore.Mvc;
using PurchaseOrderWebApplication.Filters;
using System.Diagnostics;

namespace PurchaseOrderWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDependency _dependency;
        public HomeController(ILogger<HomeController> logger, IDependency dependency)
        {
            _logger = logger;
            _dependency = dependency;
        }
        public void OnGet()
        {
            try
            {
                _logger.LogInformation("Test log");
                _dependency.WriteMessage("Developed by Logha!");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                throw;
            }
        }
       
        [HttpGet]
        [ServiceFilter(typeof(ExceptionFilter))] 
        public IActionResult Get()
        {
            throw new Exception("Exception in HomeController.");
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
            return View(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
    }
}
