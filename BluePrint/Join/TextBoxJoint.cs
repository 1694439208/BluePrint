using CPF.Controls;
using CPF.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;

namespace 蓝图重制版.BluePrint.Node
{
    public class TextBoxJoint : IJoinControl
    {

        public TextBoxJoint() : base()
        {
        }
        public TextBoxJoint(BParent _bParent, NodePosition JoinDir, Control Node) : base(_bParent, JoinDir, Node, Runtime.Token.NodeToken.Value)
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
        Node_Interface_Data textBoxDate;
        public override void Set(Node_Interface_Data value)
        {
            textBoxDate = value;
            UINode.Text = textBoxDate.Value.ToString();
;
        }
        public override Node_Interface_Data Get()
        {
            textBoxDate.Value = Convert.ChangeType(UINode.Text, GetJoinType());
            return textBoxDate;
        }
        public TextBox UINode = new TextBox
        {
            Text = "test",
            Foreground = Color.FromRgb(255, 255, 255),
            Width = 60,
            //Height = 30,
            //HScrollBarVisibility = ScrollBarVisibility.Hidden,
            //VScrollBarVisibility = ScrollBarVisibility.Hidden,
            BorderFill = "rgb(220, 220, 220)",
            //TextAlignment = TextAlignment.Center,
            Classes = { "single" },
            Commands =
            {
                {
                    nameof(TextBox.MouseEnter),
                    (s,e)=>{
                        (s as TextBox).BorderStroke = new Stroke(1);
                    }
                },
                {
                    nameof(TextBox.MouseLeave),
                    (s,e)=>{
                        (s as TextBox).BorderStroke = new Stroke(0);
                    }
                }
            }
            //BorderFill: rgb(220, 220, 220);
            //BorderStroke: 1;
        };
        
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            base.AddControl(UINode, nodePosition);
        }
    }
}
