using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TestConsole.Attributes
{
    public class ZqyRangeAttribute : ZqyBaseAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        public ZqyRangeAttribute(int min,int max)
        {
            this._Max = max;
            this._Min = min;
            /*base.FuncVaidate = Value => Value != null &&
                       !string.IsNullOrWhiteSpace(Value.ToString()) &&
                        int.TryParse(Value.ToString(), out int vales) &&
                       this._Max > vales &&
                       this._Min < vales;*/
            base.FuncVaidate = this.IsValid;
        }

        public override bool IsValid(object Value)
        {
            return     Value != null &&
                       !string.IsNullOrWhiteSpace(Value.ToString()) &&
                        int.TryParse(Value.ToString(), out int vales) &&
                       this._Max > vales &&
                       this._Min < vales;
        }
    }
}