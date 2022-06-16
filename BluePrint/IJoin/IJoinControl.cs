using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using CPF.Svg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using 蓝图重制版.BluePrint.DataType;
using 蓝图重制版.BluePrint.IJoin;
using static 蓝图重制版.BluePrint.Runtime.Token;

namespace 蓝图重制版.BluePrint.Node
{
    public class IJoinControl : Control,BP_IJoin
    {
        /*/// <summary>
        /// 添加一个子节点
        /// </summary>
        /// <param name="uIElement"></param>
        public void Add(UIElement uIElement) {
            Children.Add(uIElement);
        }
        /// <summary>
        /// 接口的提示控件
        /// </summary>
        public ToastControl toast;*/
        /// <summary>
        /// 指示接口是否可以连线
        /// </summary>
        public bool IsConnect = true;
        /// <summary>
        /// 序列化用的id
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 设置接口状态
        /// </summary>
        /// <param name="isc"></param>
        public void SetIsConnectState(bool isc) {
            IsConnect = isc;
        }
        /// <summary>
        /// 读取接口状态
        /// </summary>
        /// <returns></returns>
        public bool GetIsConnectState()
        {
            return IsConnect;
        }
        /// <summary>
        /// 设置接口方向
        /// </summary>
        /// <param name="position"></param>
        public virtual void SetDir(NodePosition position) { _position = position; }
        /// <summary>
        /// 获取接口方向，默认左边
        /// </summary>
        /// <returns></returns>
        public virtual NodePosition GetDir() { return _position; }
        public enum NodePosition
        {
            Left, right
        };
        /// <summary>
        /// 设计器使用的构造函数
        /// </summary>
        public IJoinControl()//为了设计器用的
        {
        }
        NodeToken _nodeToken = NodeToken.Call;
        public NodeToken GetNodeType() {
            return _nodeToken;
        }
        public void SetNodeType(NodeToken nodeToken)
        {
            _nodeToken = nodeToken;
        }
        public IJoinControl(BParent _bParent, NodePosition position,Control Node) {
            bParent = _bParent;
            _position = position;
            _Node = Node;
        }
        public IJoinControl(BParent _bParent, NodePosition position, Control Node, NodeToken nodeToken)
        {
            bParent = _bParent;
            _position = position;
            _Node = Node;
            _nodeToken = nodeToken;
        }
        /// <summary>
        /// 获取父元素节点引用
        /// </summary>
        /// <returns></returns>
        public virtual Control Get_NodeRef() { return _Node; }
        Control _Node;
        BParent bParent;
        public NodePosition _position;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            e.Handled = true;
        }
        /// <summary>
        /// 接口数据类型
        /// </summary>
        private Type JoinType;
        /// <summary>
        /// 是否强类型检查
        /// </summary>
        private bool IsTypeCheck;
        /// <summary>
        /// 设置接口数据类型
        /// </summary>
        /// <param name="type"></param>
        public virtual void SetType(Node_Interface_Data type) {
            JoinType = type.Type;
            IsTypeCheck = type.IsTypeCheck;
            if (type.Tips != "")
            {
                ToolTip = type.Tips;
            }
        }
        /// <summary>
        /// 读取接口数据类型
        /// </summary>
        /// <param name="type"></param>
        public virtual Type GetJoinType()
        {
            return JoinType;
        }
        public virtual bool GetIsTypeCheck()
        {
            return IsTypeCheck;
        }
        Node_Interface_Data _Data;
        /// <summary>
        /// 设置接口数据
        /// </summary>
        /// <param name="value"></param>
        public virtual void Set(Node_Interface_Data value) { _Data = value; }

        /// <summary>
        /// 读取接口数据
        /// </summary>
        /// <returns></returns>
        public virtual Node_Interface_Data Get() { return _Data; }

        /// <summary>
        /// 设置接口参数Name
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetNmae(string name) { __Nmae = name; }

        /// <summary>
        /// 读取接口数据
        /// </summary>
        /// <returns></returns>
        public virtual string GetName() { return __Nmae; }
        /// <summary>
        /// 接口==变量  变量名称
        /// </summary>
        public string __Nmae = "";
        /// <summary>
        /// 渲染数据内容
        /// </summary>
        public virtual void Render() {}
        
        public UIElement GetParnt()
        {
            return _Node;
        }
        public Point GetPos(bool IsZ)
        {
            if (IsZ)
            {
                return TransformPoint(new Point(0,ActualSize.Height / 2));
            }
            return TransformPoint(new Point(ActualSize.Width, ActualSize.Height / 2));
        }
        /// <summary>
        /// 获取接头的坐标
        /// </summary>
        /// <returns></returns>
        public Point GetJoinPos(IJoinControl.NodePosition dir)
        {
            if (dir == NodePosition.Left)
            {
                return new Point(0,ActualSize.Height/2);//B_Join.TransformPoint(new Point(0, B_Join.Height.Value / 2));
            }
            else {
                return new Point(ActualSize.Width, ActualSize.Height/2); ;// B_Join.TransformPoint(new Point(B_Join.Width.Value, B_Join.Height.Value / 2));
            }
        }
        public Control GetThis()
        {
            return this;
        }

        FloatField Interface_size = 20;
        FloatField Interface_top = "auto";

        DockPanel B_StackPanel;
        Border B_Join;
        /// <summary>
        /// 读取接头图标元素引用
        /// </summary>
        /// <returns></returns>
        public Border GetJoinRef() {
            return B_Join;
        }
        protected override void InitializeComponent()
        {
            MarginTop = 3;
            Classes = "IJoinControl";
            B_Join = new Border
            {
                Width = Interface_size,
                Height = Interface_size,
                BorderType = BorderType.BorderThickness,
                BorderThickness = new Thickness(1, 1, 1, 1),
                BorderFill = "red",
                Size = "16,16",
                //Padding = "10,10,10,10",
                Commands =
                {
                    {
                        nameof(MouseDown),
                        (s,e)=>{
                            (s as Border).BorderFill = "#d4d4d4";
                            //(s as Border).CaptureMouse();
                        }
                    },
                    {
                        nameof(MouseUp),
                        (s,e)=>{
                            (s as Border).BorderFill = "red";
                            //(s as Border).ReleaseMouseCapture();
                            //(s as Border).CaptureMouse();
                            if((e as MouseButtonEventArgs).MouseButton == MouseButton.Left){
                                if (GetIsConnectState())//如果接口禁止连线那就不发送事件 用于禁止连线
                                {
                                    bParent.SetMouseState(this,e as MouseButtonEventArgs);
                                }
                                Debug.WriteLine("抬起鼠标");
                            }
                            if((e as MouseButtonEventArgs).MouseButton == MouseButton.Right){

                                ContextMenu = new ContextMenu(){
                                     Width = 100,
                                     Items = new List<MenuItem>(){
                                        new MenuItem{
                                            Classes = "ContextMenu1",
                                            Header = "断开连接",
                                            Commands = {
                                                {nameof(MenuItem.MouseUp),(s1,e1)=>{
                                                    if (_position == NodePosition.Left)
                                                    {
                                                        
                                                        var lines = this.bParent.bluePrint.FildIutJoin(this);
                                                        foreach (var item in lines)
                                                        {
                                                            this.bParent.bluePrint.RemoveLine(item);

                                                        }
                                                    }else{
                                                        var lines = this.bParent.bluePrint.FildOutJoin(this);
                                                        foreach (var item in lines)
                                                        {
                                                            this.bParent.bluePrint.RemoveLine(item);

                                                        }
                                                    }
                                                    
                                                }}
                                            },
                                        },
                                        new MenuItem{
                                            Classes = "ContextMenu1",
                                            Header = "操作",
                                            Items = new List<MenuItem>(){
                                                    new MenuItem{
                                                    Classes = "ContextMenu1",
                                                    Header = "删除当前节点",
                                                    Commands = {
                                                        {nameof(MenuItem.MouseUp),(s1,e1)=>{
                                                            this.bParent.bluePrint.RemoveNode(_Node);
                                                            this.bParent.ClearState();
                                                            //Get_NodeRef
                                                        }}
                                                    },
                                                },
                                            },
                                        },
                                     },
                                };
                            }

                        }
                    }
                },
                Child = new SVG
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
                        IsAntiAlias = true,
                        Fill = "136,214,56",
                        Size = "16,16",
                        Stretch = Stretch.Uniform,
                        Source = "<svg><path d=\"m1,104l0,0c0,-56.88532 46.11468,-103 103,-103l0,0c27.31729,0 53.51575,10.85175 72.83198,30.16799c19.31625,19.31625 30.16802,45.5147 30.16802,72.832l0,0c0,56.88532 -46.11468,103 -103,103l0,0c-56.88532,0 -103,-46.11468 -103,-103zm51.5,0l0,0c0,28.44265 23.05735,51.5 51.5,51.5c28.44267,0 51.5,-23.05734 51.5,-51.5c0,-28.44265 -23.05734,-51.5 -51.5,-51.5l0,0c-28.44265,0 -51.5,23.05735 -51.5,51.5z\" p-id=\"1199\"></path></svg>"
                },
            };
            B_StackPanel = new DockPanel
            {
                //Orientation = Orientation.Horizontal,
                Children = { B_Join },
            };
            Children.Add(B_StackPanel);


            if (_position == NodePosition.Left)
            {
                B_Join.Attacheds.Add(DockPanel.Dock, Dock.Left);
                B_Join.MarginLeft = 0;
            }
            else {
                B_Join.Attacheds.Add(DockPanel.Dock, Dock.Right);
                //B_Join.MarginRight = 0;
            }
        }
        /// <summary>
        /// 设置控件连线之后可视true代表连接之后继续显示但禁用，反之false
        /// </summary>
        /// <returns></returns>
        public bool Enabled = true;
        /// <summary>
        /// 设置是否允许控件禁用
        /// </summary>
        /// <returns></returns>
        public bool IsEnabledd = true;
        /// <summary>
        /// 设置控件禁用
        /// </summary>
        /// <param name="ise">false禁用 true反之</param>
        public void SetEnabled(bool ise) {
            if (IsEnabledd)
            {
                Body.IsEnabled = ise;
                if (Enabled)
                {
                    return;
                }
            }
            /*if (ise)
            {
                Body.Visibility = Visibility.Visible;
            }
            else {
                //Body.Visibility = Visibility.e;
            }*/
            
        }
        private UIElement Body;
        public void AddControl(UIElement control,NodePosition position) {
            if (control)
            {
                Body = control;
                B_StackPanel.Children.Add(control);
            }
            if (position == NodePosition.Left)
            {
                MarginLeft = 0;
            }
            else {
                MarginRight = 0;
            }
            
            //Children.Add(control);
        }

        public virtual Dictionary<string, object> Dump()
        {
            return new Dictionary<string, object>();
        }

        public virtual void Load(Dictionary<string, object> data)
        {
            if (data!=null)
            {
                foreach (var item in data)
                {
                    this.SetPropretyValue(item.Key, item.Value);
                }
            }
        }

        public delegate void JoinEvent(EveType eveType, object eventArgs);
        
        
    }
}
