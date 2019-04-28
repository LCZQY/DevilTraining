using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;
using RedisChaeGrowthplan.Stores;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;

namespace RedisChaeGrowthplan.Common
{

    [AttributeUsage(AttributeTargets.All,AttributeUsageAttribute = true)]
    public class RedisCacheAttribute : Attribute
    {

        private readonly string _name;

        public string Name { get { return _name; } }


        public RedisCacheAttribute(string name)
        {
            _name = name;
        }
    }

    [RedisCache("")]
    public class CustomAttributes
    {

    }
}

