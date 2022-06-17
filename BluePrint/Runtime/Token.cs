using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint.Runtime
{
    public class Token
    {
        public enum NodeToken {
            Expression,//表达式，用于中止枚举
            Call,//执行
            Value,//参数
            CallValue,//又想让他执行又给他参数
            ObjectValue,//自定义参数，自己给节点赋值
            None
        }
    }
}
