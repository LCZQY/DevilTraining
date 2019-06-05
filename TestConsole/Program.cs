using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using TestConsole.Attributes;
using TestConsole.Common;
using TestConsole.Filters;
namespace TestConsole
{
    public static class Operation
    {
        [Command("AddLabel", "AddName")]
        [MySampleActionFilter]
        public static string Add()
        {
            Console.WriteLine("方法中执行......");
            return "ok";
        }

        [Command("DelLabel", "DelName")]
        public static void Del()
        {
            Console.WriteLine("Del");
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string Label { get; set; }
        public string Name { get; set; }

        public CommandAttribute() { }

        public CommandAttribute(string label, string name)
        {
            this.Label = label;
            this.Name = name;
        }
    }
    public class Person
    {
        public Person user { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }
    class Program
    {

        static void Main(string[] args)
        {

            // Operation.Add();

            //Student student = new Student
            //{
            //    ID = Guid.NewGuid().ToString(),
            //    Name = "小明",
            //    Age = 500,
            //    Sex = true
            //};

            //List<Student> student = new List<Student> {
            //    new Student { ID = Guid.NewGuid().ToString(), Name = "小明", Age = 500, Sex = true},
            //    new Student{ID = Guid.NewGuid().ToString(), Name = "小李", Age = 5001,Sex = false
            //    } };


            #region 序列化于反序列化

            //序列化操作
            var json = student;
            string jsonData = JsonConvert.SerializeObject(json);
            Console.WriteLine(jsonData);
            //反序列化操作方法一
            // Student p1 = JsonConvert.DeserializeObject<Student>(jsonData);
            //Console.WriteLine($"{p1.ID},{p1.Name},{p1.Age},{p1.Sex}");
            if(RedisHelp.dbContext().KeyExists("student"))
            {
                //反序列化操作方法二        
                var redisString = RedisHelp.dbContext().StringGet("student");
                List<Student> listp = JsonConvert.DeserializeObject<List<Student>>(redisString);
                foreach (var item in listp)
                {
                    Console.WriteLine($"{item.ID},{item.Name},{item.Age},{item.Sex}");
                    Console.ReadKey();
                }
            
            }
            RedisHelp.dbContext().StringSet("student",jsonData);
            //反序列化操作方法二        
            List <Student> listp2 = JsonConvert.DeserializeObject<List<Student>>(jsonData);
            Console.WriteLine($"{listp2[0].ID},{listp2[0].Name},{listp2[0].Age},{listp2[0].Sex}");
            Console.ReadKey();

            #endregion

            #region  自定义特性和特性处理逻辑的封装
            //ManagerCenter.ManagerStudent<Student>(student);
            //ManagerCenter.ManagerZqyModelVaildatin<Student>(student);
            // ManagerCenter.ManagerZqyValidataModel<Student>(student);
            #endregion

            //#region 

            ////Operation op = new Operation();
            //MethodInfo method = typeof(Operation).GetMethod("Add");
            //Attribute[] atts = Attribute.GetCustomAttributes(method);
            //foreach (Attribute att in atts)
            //{
            //    if (att.GetType() == typeof(CommandAttribute))
            //    {
            //        Console.WriteLine(((CommandAttribute)att).Name + "," + ((CommandAttribute)att).Label);
            //    }
            //}

            //#endregion

            //#region 获取所有的方法属性
            //Operation testClass = new Operation();
            //Type type = testClass.GetType();
            //// Iterate through all the methods of the class.
            //foreach (MethodInfo mInfo in type.GetMethods())
            //{
            //    // Iterate through all the Attributes for each method.
            //    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
            //    {
            //        // Check for the AnimalType attribute.
            //        if (attr.GetType() == typeof(CommandAttribute))
            //            Console.WriteLine(
            //                "Method {0} has a CommandAttribute {1},{2} .",
            //                mInfo.Name, ((CommandAttribute)attr).Label, ((CommandAttribute)attr).Name);
            //    }
            //}
            //Console.ReadLine();
            //#endregion


        }

        /// Redis 集群
        //static void Main(string[] args)
        //{
        //    // 设置 db 值
        //    //string value = "abcdefg";
        //    //db.StringSet("mykey", value);
        //    //string message = "Item value is: " + db.StringGet("mykey");
        //    //Console.WriteLine(message);

        //    Stopwatch watch = new Stopwatch();
        //    watch.Start();
        //    while (true)
        //    {
        //        //Thread.Sleep(1); // 等待100ms

        //        RedisHelp.UpdateStock();
        //        //string message = DateTime.Now.ToString("HH:mm:ss.fff") + " Item value is: " + db.StringGet("name");
        //        //Console.WriteLine(message);
        //        if (watch.ElapsedMilliseconds > 1 * 1000)
        //        {
        //            // 运行 1min
        //            break;
        //        }
        //    }
        //    watch.Stop();
        //}


    }
}

