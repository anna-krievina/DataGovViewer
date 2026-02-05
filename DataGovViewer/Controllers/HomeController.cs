using Microsoft.AspNetCore.Mvc;
using DataGovViewer.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using DataGovViewer.Services;

namespace DataGovViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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