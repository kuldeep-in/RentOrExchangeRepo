using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentOrExchange.WebApp.DAL;
using RentOrExchange.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RentOrExchange.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserPostRepository _userPostRepository;

        public HomeController(ILogger<HomeController> logger, IUserPostRepository userPostRepository)
        {
            _logger = logger;
            _userPostRepository = userPostRepository;
        }

        public IActionResult Rent()
        {
            var result = _userPostRepository.GetPostsByType(1);
            return View(result);
        }

        public IActionResult Exchange()
        {
            var result = _userPostRepository.GetPostsByType(2);
            return View(result);
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
