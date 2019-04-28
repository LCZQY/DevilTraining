using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedisChaeGrowthplan.Stores;
using RedisChaeGrowthplan.Common;

using RedisChaeGrowthplan.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisChaeGrowthplan.Controllers
{
    public class LoginController : Controller
    {
        private readonly AprilDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly IRedisCache _redisCache;
        public LoginController(AprilDbContext dbContext, IMemoryCache memoryCache, IRedisCache redisCache)
        {

            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        public IActionResult Index()
        {
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> Logining(Users users)
        {                     
            var Nokey = await _redisCache.IsExists("NoExists:" + users.UserName);
            if(Nokey)
            {
                return new JsonResult(new { code = "200", msg = "用户名或密码错误" });
            }

            var key = _redisCache.Get(users.UserName)?.ToString();
            if (!(key == users.PasswordHash))
           // if (!_memoryCache.TryGetValue<string>(users.UserName, out string _name))
            {
                var Isexist = _dbContext.Users.Where(y => y.UserName == users.UserName && y.PasswordHash == users.PasswordHash).FirstOrDefault();

                if (Isexist != null)
                {                
                    await _redisCache.SetValue(users.UserName, users.PasswordHash);
                    //MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                    //options.AbsoluteExpiration = DateTime.Now.AddMinutes(5); //绝对过期时间
                    //options.SlidingExpiration = TimeSpan.FromMinutes(5);  //滚动过期时间
                    //_name = users.UserName;
                    //_memoryCache.Set<string>(users.UserName, _name);        
                }
                else
                {
                    //为了防止缓存穿透 ， 把数据库中不存在的名称都缓存下来，下次再来查询的时候就直接返回结果即可
                    await _redisCache.SetValue($"NoExists:{users.UserName}", users.PasswordHash);
                    return new JsonResult(new { code = "200", msg = "用户名或密码错误" });
                }
            }                
            return Redirect("~/Home/Index");
        }
    }
}