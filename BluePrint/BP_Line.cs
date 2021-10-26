using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using 蓝图重制版.BluePrint.Node;
using 蓝图重制版.BluePrint.IJoin;

namespace 蓝图重制版.BluePrint
{
    public class BP_Line : Control
    {
        protected override void OnLayoutUpdated()
        {
            //PositionReckon();
            base.OnLayoutUpdated();
            //Visibility = Visibility.Visible;
        }
        protected override void OnRender(DrawingContext dc)
        {
            if (geometry != null)
            {
                dc.AntialiasMode = AntialiasMode.AntiAlias;
                dc.DrawPath(backound_color, new Stroke(_LineWidth), geometry);
                VisualClip = new Geometry(geometry);
            }
        }
        protected override void InitializeComponent()
        {
            //Width = 1;
            //Height = 1;
            //ClipToBounds = true;
        }
        /// <summary>
        /// 刷新路径
        /// </summary>
        public void RefreshDrawBezier() {
            var size = PositionReckon();
            if (IsSX)//上
            {
                if (IsZ)//左
                {
                    geometry = DrawBezier(new Point(0, 0), new Point(size.Width, size.Height));
                }
                else
                {//右
                    geometry = DrawBezier(new Point(size.Width, 0), new Point(0, size.Height));
                }
            }
            else
            {
                if (IsZ)
                {
                    geometry = DrawBezier(new Point(0, size.Height), new Point(size.Width, 0));
                }
                else
                {
                    geometry = DrawBezier(new Point(size.Width, size.Height), new Point(0, 0));
                }

            }
        }
        /// <summary>
        /// 计算自身尺寸，位置
        /// </summary>
        public Size PositionReckon()
        {
            var size = new CPF.Drawing.Size(0, 0);
            if (_Star != null && _End != null)
            {
                // IJoinControl.NodePosition
                Point Spos = _Star.GetPosition(_Star.GetJoinPos(_Star.GetDir()), Parent);
                Point SEnd = _End.GetPosition(_End.GetJoinPos(_End.GetDir()), Parent);
                /*if (Spos.X < 0|| Spos .Y<0 || SEnd.X < 0|| SEnd.Y<0)
                {
                    //Debug.WriteLine($"pos:Spos:{Spos}-SEnd:{SEnd}");
                    return;
                }*/
                //Debug.WriteLine($"pos:Spos:{Spos}-SEnd:{SEnd}");

                if (_Star is MouseJoin)
                {
                    Spos = _Star.ActualOffset;
                    if (Spos == new Point(0,0))
                    {
                        //Debug.WriteLine($"pos:Spos:{Spos}");
                        return size;
                    }
                    
                }
                if (_End is MouseJoin)
                {
                    SEnd = _End.ActualOffset;
                    if (SEnd == new Point(0, 0))
                    {
                        //Debug.WriteLine($"pos:Spos:{Spos}");
                        return size;
                    }
                    //Debug.WriteLine($"pos:SEnd:{SEnd}");
                }
                if (Spos.X == -1|| SEnd.X == -1)
                {
                    return size;
                }

                if (Spos.X < SEnd.X)
                {
                    Width = SEnd.X - Spos.X;
                    MarginLeft = Spos.X;
                    IsZ = true;

                    size.Width = SEnd.X - Spos.X;
                }
                else if (Spos.X > SEnd.X)
                {
                    Width = Spos.X - SEnd.X;
                    MarginLeft = SEnd.X;
                    IsZ = false;

                    size.Width = Spos.X - SEnd.X;
                }
                if (Spos.Y < SEnd.Y)
                {
                    Height = SEnd.Y - Spos.Y;
                    MarginTop = Spos.Y;
                    IsSX = true;

                    size.Height = SEnd.Y - Spos.Y;
                }
                else if (Spos.Y > SEnd.Y)
                {
                    Height = Spos.Y - SEnd.Y;
                    MarginTop = SEnd.Y;
                    IsSX = false;

                    size.Height = Spos.Y - SEnd.Y;
                }
                else
                {//==
                    Height = Spos.Y - SEnd.Y;
                    MarginTop = SEnd.Y;
                    IsSX = false;

                    size.Height = Spos.Y - SEnd.Y;
                }
            }
            return size;
        }
        private IJoinControl _Star = null;
        private IJoinControl _End = null;
        private PathGeometry geometry;
        public void SetJoin(IJoinControl a, IJoinControl b)
        {
            _Star = a;
            _End = b;
            //return this;//
            PositionReckon();
            //Invalidate();
        }
        public void SetJoin(Control a, Control b)
        {
            _Star = a as IJoinControl;
            _End = b as IJoinControl;
            PositionReckon();
            //Invalidate();
            // return this;//Control
        }

        public BP_IJoin GetStarJoin() {
            return _Star;
        }
        public BP_IJoin GetEndJoin()
        {
            return _End;
        }
        public void SetStarJoin(IJoinControl value)
        {
            _Star = value;
        }
        public void SetEndJoin(IJoinControl value)
        {
            _End = value;
        }
        public bool IsSX = true;
        public bool IsZ = true;
        /// <summary>
        /// 线段颜色
        /// </summary>
        //[UIPropertyMetadata(null, UIPropertyOptions.AffectsRender)]//属性变化之后自动刷新
        public Color backound_color = "rgb(0,169,244)";
        /// <summary>
        /// 线条宽度
        /// </summary>
        private float _LineWidth = 2;
        public float LineWidth {
            get {
                return _LineWidth;
            }
            set {
                _LineWidth = value;
            }
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            backound_color = Color.FromRgb((byte)(backound_color.R - 30), backound_color.G, backound_color.B);
            Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            backound_color = Color.FromRgb((byte)(backound_color.R + 30), backound_color.G, backound_color.B);
            Invalidate();
            base.OnMouseLeave(e);
        }
        /// <summary>
        /// 计算两点之间长度
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public float Distance(Point p1, Point p2)
        {
            //C# code
            float width = p2.X - p1.X;
            float height = p2.Y - p1.Y;
            float result = (width * width) + (height * height);
            return (float)Math.Sqrt(result);//根号
        }
        /// <summary>
        /// 绘制线条
        /// </summary>
        /// <param name="kCanvas">画布</param>
        /// <param name="cubicTopoint">起始点坐标</param>
        /// <param name="Mousepoint">结束点坐标</param>
        public PathGeometry DrawBezier(Point cubicTopoint,
            Point Mousepoint)
        {
            List<Point> sKPoints = new List<Point>();

            sKPoints.Add(new Point(Mousepoint.X, Mousepoint.Y));
            sKPoints.Add(new Point(cubicTopoint.X, cubicTopoint.Y));

            sKPoints.Add(new Point(50, cubicTopoint.Y));//控制点
            sKPoints.Add(new Point(50, cubicTopoint.Y));//控制点
            //paint1.AddPoly(sKPoints.ToArray());


            float wid = Mousepoint.X - cubicTopoint.X;


            float yiban = Distance(cubicTopoint, Mousepoint) / 3;
            if (Mousepoint.Y < cubicTopoint.Y)
            {
                float hei = cubicTopoint.Y - Mousepoint.Y;
                sKPoints[2] = new Point(cubicTopoint.X + yiban, cubicTopoint.Y);
                sKPoints[3] = new Point(Mousepoint.X - yiban, Mousepoint.Y);
            }
            else
            {
                float hei = Mousepoint.Y - cubicTopoint.Y;
                sKPoints[2] = new Point(cubicTopoint.X + yiban, cubicTopoint.Y);
                sKPoints[3] = new Point(Mousepoint.X - yiban, cubicTopoint.Y + hei);
            }

            //path1.MoveTo(sKPoints[1]);
            //path1.CubicTo(sKPoints[2], sKPoints[3], sKPoints[0]);
            //path1.QuadTo(sKPoints[2], sKPoints[0]);
            //kCanvas.DrawPath(path1, paint1);


            var p = new PathGeometry();


            p.BeginFigure(sKPoints[1].X, sKPoints[1].Y);
            p.CubicTo(sKPoints[2], sKPoints[3], sKPoints[0]);
            return p;
        }
    }
}
