using System;
using System.Reflection;

namespace TestConsole.Attributes
{


    /// <summary>
    /// 父类特性器方法可在子类中扩展
    /// </summary>
    public abstract class ZqyBaseAttribute : Attribute
    {

        public abstract bool IsValid(object Value);

        public string ErrorMessage = null;

        protected Func<object, bool> FuncVaidate;


        public  ValidataModel ValidataModelMsg(object oValue)
        {

            if (this.FuncVaidate.Invoke(oValue))
            {
                return new ValidataModel
                {
                    ErrorMessage = "数据验证正确！",
                    Result = true,
                };
            }
            else
            {
                return new ValidataModel
                {
                    ErrorMessage = this.ErrorMessage,
                    Result = false,
                };
            }
        }
    }

}
