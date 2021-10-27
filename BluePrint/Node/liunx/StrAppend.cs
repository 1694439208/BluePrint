using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("字符串拼接", "LiunxScript")]
    public class StrAppend : NodeBase
    {
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
        }
        public StrAppend(BParent _bParent):base(_bParent) {
            Title = "字符串拼接";
            base._IntPutJoin.AddRange(new List<(IJoinControl,Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "开始执行的接头",
                    Value = new JoinType("执行开始"),
                    Type = typeof(JoinType),
                    Tips = "开始执行的接头",
                }),
                (new AddExecJoin(bParent, IJoinControl.NodePosition.Left, this){
                    Commands = {
                        {nameof(AddExecJoin.OnJoinEveTemp),(s,e)=>{
                            var temp = e as DataType.JoinEventType;
                            if (temp.eveType == DataType.EveType.MouseUp)
                            {
                                //_OutPutJoin.find
                                AddIntPut((new TextBoxJoint(bParent, IJoinControl.NodePosition.Left, this){
                                    Watermark = "字符串|变量名",
                                    Enabled = false,
                                },new Node_Interface_Data{
                                    Title = "执行结束的接头",
                                    Value = "",
                                    Type = typeof(string),
                                    Tips = "拼接的字符串",
                                }),s as IJoinControl,IsAddList:true);
                            }
                        }},
                    },
                },new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "添加一个执行序列",
                }),
            });
            base._OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "执行结束的接头",
                }),
                (new ValueText(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = "",
                    Type = typeof(string),
                    Tips = "返回拼接完成的字符串",
                }),
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
