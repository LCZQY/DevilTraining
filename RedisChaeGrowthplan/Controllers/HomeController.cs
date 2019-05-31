using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedisChaeGrowthplan.Models;

namespace RedisChaeGrowthplan.Controllers
{
    public class HomeController : Controller
    {
        private readonly AprilDbContext _DbContext;
        private readonly IMemoryCache _memoryCache;
        public HomeController(AprilDbContext aprilDbContext, IMemoryCache memoryCache)
        {
            _DbContext = aprilDbContext;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var list = _DbContext.Users.FirstOrDefault(u=>u.TrueName !=null).UserName;
            ViewBag.name = list;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
