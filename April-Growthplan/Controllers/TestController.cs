using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace April_Growthplan.Controllers
{
    public class TestController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public TestController(IMemoryCache memory)
        {
            _memoryCache = memory;
        }


        public IActionResult Index()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (!_memoryCache.TryGetValue<List<int>>("list", out List<int> list))
            {
                list = CreateList(out list);


                _memoryCache.Set<List<int>>("list", list);
            }
            stopwatch.Stop();
            ViewBag.time = stopwatch.ElapsedMilliseconds.ToString();
            return View("Index", list);
        }

        public static List<int> CreateList(out List<int> list)
        {
            list = new List<int>();
            for (int i = 0; i < 10000000; i++)
            {
                list.Add(i / 3);
            }
            return list;
        }
    }
}