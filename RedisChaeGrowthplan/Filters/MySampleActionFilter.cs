using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RedisChaeGrowthplan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using RedisChaeGrowthplan.Models;

namespace RedisChaeGrowthplan.Filters
{
    /// <summary>
    /// 过滤器
    /// Redis的数据类型 https://blog.csdn.net/qin_yu_2010/article/details/84262742
    /// </summary>
    public class MySampleActionFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// Action 方法调用后，Result 方法调用前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
            var forms = context.HttpContext.Request.Form;
            foreach (var item in forms)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    var DisplayName = context.HttpContext.Request.Method + "-" + context.RouteData.Values["Controller"].ToString() + "-" + context.RouteData.Values["Action"].ToString() + "-" + item.Key + "-" + item.Value;
                    var result = context.Result as JsonResult;
                    RedisHelp.dbContext().StringSet(DisplayName, MyJsonHelper.SerializeObject(result.Value));
                }
            }

        }

        /// <summary>
        /// Action 调用前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var forms = context.HttpContext.Request.Form;
            foreach (var item in forms)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    var DisplayName = context.HttpContext.Request.Method + "-" + context.RouteData.Values["Controller"].ToString() + "-" + context.RouteData.Values["Action"].ToString() + "-" + item.Key + "-" + item.Value;
                    var redisHas = RedisHelp.dbContext().KeyExists(DisplayName);
                    //如果存在该数据即直接返回不请求数据库
                    if (redisHas)
                    {
                        var json = RedisHelp.dbContext().StringGet(DisplayName);
                        var listUser = MyJsonHelper.DeserializeJsonToList<Users>(json);
                        context.Result = new JsonResult(listUser);
                    }

                }
            }
        }

    }
}
