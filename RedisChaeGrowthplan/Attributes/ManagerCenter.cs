using RedisChaeGrowthplan.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Attributes
{
     /// <summary>
     /// 
     /// </summary>
    public static class ManagerCenter
    {      
        public static void DataShow()
        {
    
            MethodInfo method = typeof(HomeController).GetMethod("get");
            Attribute[] atts = Attribute.GetCustomAttributes(method);
            foreach (Attribute att in atts)
            {                
                if (att.GetType() == typeof(CacheMethodAttribute))
                {                                    
                   //Console.WriteLine(((CacheMethodAttribute)att).TypeId + "," + ((CacheMethodAttribute)att).vet);
                }
            }
        }
    }
}
