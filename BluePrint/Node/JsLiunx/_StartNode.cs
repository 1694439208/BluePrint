using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("执行入口", "JsLiunx")]
    public class Js_StartNode : NodeBase
    {
        public Js_StartNode(BParent _bParent) :base(_bParent)
        {
            Title = "开始执行";
            ///节点输出参数 设置
            _OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>
            {
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this,true),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "test",
                }),
            });

            ///节点输入参数 设置
            /*_IntPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>
            {
            });*/
        }
        
        /// <summary>
        /// 计算节点
        /// </summary>
        public override void Execute(List<object> arguments, in Runtime.Evaluate.Result result)
        {
            base.Execute(arguments, result);
        }
        public override string CodeTemplate(List<string> Execute, List<string> PrevNodes, List<ParameterAST> arguments, List<ParameterAST> result)
        {
            return $"{Execute.join("\r\n")}";
        }
    }
}
