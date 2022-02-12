using DomZdravljaCL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string cs = "Server=.;Database=DomZdravlja;Trusted_Connection=True;";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            var pacijenti = Repository.GetPacijenti(1);
            return View(pacijenti);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}