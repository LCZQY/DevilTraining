using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Common
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)] //AttributeUsage(对那个进行修饰,允许特性的叠加）
    public class MyCacheAttribute : Attribute
    {

    }
}
