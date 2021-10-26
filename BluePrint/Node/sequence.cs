using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.INode
{
    [NodeBaseInfo("顺序执行", "流程")]
    public class sequence : NodeBase
    {
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
        }
        public sequence(BParent _bParent):base(_bParent) {
            Title = "序列执行";
            base._IntPutJoin.AddRange(new List<(IJoinControl,Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.Left, this),new Node_Interface_Data{
                    Title = "开始执行的接头",
                    Value = new JoinType("执行开始"),
                    Type = typeof(JoinType),
                    Tips = "test",
                }),
            });
            base._OutPutJoin.AddRange(new List<(IJoinControl, Node_Interface_Data)>{
                (new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                    Title = "执行结束的接头",
                    Value = new JoinType("执行结束"),
                    Type = typeof(JoinType),
                    Tips = "test",
                }),
                (new AddExecJoin(bParent, IJoinControl.NodePosition.right, this){ 
                    Commands = {
                        {nameof(AddExecJoin.OnJoinEveTemp),(s,e)=>{
                            var temp = e as DataType.JoinEventType;
                            if (temp.eveType == DataType.EveType.MouseUp)
                            {
                                //_OutPutJoin.find
                                AddOntPut((new ExecJoin(bParent, IJoinControl.NodePosition.right, this),new Node_Interface_Data{
                                    Title = "执行结束的接头",
                                    Value = new JoinType("执行结束"),
                                    Type = typeof(JoinType),
                                    Tips = "test",
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
