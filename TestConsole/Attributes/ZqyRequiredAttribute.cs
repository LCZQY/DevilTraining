using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TestConsole.Attributes
{
    public class ZqyRequiredAttribute : ZqyBaseAttribute
    {

        public ZqyRequiredAttribute()
        {
            base.FuncVaidate = this.IsValid;
        }

        public override bool IsValid(object Value)
        {
            return Value != null &&
                   !string.IsNullOrWhiteSpace(Value.ToString());
                       
        }

    }
}