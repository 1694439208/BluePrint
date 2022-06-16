﻿using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Join;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("创建变量", "LiunxScript")]
    public class CreateVar : NodeBase
    {
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
        }
        public CreateVar(BParent _bParent):base(_bParent) {
            Title = "创建变量";
            base._IntPutJoin.AddRange(new List<(IJoinControl,Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "开始执行的接头",
                    Value = new JoinType("执行开始"),
                    Type = typeof(JoinType),
                    Tips = "test",
                })
            });
            base._OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "test",
                }),
                (new VarComboxJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "变量类型",
                    Value = (0,"",""),
                    Type = typeof((int typeindex,string name,string value)),
                    Tips = "输出变量名",
                }),
            });
        }

        public override void Execute(object Context, List<object> arguments, in Runtime.Evaluate.Result result)
        {
            //输出默认
            base.Execute(Context,arguments, result);
        }

        public override string CodeTemplate(List<string> Execute, List<string> PrevNodes, List<ParameterAST> arguments, List<ParameterAST> result)
        {
            var data = result[0].Join.Get().GetData<(int typeindex, string name, string value)>();
            return $@"{data.name}={(data.typeindex == 0 ? $"\"{data.value}\"" : data.value)}
{result[0].ID.GetID(false)}=${{{data.name}}}
{Execute.join("\r\n")}";  
            
        }
    }
}
