using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.INode;
using 蓝图重制版.BluePrint.Join;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint
{
    public class BParent : Control
    {
        /// <summary>
        /// 设置节点上下文
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(List<Type> context) {
            _NodeTypes = context;
        }
        /// <summary>
        /// 在蓝图指定位置创建一个指定类型的节点
        /// </summary>
        /// <param name="type">节点类型</param>
        /// <param name="element">鼠标相对元素</param>
        /// <param name="point">元素内部偏移</param>
        public void CreateNode(Type type,CPF.Controls.Control element,Point point) {
            if (_NodeTypes!=null)
            {
                Point p = element.GetPosition(point, bluePrint);
                foreach (var item in _NodeTypes.Where(x => x == type).ToList())
                {
                    var Control = System.Activator.CreateInstance(item, new object[] { this });
                    (Control as UIElement).Margin = $"{p.X},{p.Y},auto,auto";
                    bluePrint.AddChildren((CPF.Controls.Control)Control);
                }
            }
        }
        /// <summary>
        /// 在蓝图0，0位置创建一个指定类型的节点
        /// </summary>
        /// <param name="type"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void CreateNode(Type type,float x1 = 0,float y1 = 0)
        {
            if (_NodeTypes != null)
            {
                foreach (var item in _NodeTypes.Where(x => x == type).ToList())
                {
                    var Control = System.Activator.CreateInstance(item, new object[] { this });
                    (Control as UIElement).Margin = $"{x1-60},{y1},auto,auto";
                    bluePrint.AddChildren((CPF.Controls.Control)Control);
                }
            }
        }
        protected override void OnRender(DrawingContext dc)
        {
            var rect = new Rect(ActualSize);
            base.OnRender(dc);

            using (var brush = XPathColor.CreateBrush(rect, Root.RenderScaling))
            {
                using (var brush1 = YPathColor.CreateBrush(rect, Root.RenderScaling))
                {
                    //dc.FillRectangle(brush, rect);

                    PathGeometry XPath = new PathGeometry();
                    PathGeometry YPath = new PathGeometry();
                    for (int i = 0; i < rect.Height; i++)
                    {
                        if (i % 12 == 0)
                        {
                            XPath.BeginFigure(0, i);
                            XPath.LineTo(rect.Width, i);
                        }
                        if (i % 84 == 0)
                        {
                            YPath.BeginFigure(0, i);
                            YPath.LineTo(rect.Width, i);
                        }
                    }

                    for (int i = 0; i < rect.Width; i++)
                    {
                        if (i % 12 == 0)
                        {
                            XPath.BeginFigure(i, 0);
                            XPath.LineTo(i, rect.Width);
                        }
                        if (i % 84 == 0)
                        {
                            YPath.BeginFigure(i, 0);
                            YPath.LineTo(i, rect.Width);
                        }
                    }

                    dc.DrawPath(brush, "1", XPath);
                    dc.DrawPath(brush1, "1", YPath);
                }
            }
        }

        private ViewFill XPathColor = Color.FromRgba(52, 52, 52, 255);
        private ViewFill YPathColor = Color.FromRgba(0, 0, 0, 150);


        public BluePrint bluePrint;
        //public Panel panel;
        protected override void InitializeComponent()
        {
            //Instances.Add(this);
            //模板定义
            ClipToBounds = true;
            base.InitializeComponent();
            Background = Color.FromRgb(39, 39, 39);
            bluePrint = new BluePrint
            {
                MarginLeft = 0f,
                MarginTop = 0f,
                //Width = "100%",
                //Height = "100%",
                Tag = 1f,
                RenderTransformOrigin = new PointField(0, 0),
            };
            //panel = new Panel
            //{
            //    MarginLeft = 0f,
            //    MarginTop = 0f,
            //    Children = { bluePrint }
            //};
            Children.Add(bluePrint);
            //添加拖动节点
            MouseJoin = new MouseJoin(this, IJoinControl.NodePosition.Left,this) {
                MarginLeft = 0f,
                MarginTop = 0f,
                Width = 1f,
                Height = 1f,
                Visibility = Visibility.Hidden
            };
            bluePrint.AddChildren(MouseJoin);
            //
            //添加默认拖动显示线条
            bP_Line = new BP_Line
            {
                MarginLeft = 0f,
                MarginTop = 0f,
                backound_color = Color.Parse("#89C4F8"),
                LineWidth = 5,
            };
            MousepanelPupopPos = new Panel
            {
                Width = 1,
                Height = 1,
                Visibility = Visibility.Hidden,
            };
            Children.Add(MousepanelPupopPos);
            //bP_Line.Size = new SizeField(1,1);AddLineChildren
            //ClearState();AddChildren
            //之所以不用AddLineChildren 是因为此线条为默认，不参与计算判断
            bluePrint.AddChildren(bP_Line);
            //ClearState();

        }

        float scale = 1;
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            Parent.Invalidate();
            var p = e.MouseDevice.GetPosition(bluePrint);
            Matrix matrix = Matrix.Identity;
            if (bluePrint.RenderTransform is MatrixTransform transform)
            {
                matrix = transform.Value;
            }
            if (e.Delta.Y < 0)
            {
                scale *= 0.8f;
                if (scale < 0.01)
                {
                    scale /= 0.8f;
                }
                else
                {
                    matrix.ScaleAtPrepend(0.8f, 0.8f, p.X, p.Y);
                }
            }
            else
            {
                scale /= 0.8f;
                matrix.ScaleAtPrepend(1 / 0.8f, 1 / 0.8f, p.X, p.Y);
            }
            bluePrint.RenderTransform = new MatrixTransform(matrix);
            base.OnMouseWheel(e);
        }
        Point? mousePos;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            ClearState();
            base.OnMouseDown(e);
            var key = Root.InputManager.KeyboardDevice.Modifiers;

            //Console.WriteLine($"key:{key}");
            //cpf接收不到alt的消息暂时用ctrl代替，等待修复
            if (e.MouseButton == MouseButton.Right &&
                key == InputModifiers.Control)
            {
                mousePos = e.Location / scale;
                CaptureMouse();
            }
            MousepanelPupopPos.MarginLeft = e.Location.X;
            MousepanelPupopPos.MarginTop = e.Location.Y;
        }
        Popup popup;
        /// <summary>
        /// 鼠标位置为了弹窗IList, ICollection
        /// </summary>
        public Panel MousepanelPupopPos;

        private List<Type> _NodeTypes = new List<Type>();
        public List<Type> NodeContextTypes
        {
            set {
                _NodeTypes = value;
            }
            get {
                return _NodeTypes;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.OnMouseUp(e);
            var key = Root.InputManager.KeyboardDevice.Modifiers;
            //Console.WriteLine($"OnMouseUp:\r\n    e.MouseButton:{e.MouseButton}\r\n    key:{key}");

            if (e.MouseButton == MouseButton.Right && 
                (key == InputModifiers.Control || key == (InputModifiers.Control | InputModifiers.MiddleMouseButton)))
            {
                //Console.WriteLine($"ReleaseMouseCapture");
                mousePos = null;
                ReleaseMouseCapture();
            }
            if (e.MouseButton == MouseButton.Right && 
                (key == InputModifiers.None || key == (InputModifiers.None | InputModifiers.MiddleMouseButton)))
            {
                //Console.WriteLine($"if (e.MouseButton == MouseButton.Right&& key == InputModifiers.None)");
                if (popup == null)
                {
                    //Console.WriteLine($"popup == null");
                    popup = new Popup
                    {
                        PlacementTarget = MousepanelPupopPos,
                        Placement = PlacementMode.Padding,
                        CanActivate = true,
                        StaysOpen = true,
                        MarginTop = -10,
                        //MarginLeft = -10,
                        Background = null,
                        Children =
                        {
                            new SearchMenuItem(NodeContextTypes,this),
                        },
                        Commands = {
                            {nameof(Popup.GotFocus),(s1,e1)=>{
                                //var a = 123;
                                //Debug.WriteLine("获取焦点");
                                //popup.Visibility = Visibility.Visible;
                                //popup.Hide();
                            }},
                            {nameof(Popup.LostFocus),(s1,e1)=>{
                                popup.Hide();
                                //var a = 123;
                                //popup.Visibility = Visibility.Hidden;
                            }}
                        },
                    };
                    popup.LoadStyleFile("res://蓝图重制版/css/ElementUI1.css");
                    popup.LoadStyleFile("res://蓝图重制版/css/icons.css", true);
                }
                Debug.WriteLine($"{popup.Visibility}");
                
                //(popup.Children[0] as SearchMenuItem).close();
                popup.Show();
                //var aa = popup.Focus(NavigationMethod.Click);

            }
        }
        /// <summary>
        /// 清空当前拖放状态
        /// </summary>
        public void ClearState() {
            ParentJoin = null; 
            IsMouseJoin = false;
            bP_Line.SetJoin(null, null);
            //bP_Line.Width = 1f;
            //bP_Line.Height = 1f;
            bP_Line.Margin = "0,0,auto,auto";
            MouseJoin.Margin = "0,0,auto,auto";
            //MouseJoin.MarginTop = -1;
            bP_Line.Visibility = Visibility.Hidden;
            MouseJoin.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 用于鼠标拖动的接口，本身只是为了拖动 
        /// </summary>
        public IJoinControl MouseJoin;// = new MouseJoin(this,IJoinControl.NodePosition.Left);
        /// <summary>
        /// 用于模拟拖动的线，不参与流程
        /// </summary>
        public BP_Line bP_Line;
        /// <summary>
        /// 上一个选中的接口
        /// </summary>
        public IJoinControl ParentJoin = null;
        /// <summary>
        /// 设置鼠标状态
        /// </summary>
        /// <param name="State"></param>
        public void SetMouseState(IJoinControl State, MouseButtonEventArgs e) {
            //IsMouseJoin = false;
            bP_Line.Invalidate();
            bP_Line.RefreshDrawBezier();
            if (ParentJoin == null)
            {
                ParentJoin = State;
                Point p = State.GetPosition(State.GetJoinPos(IJoinControl.NodePosition.Left), bluePrint);
                //MouseJoin.MarginLeft = p.X;
                //MouseJoin.MarginTop = p.Y;
                //bP_Line.MarginLeft = p.X;
                //bP_Line.MarginTop = p.Y;
                //Debug.WriteLine($"e.Location:{MouseJoin.Margin}");
                //Debug.WriteLine($"e.Location1:{bP_Line.Margin}");
                //Debug.WriteLine($"-----------------------------------------------");
                //bP_Line.SetJoin(null, null);
                if (ParentJoin.GetDir() == IJoinControl.NodePosition.Left)
                {
                    MouseJoin.SetDir(IJoinControl.NodePosition.right);
                    bP_Line.SetJoin(MouseJoin, ParentJoin);
                }
                if (ParentJoin.GetDir() == IJoinControl.NodePosition.right)
                {
                    MouseJoin.SetDir(IJoinControl.NodePosition.Left);
                    bP_Line.SetJoin(ParentJoin, MouseJoin);
                }
                
                //bP_Line.PositionReckon();
                IsMouseJoin = true;
                //MouseJoin.Visibility = Visibility.Visible;
                /*this.Delay(TimeSpan.FromSeconds(1), () => {
                    bP_Line.Visibility = Visibility.Visible;
                });*/
                bP_Line.Visibility = Visibility.Visible;

            }
            else {
                Point p = State.GetPosition(State.GetJoinPos(IJoinControl.NodePosition.Left), bluePrint);
                //输入输出一样或者父元素一样全部不可连接
                if (ParentJoin.GetDir() == State.GetDir() ||
                    ParentJoin.GetParnt() == State.GetParnt())
                {
                    ClearState();
                    UIElementTool.Toast(bluePrint, "接口不匹配||不能驲自己",p);
                }
                else {
                    //现在就可以初始化线条用于连接
                    //再判断一下两个节点是否已经有线条了

                    IJoinControl a,b;
                    
                    if (ParentJoin.GetDir() == IJoinControl.NodePosition.Left)
                    {
                        a = State;
                        b = ParentJoin;
                    }
                    else {
                        a = ParentJoin;
                        b = State;
                        //bP_Line1.SetJoin(ParentJoin, State);
                    }
                    
                    if (a.GetJoinType() == b.GetJoinType())
                    {
                        //var add = a.GetJoinType();
                        if (a.GetJoinType() == typeof(JoinType))
                        {
                            if (!bluePrint.FildIsJoinRef(a) && !bluePrint.FildIsJoinRef(b))
                            {
                                //执行线只支持一对一
                                var bP_Line1 = new BP_Line
                                {
                                    MarginLeft = 0f,
                                    MarginTop = 0f,
                                    Width = 1f,
                                    Height = 1f,
                                    backound_color = "rgb(255,255,255)",
                                };
                                bluePrint.AddLineChildren(bP_Line1);
                                bP_Line1.SetJoin(a, b);
                                //bP_Line.Invalidate();
                                //创建完成让他先刷新一下
                                bP_Line1.RefreshDrawBezier();
                            }
                            else {
                                UIElementTool.Toast(bluePrint, "流程只支持一对一", p);
                            }
                        }
                        else {
                            //剩下的是普通线条
                            if (!bluePrint.FildLine(a, b))
                            {
                                var bP_Line1 = new BP_Line
                                {
                                    MarginLeft = 0f,
                                    MarginTop = 0f,
                                    Width = 1f,
                                    Height = 1f
                                };
                                bluePrint.AddLineChildren(bP_Line1);
                                bP_Line1.SetJoin(a, b);

                                //创建完成让他先刷新一下
                                bP_Line1.RefreshDrawBezier();
                            }
                            else
                            {
                                //已经连过了
                                UIElementTool.Toast(bluePrint, "已经连过了", p);
                            }
                        }
                        
                    }
                    else {
                        //判断接口数据类型
                        if (!a.GetIsTypeCheck() || !b.GetIsTypeCheck())
                        {
                            //接口数据类型不匹配
                            UIElementTool.Toast(bluePrint, "已消除类型检查", p,0.35f);
                            //不进行强类型检测
                            var csharp的goto有限制所以只能复制一份 = "";
                            //剩下的是普通线条
                            if (!bluePrint.FildLine(a, b))
                            {
                                var bP_Line1 = new BP_Line
                                {
                                    MarginLeft = 0f,
                                    MarginTop = 0f,
                                    Width = 1f,
                                    Height = 1f
                                };
                                bluePrint.AddLineChildren(bP_Line1);
                                bP_Line1.SetJoin(a, b);

                                //创建完成让他先刷新一下
                                bP_Line1.RefreshDrawBezier();
                            }
                            else
                            {
                                //已经连过了
                                UIElementTool.Toast(bluePrint, "已经连过了", p);
                            }
                        }
                        else {
                            //接口数据类型不匹配
                            UIElementTool.Toast(bluePrint, "接口数据类型不匹配", p);
                        }
                    }
                    
                    ClearState(); 
                }
                ParentJoin = null;
            }
            //Invalidate();
        }
        /// <summary>
        /// 是否已经点击接口拖动
        /// </summary>
        public bool IsMouseJoin = false;
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        public Point MousePoint;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            MousePoint = e.Location;
            base.OnMouseMove(e);
            if (IsMouseJoin)
            {
                bP_Line.Invalidate();
                bP_Line.RefreshDrawBezier();

                var p1 = e.MouseDevice.GetPosition(bluePrint);
                MouseJoin.MarginLeft = p1.X;
                MouseJoin.MarginTop = p1.Y;
                
                //Debug.WriteLine($"e.Location11:{p}--{e.Location}");
            }
            
            if (mousePos.HasValue)
            {
                //bluePrint.MarginLeft
                var aa = e.Location.X - bluePrint.MarginLeft.Value / scale;
                var p = e.Location / scale;
                var a = p - mousePos.Value;
                Matrix matrix = Matrix.Identity;
                if (bluePrint.RenderTransform is MatrixTransform transform)
                {
                    matrix = transform.Value;
                }
                
                matrix.TranslatePrepend(a.X, a.Y);
                System.Diagnostics.Debug.WriteLine(a);
                bluePrint.RenderTransform = new MatrixTransform(matrix);
                mousePos = p;
            }
            Parent.Invalidate();
        }

    }
}
