using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Join;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("设置脚本类型", "LiunxScript")]
    public class ScriptType : NodeBase
    {
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
        }
        public ScriptType(BParent _bParent):base(_bParent) {
            Title = "设置脚本类型";
            base._IntPutJoin.AddRange(new List<(IJoinControl,Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "开始执行的接头",
                    Value = new JoinType("执行开始"),
                    Type = typeof(JoinType),
                    Tips = "test",
                }),
                (new ComboBoxJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "脚本类型",
                    Value = new List<string>{"脚本","自动化脚本"},
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
