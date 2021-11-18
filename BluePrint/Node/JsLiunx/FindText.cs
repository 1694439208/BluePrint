using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("字符串匹配", "JsLiunx")]
    public class JsFindText : NodeBase
    {
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
        }
        public JsFindText(BParent _bParent):base(_bParent) {
            Title = "字符串匹配";
            base._IntPutJoin.AddRange(new List<(IJoinControl,Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "开始执行的接头",
                    Value = new JoinType("执行开始"),
                    Type = typeof(JoinType),
                    Tips = "流程开始",
                }),
                (new TextBoxJoint(bParent, IJoinControl.NodePosition.Left, this){ 
                    Watermark = "字符串|支持正则",
                    Enabled = false,
                    width = 120,
                },new Node_Interface_Data{
                    Title = "",
                    Value = "",
                    Type = typeof(string),
                    Tips = "字符串",
                }),
                (new TextBoxJoint(bParent, IJoinControl.NodePosition.Left, this){
                    Watermark = "字符串|支持正则",
                    Enabled = false,
                    width = 120,
                },new Node_Interface_Data{
                    Title = "",
                    Value = "",
                    Type = typeof(string),
                    Tips = "字符串",
                })
            });
            base._OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "匹配成功流程|匹配不成功就一直等待",
                    Value = new JoinType("匹配成功流程|匹配不成功就一直等待"),
                    Type = typeof(JoinType),
                    Tips = "匹配成功流程|匹配不成功就一直等待",
                })
            });
        }

        public override void Execute(List<object> arguments, in Runtime.Evaluate.Result result)
        {
            //输出默认
            base.Execute(arguments, result);
        }

        public override string CodeTemplate(List<string> Execute, List<string> PrevNodes, List<ParameterAST> arguments, List<ParameterAST> result)
        {
            var data = arguments[0].Join.Get().GetData<string>();
            return $@"{result[0].ID.GetID(false)}=${{{data}}}";
        }
    }
}
