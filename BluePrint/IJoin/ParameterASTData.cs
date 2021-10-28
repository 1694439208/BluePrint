using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint.IJoin
{
    public class ParameterAST
    {
        /// <summary>
        /// 接口id用来生成变量名
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 接口对象
        /// </summary>
        public Node.IJoinControl Join { set; get; }
        /// <summary>
        /// 当前接口指针是否是自己
        /// </summary>
        public bool IsThis { set; get; }
        /// <summary>
        /// 读取接口数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public string GetData<TResult>(Func<Node_Interface_Data, TResult> func) {
            if (IsThis)
            {
                return func(Join.Get()).ToString();
            }
            else {
                return ID.GetID();
            }
        }
    }
}
