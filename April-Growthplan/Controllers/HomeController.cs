using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using April_Growthplan.Models;
using Microsoft.Extensions.Caching.Memory;
using AprilGrowthplan.Models;

namespace April_Growthplan.Controllers
{

    public class HomeController : Controller    
    {


        private readonly IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memory)
        {
            _memoryCache = memory;
        }


        private static void  MyCallback(object key, object value, EvictionReason reason, object state)
        {
            var message = $"Cache entry was removed : {reason}";
            ((HomeController)state).
        _memoryCache.Set("callbackMessage", message);

        }

        public IActionResult Index()
        {
           
            if (!_memoryCache.TryGetValue<DateTime>("timestamp", out DateTime timestamp))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(30); //绝对过期时间
                options.SlidingExpiration = TimeSpan.FromMinutes(1);  //滚动过期时间
                options.RegisterPostEvictionCallback(MyCallback, this);
                //当缓存该删除之后可以通过一个函数通知我们是否被删除了 ， 也就是回调函数

                //设置缓存的值
                _memoryCache.Set<DateTime>("timestamp", DateTime.Now);
            }
            ViewBag.back = _memoryCache.Get<string>("callbackMessage");
            return View();

        }



        public IActionResult CacheTryGetValuSet()
        {
            //取得缓存的值
            var timestamp = _memoryCache.Get<DateTime>("timestamp");            
            return View("CacheTryGetValuSet", timestamp);
        }



        #region 视图缓存

        //[ResponseCache(Duration = 60)] //数据保留60秒再次刷新将不会再次请求
        //[ResponseCache(VaryByHeader = "User-Agent", Duration = 5)] //关于vary在Http响应头的作用就是:告诉缓存服务器或者CDN，我还是同一个浏览器的请求，你给我缓存就行了，如果你换个浏览器去请求，那么vary的值肯定为空，那么缓存服务器就会认为你是一个新的请求，就会去读取最新的数据给浏览器
        //[ResponseCache(Location =  ResponseCacheLocation.None,NoStore = true)] //禁用缓存
        [ResponseCache(CacheProfileName = "action1")] 
        public IActionResult ResponseCaches()
        {
            ViewBag.time = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            return View();
        }
        #endregion


     

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
