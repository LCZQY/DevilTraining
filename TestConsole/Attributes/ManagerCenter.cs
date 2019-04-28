using System;
using System.Reflection;

namespace TestConsole.Attributes
{


    /// <summary>
    /// 利用反射找到该特性中得方法和属性
    /// </summary>
    public class ManagerCenter
    {

        public static void ManagerStudent<T>(T t) where T : Student
        {


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
