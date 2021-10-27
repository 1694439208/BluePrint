using System;
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
                }),
                (new VarComboxJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "变量类型",
                    Value = new List<string>{"字符串","整数"},
                    Type = typeof(List<string>),
                    Tips = "选择脚本类型",
                }),
            });
            base._OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "test",
                })
            });
        }

        public override void Execute(List<object> arguments, in Runtime.Evaluate.Result result)
        {
            //输出默认
            base.Execute(arguments, result);
        }

        public override string CodeTemplate(List<string> Execute, List<string> PrevNodes, List<string> arguments, List<string> result)
        {
            return $@"
{Execute.join("\r\n")}
";
        }
    }
}
