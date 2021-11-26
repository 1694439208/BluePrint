using CPF;
using CPF.Controls;
using CPF.Input;
using CPF.Svg;
using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.INode;
using 蓝图重制版.BluePrint.Node;

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace 蓝图重制版.BluePrint.IJoin
{
    
    public class ExecJoin : IJoinControl
    {
        public ExecJoin() : base()
        {
        }
        public ExecJoin(BParent _bParent, NodePosition JoinDir, Control Node,bool isB = false) : base(_bParent, JoinDir,Node, Runtime.Token.NodeToken.Call)
        {
            nodePosition = JoinDir;
            _Node = Node;
            IsButton = isB;
            bParent = _bParent;
        }
        BParent bParent;


        //public override Control Get_NodeRef() { return base.Get_NodeRef(); }
        public bool IsButton = false;
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

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            var b = base.GetJoinRef();
            b.BorderThickness = new Thickness(0, 0, 0, 0);
            b.Width = 16;
            b.Height = 16;
            b.Child = new SVG
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
                ToolTip = title.Value,
                IsAntiAlias = true,
                Fill = "#FFFFFF",
                Size = "16,16",
                Stretch = Stretch.Uniform,
                Source = "<svg ><path d=\"m0,0l133.09092,0l110.90908,129.85546l-110.90908,129.85545l-133.09092,0l0,-259.71091z\" p-id=\"1199\"></path></svg>"
            };

            if (IsButton)
            {
                UINode = new Button
                {
                    Width = 60f,
                    Content = "开始执行",
                };
                (UINode as Button).Click += (s, e) => {
                    //(_Node as Context).Execute();
                    //var a = new Runtime.NodeParse(bParent);
                    //var ast = a.Parser(_Node as NodeBase);
                    ////Runtime.Evaluate.Eval(ast);
                    //var code = Runtime.CodeGenerator.Generator(ast);
                    //System.Diagnostics.Debug.WriteLine(code);
                    //ToSZArray

                    CPF.Skia.SkiaPdf.CreatePdf(Root,"蓝图.pdf");
                };
            }
            base.AddControl(UINode, nodePosition);
        }
    }
}
