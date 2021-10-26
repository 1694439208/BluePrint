﻿using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.DataType;
using 蓝图重制版.BluePrint.IJoin;

namespace 蓝图重制版.BluePrint.Node
{
    public class ValueText : IJoinControl
    {
        public ValueText() : base()
        {
        }
        public ValueText(BParent _bParent, NodePosition JoinDir, Control Node) :base(_bParent, JoinDir, Node, Runtime.Token.NodeToken.Value)
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
        public Node_Interface_Data __value;
        public override void Set(Node_Interface_Data value)
        {
            __value = value;
            if (GetJoinType() == typeof(Data_Bitmap))
            {
                text1.Text = (__value.Value as Data_Bitmap).Title;
            }
            if (GetJoinType() == typeof(bool))
            {
                text1.Text = __value.Value.ToString();
            }
        }
        public override Node_Interface_Data Get()
        {
            return __value;
        }
        public TextBlock text1 = new TextBlock
        {
            Text = "文本a11111111",
            Foreground = Color.FromRgb(255, 255, 255),
        };
        /*protected override void Initial0izeComponent()
        {
            Children.Add(new Border { 
                MarginLeft = 0,
                MarginTop = "auto",
                Width = 10,
                Height = 10,
                BorderType = BorderType.BorderThickness,
                BorderThickness = new Thickness(1,1, 1, 1),
                BorderFill = "red",
                Padding = "10,10,10,10",
            });
           
            //Background = Color.FromRgb(81, 137, 255);
        }*/
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            base.AddControl(text1, nodePosition);
        }
    }
}
