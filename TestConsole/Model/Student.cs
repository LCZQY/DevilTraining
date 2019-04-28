using System;
using TestConsole.Attributes;

namespace TestConsole
{
    public  class Student
    {

        [DisplayName("主键")]
        public string ID { get; set; }


        [DisplayName("姓名")]
        public string Name { get; set; }


        public void Study()
        {
            Console.WriteLine($"我的主键是：{this.ID},我得姓名是：{this.Name},现在正在学习....");
        }
    }
}
