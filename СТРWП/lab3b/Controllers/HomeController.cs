using System.Diagnostics;
using System.Security.Claims;
using lab3b.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3b.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public /*async*/ IActionResult Index()
        {
            //var userName = 
            ////var roles = string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
            ViewBag.UserName = User.Identity?.Name;
            //ViewBag.Roles = await ;
            //return View();
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ViewBag.current_roles = roles; // Передача списка ролей в представление
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
