using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.Join
{
    public class CheckJoint : IJoinControl
    {
        public CheckJoint() : base()
        {
        }
        public CheckJoint(BParent _bParent, NodePosition JoinDir, Control Node) : base(_bParent, JoinDir, Node, Runtime.Token.NodeToken.Value)
        {
            nodePosition = JoinDir;
        }


        public NodePosition nodePosition;
        public override void SetDir(NodePosition value)
        {
            nodePosition = value;
        }
        public override NodePosition GetDir()
        {
            return nodePosition;
        }
        Node_Interface_Data dataDate;
        public override void Set(Node_Interface_Data value)
        {
            dataDate = value;
        }
        public override Node_Interface_Data Get()
        {
            return dataDate;
        }
        public override void Render( )
        {
            if (GetJoinType() == typeof(bool))
            {
                UINode.IsChecked = (bool)dataDate.Value;
                //UINode.Content = dataDate.Title;
            }
        }
        public CheckBox1 UINode = new CheckBox1
        {
            Classes = "el-checkbox",
            Content = "布尔值",
            Foreground = "255,255,255",
        };


        protected override void InitializeComponent()
        {
            UINode.Checked += UINode_Checked;
            base.InitializeComponent();
            base.AddControl(UINode, nodePosition);
        }

        private void UINode_Checked(object sender, EventArgs e)
        {
            dataDate.Value = !(bool)dataDate.Value;
        }
    }
    public class CheckBox1 : ToggleButton
    {
        protected override void InitializeComponent()
        {
            Children.Add(new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new Border
                    {
                        Name = "contentPresenter",
                        MarginLeft=2,
                        BorderFill=null,
                        PresenterFor=this,
                        Height="100%"
                    },
                    new Panel
                    {
                        Name="markPanel",
                        Children =
                        {
                            new Border
                            {
                                Name="checkBoxBorder",
                                Width = 13,
                                Height = 13,
                                BorderFill = "124,124,124",
                                BorderStroke= "1",
                                Background = "255,255,255",
                                UseLayoutRounding=true,//IsAntiAlias=true,
                                Child=new CPF.Shapes.Polyline
                                {
                                    Points=
                                    {
                                        new Point(2,6),
                                        new Point(6,10),
                                        new Point(12,2)
                                    },
                                    IsAntiAlias=true,
                                    StrokeStyle="1.5",
                                    Bindings =
                                    {
                                        {
                                            nameof(Visibility),
                                            nameof(IsChecked),
                                            4,
                                            BindingMode.OneWay,
                                            a=>(bool?)a==true?Visibility.Visible:Visibility.Collapsed
                                        }
                                    }
                                },
                            },
                            new Rectangle
                            {
                                Name="indeterminateMark",
                                Width = 9,
                                Height = 9,
                                Fill = "124,124,124",
                                StrokeFill=null,
                                IsAntiAlias=true,
                                Bindings =
                                {
                                    {
                                        nameof(Visibility),
                                        nameof(IsChecked),
                                        3,
                                        BindingMode.OneWay,
                                        a=>a==null?Visibility.Visible:Visibility.Collapsed
                                    }
                                }
                            },
                        },
                        Height="100%"
                    }
                }
            });
        }

    }
}
