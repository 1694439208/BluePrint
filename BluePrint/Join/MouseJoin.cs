﻿using CPF.Controls;
using CPF.Input;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;

namespace 蓝图重制版.BluePrint.Node
{
    class MouseJoin : IJoinControl
    {
        public MouseJoin(BParent _bParent, NodePosition JoinDir, Control Node) : base(_bParent, JoinDir, Node, Runtime.Token.NodeToken.None)
        {
            nodePosition = JoinDir;
        }

        public NodePosition nodePosition;

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
        }
        protected override void InitializeComponent() {
            base.InitializeComponent();
        }
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

        }
        public override Node_Interface_Data Get()
        {
            return new Node_Interface_Data
            { 
                Title = "MouseJoin",
            };
        }
       
    }
}
