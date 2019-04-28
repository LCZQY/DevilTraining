using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TestConsole.Attributes
{
    public class DisplayNameAttribute : Attribute
    {
        private string Name = null;
        public DisplayNameAttribute(string _name)
        {
            Console.WriteLine($"{this.GetType().Name},在构造赋值中..");
            this.Name = _name;
        }

        public string GetName()
        {
            return this.Name;
        }
    }
}