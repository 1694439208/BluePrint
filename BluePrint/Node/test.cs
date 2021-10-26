using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.INode;

namespace 蓝图重制版.BluePrint.Node
{
    public class CheckBox1: Window
    {
        protected override void InitializeComponent()
        {
            Width = 500;
            Height = 300;
            Background = "0,0,0";
            Children.Add(new TreeView
            {
                MarginLeft = 88,
                MarginTop = 39,
                Height = 221,
                Width = 215,
                Items =
                {
                    new Join.SearchTreeViewItem
                    {
                        Header = "66666666",
                    },
                    new Join.SearchTreeViewItem
                    {
                        Header = "66666666",
                    }
                },
            });
        }

    }
}
