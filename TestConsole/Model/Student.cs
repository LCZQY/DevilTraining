using System;
using TestConsole.Attributes;

namespace TestConsole
{
    public  class Student
    {
        [ZqyRequired(ErrorMessage ="不能够为空")]
        [DisplayName("编号")]
        public string ID { get; set; }

        [ZqyRequired(ErrorMessage = "不能够为空")]
        [DisplayName("姓名")]
        [ZqyStringLength(6,ErrorMessage ="姓名不能够超过6位数")]
        public string Name { get; set; }

        [ZqyRequired(ErrorMessage = "不能够为空")]
        [ZqyRange(1,100,ErrorMessage ="年龄范围不正确")]
        public int Age { get; set; }


        [ZqyRequired(ErrorMessage = "不能够为空")]
        public bool? Sex { get; set; }

        public void Study()
        {
            Console.WriteLine($"我的主键是：{this.ID},我得姓名是：{this.Name},现在正在学习....");
        }
    }
}
