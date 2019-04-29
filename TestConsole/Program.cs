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
                Name = "小明",
                Age= 500,
                Sex=true
            };

            //ManagerCenter.ManagerStudent<Student>(student);
            //ManagerCenter.ManagerZqyModelVaildatin<Student>(student);
            ManagerCenter.ManagerZqyValidataModel<Student>(student);

        }
    }
}
