using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Attributes
{
    /// <summary>
    /// 缓存该方法数据
    /// </summary>
    public class CacheMethodAttribute : Attribute
    {
        private string Guid { get; set; }

        public CacheMethodAttribute(string id)
        {
            this.Guid = id;
        }

        public void Read(object Value)
        {
            //Console.WriteLine($"Id:{this.Guid},该方法的返回体是:{Value}");
            Console.WriteLine($"方法名称是：{this.Guid}");
        }
    }
}
