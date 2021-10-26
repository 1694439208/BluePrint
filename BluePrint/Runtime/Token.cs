using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint.Runtime
{
    public class Token
    {
        public enum NodeToken {
            Expression,
            Call,
            Value,
            None
        }
    }
}
