using System;
using TestConsole.Attributes;

namespace TestConsole
{
    class Program
   {
        static void Main(string[] args)
        {
            Student student = new Student {
                ID = Guid.NewGuid().ToString(),
                Name = "小明"
            };
            ManagerCenter.ManagerStudent<Student>(student);

        }
    }
}
