using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedisChaeGrowthplan.Models;
using RedisChaeGrowthplan.Stores;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Controllers
{
    public class LoginController : Controller
    {
        private readonly AprilDbContext _dbContext;
        // 单机Redis 扩展类使用包 ： Microsoft.Extensions.Caching.Redis.Core
        private readonly IMemoryCache _memoryCache;
        private readonly IRedisCache _redisCache;
        public LoginController(AprilDbContext dbContext, IMemoryCache memoryCache, IRedisCache redisCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        private static ConnectionMultiplexer connDCS =
            ConnectionMultiplexer.Connect("192.168.40.136:8001,192.168.40.136:8002，192.168.40.136:8003,192.168.40.136:8004，192.168.40.136:8005,192.168.40.136:8006");

        private static readonly object Locker = new object();
        //singleton
        private static ConnectionMultiplexer redisConn;
        //singleton
        public static ConnectionMultiplexer getRedisConn()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(connDCS.ToString());
                    }
                }
            }
            return redisConn;
        }

        public IActionResult Index()
        {
            //var csredis = new CSRedis.CSRedisClient(null,
            //    "192.168.40.136:8001,password='',defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=1",
            //    "192.168.40.136:8002,password='',defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=1",
            //    "192.168.40.136:8003,password='',defaultDatabase=13,poolsize=12,ssl=false,writeBuffer=10240,prefix=1",
            //    "192.168.40.136:8004,password='',defaultDatabase=14,poolsize=13,ssl=false,writeBuffer=10240,prefix=1",
            //     "192.168.40.136:8005,password='',defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=1",
            //     "192.168.40.136:8006,password='',defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=1");

            redisConn = getRedisConn();
            var db = redisConn.GetDatabase();
            //set get
            string strKey = "Hello";
            string strValue = "DCS for Redis!";
            db.StringSet(strKey, strValue);
            Console.WriteLine(strKey + ", " + db.StringGet(strKey));


            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Logining(Users users)
        {
            var Nokey = await _redisCache.IsExists("NoExists:" + users.UserName);
            if (Nokey)
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