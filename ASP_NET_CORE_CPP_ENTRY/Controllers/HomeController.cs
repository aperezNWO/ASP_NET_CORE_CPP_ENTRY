using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;

namespace Pruebas.Cliente.Controllers
{
    /// <summary>
    /// Represents the structure of the incoming JSON payload.
    /// </summary>
    public class UploadRequest
    {
        public string Base64Image { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        #region "ROOT FUNCTIONS "

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

  
        #endregion 
    }
}