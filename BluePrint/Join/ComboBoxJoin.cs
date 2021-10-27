﻿using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.Join
{
    public class ComboBoxJoin : IJoinControl
    {
        public ComboBoxJoin() : base()
        {
        }
        public ComboBoxJoin(BParent _bParent, NodePosition JoinDir, Control Node) : base(_bParent, JoinDir, Node, Runtime.Token.NodeToken.Value)
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
        public override void Render()
        {
            if (GetJoinType() == typeof(List<string>))
            {
                UINode.Items = (List<string>)dataDate.Value;
                UINode.SelectedIndex = 0;
                //UINode.Content = dataDate.Title;
            }
        }
        public ComboBox UINode = new ComboBox
        {
            Classes = "el-textbox",
            Height = 24f,
            Width = 123.2f,
            SelectedIndex = 0,
            Items =
            {
                "test",
                "test1",
            }
        };


        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            base.AddControl(UINode, nodePosition);
        }
    }
}
