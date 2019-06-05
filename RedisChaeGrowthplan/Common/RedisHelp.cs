using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Common
{
    public static class RedisHelp
    {
        // 设置 redis 集群服务器地址，目前不知道如何自动切换，所以只能给出所有可连接的地址
        private static string[] ips = new[] {
            "192.168.40.136:8001", "192.168.40.136:8002",
            "192.168.40.136:8003", "192.168.40.136:8004",
            "192.168.40.136:8005","192.168.40.136:8006",};
        private static string connectionip = string.Join(",", ips);
        private static readonly object Locker = new object();
        private static ConnectionMultiplexer redisConn;

        /// <summary>
        /// 单例模式，连接数据库
        /// </summary>
        /// <returns></returns>
        public static ConnectionMultiplexer redisConntion()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(connectionip);
                    }
                }
            }
            return redisConn;
        }

        /// <summary>
        /// 连接到那个数据库
        /// </summary>
        /// <returns></returns>
        public static IDatabase dbContext()
        {
            IDatabase db = redisConntion().GetDatabase();
            return db;
        }

        private readonly static object locks = new object();


        /// <summary>
        /// 模拟库存数量的修改，当高并发量请求时会发生什么
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateStock()
        {
            lock (locks) // 加上锁，防止在高并发量请问求时修改失误
            {
                int ints = int.Parse(dbContext().StringGet("stock"));
                if (ints > 0)
                {
                    dbContext().StringSet("stock", ints - 1);
                    Console.WriteLine("修改成功，当前库存数量是：" + (ints - 1));
                }
                else
                {
                    Console.WriteLine("库存不足不允许修改");
                }
            }

        }



    }
}
