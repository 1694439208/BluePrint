using CPF;
using CPF.Animation;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using CPF.Shapes;
using CPF.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 蓝图重制版.BluePrint;
using 蓝图重制版.BluePrint.INode;

namespace 蓝图重制版
{
    public class Window1 : Window
    {
        protected override void InitializeComponent()
        {
            Title = "标题";
            Width = 1200;
            Height = 800;
            Background = null;
            CanResize = true;
            Children.Add(new WindowFrame(this, new Panel
            {
                Width = "100%",
                Height = "90%",
                Background = ViewFill.Parse("#ccc"),
                PresenterFor = this,
                Name = "BP1",
                ClipToBounds = true,
                Children =
                {
                    new Button
                    {
                        MarginLeft = 496f,
                        MarginTop = 34.7f,
                        Height = 28f,
                        Width = 67.4f,
                        Content = "Button",
                        Commands =
                        {
                            {
                                nameof(Button.Click),
                                nameof(test.Click)
                            }
                        }
                    }
                }
            }));

            //LoadStyleFile("res://蓝图重制版/css/Stylesheet1.css");
            LoadStyleFile("res://蓝图重制版/css/ElementUI1.css");
            LoadStyleFile("res://蓝图重制版/css/icons.css", true);
            //LoadStyleFile("res://蓝图重制版/css/icons.css", true);


            //加载样式文件，文件需要设置为内嵌资源

            if (!DesignMode)//设计模式下不执行
            {
                var pan = FindPresenterByName<Panel>("BP1");
                var bp = new BluePrint.BParent
                {
                    MarginLeft = 0f,
                    MarginTop = 0f,
                    Width = "100%",
                    Height = "100%",
                };
                pan.Children.Add(bp);
                //NodeTypes.Add(typeof(_StartNode));
                //NodeTypes.Add(typeof(Branch));
                //设置节点上下文
                bp.SetContext(new List<Type>{
                    typeof(_StartNode),
                    typeof(Branch),
                    typeof(ImageShow),
                    typeof(ImageSplit),
                    typeof(sequence),
                    typeof(GreaterThan),
                    typeof(ScriptType),
                    typeof(CreateVar),
                    typeof(StrAppend),
                    typeof(GetVar),
                });

                bp.bluePrint.AddChildren(new _StartNode(bp)
                {
                    MarginTop = 0,
                    MarginLeft = 0,
                });
                bp.bluePrint.AddChildren(new ImageSplit(bp)
                {
                    MarginTop = 0,
                    MarginLeft = 0,
                });
                for (int i = 0; i < 3; i++)
                {
                    bp.bluePrint.AddChildren(new ImageShow(bp)
                    {
                        MarginTop = 0,
                        MarginLeft = 0,
                    });
                }
                bp.bluePrint.AddChildren(new sequence(bp)
                {
                    MarginTop = 0,
                    MarginLeft = 0,
                });
                bp.bluePrint.AddChildren(new Branch(bp)
                {
                    MarginTop = 0,
                    MarginLeft = 0,
                });

                //var line = new BP_Line ImageShow
                //{
                //    MarginLeft = 0f,
                //    MarginTop = 0f,
                //    Width = 1f,
                //    Height = 1f
                //};
                //var line1 = new BP_Line
                //{
                //    MarginLeft = 0f,
                //    MarginTop = 0f,
                //    Width = 1f,
                //    Height = 1f
                //};
                //var line2 = new BP_Line
                //{
                //    MarginLeft = 0f,
                //    MarginTop = 0f,
                //    Width = 1f,
                //    Height = 1f
                //};
                //bp.bluePrint.AddLineChildren(line);
                //bp.bluePrint.AddLineChildren(line1);
                //bp.bluePrint.AddLineChildren(line2);
                ////blist[0].AddIJoin(NodeContext.NodePosition.right, line);
                ////输出
                ////blist[1].AddIJoin(NodeContext.NodePosition.Left, line);
                ////输入
                //line.SetJoin(blist[0].OuPutIJoin[0], blist[1].InPutIJoin[1]);
                //line1.SetJoin(blist[0].OuPutIJoin[0], blist[1].InPutIJoin[0]);
                //line2.SetJoin(blist[0].OuPutIJoin[1], blist[1].InPutIJoin[0]);

                //bp.bluePrint.RemoveNode(blist[1]);
                //bp.bluePrint.RemoveLine(line1);

            }
        }
    }

}
