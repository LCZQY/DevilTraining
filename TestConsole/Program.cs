using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading;
using TestConsole.Attributes;
using TestConsole.Common;

namespace TestConsole
{
    class Program
   {      
        //static void Main(string[] args)
        //{
        //    Student student = new Student {
        //        ID = Guid.NewGuid().ToString(),
        //        Name = "小明",
        //        Age= 500,
        //        Sex=true
        //    };
        //    //ManagerCenter.ManagerStudent<Student>(student);
        //    //ManagerCenter.ManagerZqyModelVaildatin<Student>(student);
        //    ManagerCenter.ManagerZqyValidataModel<Student>(student);

        //}
        static void Main(string[] args)
        {
            // 设置 db 值
            //string value = "abcdefg";
            //db.StringSet("mykey", value);
            //string message = "Item value is: " + db.StringGet("mykey");
            //Console.WriteLine(message);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                //Thread.Sleep(1); // 等待100ms
                                 
                RedisHelp.UpdateStock();
                //string message = DateTime.Now.ToString("HH:mm:ss.fff") + " Item value is: " + db.StringGet("name");
                //Console.WriteLine(message);
                if (watch.ElapsedMilliseconds > 1 * 1000)
                {
                    // 运行 1min
                    break;
                }
            }
            watch.Stop();
        }


    }
}

