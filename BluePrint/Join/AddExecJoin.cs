using CPF;
using CPF.Controls;
using CPF.Input;
using CPF.Svg;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.INode;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.IJoin
{
    
    public class AddExecJoin : IJoinControl
    {
        public AddExecJoin() : base()
        {
        }
        public AddExecJoin(BParent _bParent, NodePosition JoinDir, Control Node) : base(_bParent, JoinDir,Node, Runtime.Token.NodeToken.None)
        {
            nodePosition = JoinDir;
            _Node = Node;
        }
        //public override Control Get_NodeRef() { return base.Get_NodeRef(); }
        public NodePosition nodePosition;
        Control _Node;
        public override void SetDir(NodePosition value)
        {
            nodePosition = value;
        }
        public override NodePosition GetDir()
        {
            return nodePosition;
        }
        public override void Set(Node_Interface_Data value)
        {
            title = value;
        }
        public override Node_Interface_Data Get()
        {
            return title;
        }
        public UIElement UINode = new Panel {
            Width = 20f,
        };

        public Node_Interface_Data title;

        public event JoinEvent OnJoinEveTemp {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }


        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            //为了方便就固定了状态
            SetIsConnectState(false);

            var svg = new SVG
            {
                Triggers =
                {
                    {
                        nameof(SVG.IsMouseOver),
                        Relation.Me,
                        null,
                        (nameof(SVG.Fill),"#aaa")
                    }
                },
                Commands =
                {
                    {
                        nameof(SVG.MouseUp),
                        (s,e)=>{
                            this.RaiseEvent(new DataType.JoinEventType(){
                                eveType=DataType.EveType.MouseUp,
                                Value = e,
                            },nameof(OnJoinEveTemp));
                        }
                    },
                },
                IsHitTestVisible = true,
                ToolTip = title?.Value,
                IsAntiAlias = true,
                Fill = "#FFFFFF",
                Size = "16,16",
                Stretch = Stretch.Uniform,
                Source = "<svg ><path d=\"m0,29.08312l29.08312,0l0,-29.08312l29.83376,0l0,29.08312l29.08312,0l0,29.83376l-29.08312,0l0,29.08312l-29.83376,0l0,-29.08312l-29.08312,0l0,-29.83376z\" p-id=\"1199\"></path></svg>"
            };
            //svg.RaiseEvent(1, nameof(SVG.MouseUp));

            var b = base.GetJoinRef();
            b.BorderThickness = new Thickness(0, 0, 0, 0);
            b.Width = 16;
            b.Height = 16;
            b.Child = svg;

            /*if (OnJoinEveTemp != null)
            {
                OnJoinEveTemp(this, EveType.MouseUp, e);
            }*/
            //b.Child.RaiseEvent(e, nameof(DoubleClick));
            UINode = new TextBlock
            {
                Width = 60f,
                Text = title.Title,
                Foreground = "255,255,255",
                TextAlignment = CPF.Drawing.TextAlignment.Center,
            };
            base.AddControl(UINode, nodePosition);
        }
        /*public event EventHandler<RoutedEventArgs> DoubleClick
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }*/
    }
}
