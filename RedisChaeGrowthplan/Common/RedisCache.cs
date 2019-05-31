////using Microsoft.Extensions.Caching.Redis;
//using Newtonsoft.Json;
//using RedisChaeGrowthplan.Stores;
//using StackExchange.Redis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Caching.Distributed;
//using System.Threading;

//namespace RedisChaeGrowthplan.Common
//{

//    public class RedisCache : IRedisCache
//    {
//        private ConnectionMultiplexer connection { get; set; }
//        private IDatabase db { get; set; }
//        private readonly string _instance;

        


//        public RedisCache(RedisCacheOptions options, int database = 0)
//        {
          
//            //实现思路：根据key.GetHashCode() % 节点总数量，确定连向的节点
//            //也可以自定义规则(第一个参数设置)

//            connection = ConnectionMultiplexer.Connect(options.Configuration);
//            //读取第一个缓存数据库
//            db = connection.GetDatabase(database);
//            _instance = options.InstanceName;
//        }

//        /// <summary>
//        /// 是否存在
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>     
//        async Task<bool> IRedisCache.IsExists(string key)
//        {
//            return await db.KeyExistsAsync(key);
//        }

//        /// <summary>
//        /// 添加缓存
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public async Task<bool> SetValue(string key, string value)
//        {
//            if (key == null)
//            {
//                throw new ArgumentNullException(nameof(key));
//            }

//            var add = await db.StringSetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
//            return add;
//        }
//        /// <summary>
//        /// 添加缓存
//        /// </summary>
//        /// <param name="key">缓存Key</param>
//        /// <param name="value">缓存Value</param>
//        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
//        /// <param name="expiressAbsoulte">绝对过期时长</param>
//        /// <returns></returns>
//        public async Task<bool> SetValue(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
//        {
//            if (key == null)
//            {
//                throw new ArgumentNullException(nameof(key));
//            }
//            return await db.StringSetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
//        }
//        /// <summary>
//        /// 添加缓存
//        /// </summary>
//        /// <param name="key">缓存Key</param>
//        /// <param name="value">缓存Value</param>
//        /// <param name="expiresIn">缓存时长</param>
//        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
//        /// <returns></returns>
//        public async Task<bool> SetValue(string key, object value, TimeSpan expiresIn, bool isSliding = false)
//        {
//            if (key == null)
//            {
//                throw new ArgumentNullException(nameof(key));
//            }
//            return await db.StringSetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiresIn);
//        }

//        /// <summary>
//        /// 删除缓存
//        /// </summary>
//        /// <param name="key">缓存Key</param>
//        /// <returns></returns>
//        public async Task<bool> Remove(string key)
//        {
//            if (key == null)
//            {
//                throw new ArgumentNullException(nameof(key));
//            }
//            return await db.KeyDeleteAsync(key);
//        }
//        /// <summary>
//        /// 批量删除缓存
//        /// </summary>
//        /// <param name="key">缓存Key集合</param>
//        /// <returns></returns>
//        public async Task<bool> RemoveAll(IEnumerable<string> keys)
//        {
//            if (keys == null)
//            {
//                throw new ArgumentNullException(nameof(keys));
//            }
//            foreach (var item in keys)
//            {
//                if (!await Remove(item))
//                {
//                    return false;
//                };
//            }
//            return true;
//        }



//        /// <summary>
//        /// 获取缓存
//        /// </summary>
//        /// <param name="key">缓存Key</param>
//        /// <returns></returns>
//        //public T Get<T>(string key) where T : class
//        //{
//        //    if (key == null)
//        //    {
//        //        throw new ArgumentNullException(nameof(key));
//        //    }

//        //    var value = db.StringGet(key);

//        //    if (!value.HasValue)
//        //    {
//        //        return default(T);
//        //    }
//        //    return JsonConvert.DeserializeObject<T>(value);
//        //}    
//        /// <summary>
//        /// 获取缓存
//        /// </summary>
//        /// <param name="key">缓存Key</param>
//        /// <returns></returns>
//        public object Get(string key)
//        {
//            if (key == null)
//            {
//                throw new ArgumentNullException(nameof(key));
//            }

//            var value = db.StringGet(key);
//            if (!value.HasValue)
//            {
//                return null;
//            }
//            return value;

//        }
//        /// <summary>
//        /// 获取缓存集合
//        /// </summary>
//        /// <param name="keys">缓存Key集合</param>
//        /// <returns></returns>
//        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
//        {
//            if (keys == null)
//            {
//                throw new ArgumentNullException(nameof(keys));
//            }

//            var dict = new Dictionary<string, object>();
//            keys.ToList().ForEach(item => dict.Add(item, Get(item)));
//            return dict;
//        }


//    }
//}

