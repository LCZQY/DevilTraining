using System;
using System.Reflection;

namespace TestConsole.Attributes
{


    /// <summary>
    /// 利用反射找到该特性中得方法和属性
    /// </summary>
    public class ManagerCenter
    {

        /// <summary>
        /// 读取数据值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void ManagerStudent<T>(T t) where T : Student
        {

            Console.WriteLine("常规的方法>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine($"主键：{t.ID}");
            Console.WriteLine($"姓名：{t.Name}");


            Console.WriteLine("反射得到特性中得值>>>>>>>>>>>>>>");
            Type type = t.GetType();
            foreach (var item in type.GetProperties()) //找到全部属性并遍历
            {
                //找到特性中的属性名和值
                Console.WriteLine($"{AttributeExend.GetDispaly(item)},{ item.GetValue(t)}");
            }
            t.Study();
        }

        /// <summary>
        /// 显示自定义的错误描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ManagerZqyValidataModel<T>(T t)
        {
            Type type = t.GetType();
            foreach (var prope in type.GetProperties())
            {
                if (prope.IsDefined(typeof(ZqyBaseAttribute), true))
                {
                    dynamic Value = prope.GetValue(t);
                    foreach (var item in prope.GetCustomAttributes<ZqyBaseAttribute>())
                    {
                        ValidataModel validate = item.ValidataModelMsg(Value);
                        if (!validate.Result)
                        {
                            return validate.ErrorMessage;
                        }
                    }
                }
            }
            return "数据没毛病";
            
        }


        /// <summary>
        /// 验证数据是否确定
        /// </summary>
        public static bool ManagerZqyModelVaildatin<T>(T t)
        {
            Type type = t.GetType();
            foreach (var prope in type.GetProperties())
            {
                if (prope.IsDefined(typeof(ZqyBaseAttribute), true))
                {
                    dynamic Value = prope.GetValue(t);
                    foreach (var item in prope.GetCustomAttributes<ZqyBaseAttribute>())
                    {
                        if (!item.IsValid(Value))
                        {
                            Console.WriteLine(item.GetType().Name + "不正确");
                        }
                    }
                }

                #region 封装前
                //if (prope.IsDefined(typeof(ZqyRangeAttribute), true))
                //{
                //    ZqyRangeAttribute attribute = prope.GetCustomAttribute<ZqyRangeAttribute>();                    
                //    dynamic Value = prope.GetValue(t);
                //    var range = attribute.IsValid(Value);
                //    if (!attribute.IsValid(Value))
                //    {
                //        Console.WriteLine($"该字段的数据范围错误");
                //    }


                //    //return Value == null ||
                //    //    string.IsNullOrWhiteSpace(Value.ToString()) ||
                //    //    attribute._Max < Value.ToString().Length ||
                //    //    attribute._Min > Value.ToString().Length;

                //}

                //if (prope.IsDefined(typeof(ZqyStringLengthAttribute), true))
                //{
                //    ZqyStringLengthAttribute attribute = prope.GetCustomAttribute<ZqyStringLengthAttribute>();
                //    dynamic Value = prope.GetValue(t);

                //    var range = attribute.IsValid(Value);
                //    if (!attribute.IsValid(Value))
                //    {
                //        Console.WriteLine($"该字段输入的长度错误");
                //    }

                //    //新写法
                //    //return Value == null ||
                //    //    string.IsNullOrWhiteSpace(Value.ToString()) ||
                //    //    attribute._length < Value.ToString().Length;

                //}
                #endregion

            }
            return true;
        }

    }

    public static class AttributeExend
    {
        public static string GetDispaly(this PropertyInfo property)
        {

            if (property.IsDefined(typeof(DisplayNameAttribute), true))
            {
                DisplayNameAttribute attribute = property.GetCustomAttribute<DisplayNameAttribute>();
                return attribute.GetName();
            }
            else
            {
                return property.Name;
            }

        }
    }


}
