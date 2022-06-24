using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using System;
using System.Collections.Generic;
using 蓝图重制版.BluePrint.IJoin;
using 蓝图重制版.BluePrint.Node;
using System.Linq;

namespace 蓝图重制版.BluePrint.INode
{
    //[NodeBaseInfo("控件基类","基类")]
    public class NodeBase : Control,Context
    {
        /// <summary>
        /// 设计器专用
        /// </summary>
        public NodeBase()
        {
        }
        public NodeBase(BParent _bParent) {
            bParent = _bParent;
        }
        public BParent bParent;
        public List<(IJoinControl, Node_Interface_Data)> _IntPutJoin = new List<(IJoinControl, Node_Interface_Data)>();
        List<(IJoinControl, Node_Interface_Data)> Context.IntPutJoin { 
            get{
                return _IntPutJoin;
            }
            set {
                value = _IntPutJoin;
            }
        }
        public List<(IJoinControl, Node_Interface_Data)> _OutPutJoin = new List<(IJoinControl, Node_Interface_Data)>();
        List<(IJoinControl, Node_Interface_Data)> Context.OutPutJoin {
            get {
                return _OutPutJoin;
            }
            set
            {
                value = _OutPutJoin;
            }
        }

        /// <summary>
        /// 序列化用的id
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 计算节点
        /// </summary>
        /// <param name="Context">执行上下文</param>
        /// <param name="arguments">参数</param>
        /// <param name="result">返回</param>
        public virtual void Execute(object Context,List<object> arguments, in Runtime.Evaluate.Result result) {


            //默认动作 ，输出所有连接
            for (int i = 0; i < result.GetNextNodeSize(); i++)
            {
                result.SetExecute(i);
            }
        }
        /// <summary>
        /// 代码生成模板
        /// </summary>
        /// <param name="Execute">下一个流程的代码数组</param>
        /// <param name="PrevNodes">上一个流程的代码数组</param>
        /// <param name="arguments">参数变量名数组</param>
        /// <param name="result">返回的变量数组</param>
        /// <returns></returns>
        public virtual string CodeTemplate(List<string> Execute, List<string> PrevNodes, List<ParameterAST> arguments, List<ParameterAST> result)
        {
            return "";
        }
        /*
         *             
return $@"
if({arguments[0]} > {arguments[1]}){{
    {Execute[0]}
}}else{{
    {Execute[1]}
}}
{result[0]} = {arguments[0]} > {arguments[1]}
";
         * /// <summary>
        /// 计算节点
        /// </summary>
        public virtual void Execute()
        {
            //输入节点  _IntPutJoin 可以直接读取所有输入节点数据
            foreach (var item in _IntPutJoin)
            {
                item.Item1.Render();
            }
            //执行下一个连接节点
            if (_OutPutJoin.Count > 0 && _OutPutJoin[0].Item1.GetJoinType() == typeof(JoinType))
            {
                var line = bParent.bluePrint.FildOutJoin(_OutPutJoin[0].Item1);
                foreach (var item in line)
                {
                    var join = (item.GetEndJoin() as IJoinControl);
                    (join.Get_NodeRef() as Context).Execute();
                }
            }
        }*/

        protected override void InitializeComponent()
        {
            ClipToBounds = true;
            CornerRadius = "3.8";
            Background = Color.FromRgb(35, 38, 35);
            BorderFill = "#000";
            BorderStroke = "1";
            
            var stack = new DockPanel
            {
                Children =
                {
                    new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Attacheds =
                        {
                            {DockPanel.Dock,Dock.Left}
                        },
                        MarginTop = TitleHeight + 5,
                        //MarginRight = 20,
                    },
                    new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Attacheds =
                        {
                            {DockPanel.Dock,Dock.Right}
                        },
                        MarginLeft = 20,
                        MarginTop = TitleHeight + 5,
                    },
                }
            };
            InPutIJoin = stack.Children[0] as StackPanel;
            OuPutIJoin = stack.Children[1] as StackPanel;
            Children.Add(stack);
            RefreshNodes();
        }
        public StackPanel OuPutIJoin;
        public StackPanel InPutIJoin;

        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            //base.OnGotFocus(e);
            BorderFill = Color.FromRgb(197, 131, 35);
            BorderStroke = "1";
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            //base.OnLostFocus(e);
            BorderFill = Color.FromRgb(35, 38, 35);
            BorderStroke = "1";
        }
        protected override void OnLayoutUpdated()
        {
            base.OnLayoutUpdated();
            RefreshDrawBezier();
            //IsKeyboardFocusWithin
        }
        public string Title = "";
        public void RefreshNodes()
        {
            Children.Add(new Title
            {
                IsAntiAlias = true,
                Width = "100%",
                Height = TitleHeight,
                MarginLeft = 0,
                MarginTop = 0,
                title = Title,
            });
            foreach (var item in _IntPutJoin)
            {
                /*item.Item1.SetType(item.Item2);
                item.Item1.Set(item.Item2);
                item.Item1.Render();
                item.Item1.Attacheds.Add(DockPanel.Dock, Dock.Left);
                InPutIJoin.Children.Add(item.Item1);*/
                AddIntPut(item);
            }
            foreach (var item in _OutPutJoin)
            {
                /*item.Item1.SetType(item.Item2);
                item.Item1.Set(item.Item2);
                item.Item1.Render();
                item.Item1.Attacheds.Add(DockPanel.Dock, Dock.Right);
                OuPutIJoin.Children.Add(item.Item1);*/
                AddOntPut(item);
            }
            InvalidateArrange();
        }
        /// <summary>
        /// 添加输入节点
        /// </summary>
        /// <param name="JoinControl">要插入的元素</param>
        /// <param name="join">指定元素位置插入</param>
        /// <param name="pos">true 之前，false 之后</param>
        /// <param name="IsAddList">是否添加到接口数据列表用于管理</param>
        public void AddIntPut((IJoinControl, Node_Interface_Data) JoinControl, IJoinControl join = null,bool pos = true,bool IsAddList = false) {
            JoinControl.Item1.Index = intput_index++;
            JoinControl.Item1.SetType(JoinControl.Item2);
            JoinControl.Item1.Set(JoinControl.Item2);
            JoinControl.Item1.Render();
            JoinControl.Item1.Attacheds.Add(DockPanel.Dock, Dock.Left);

            if (join != null)
            {
                var index = InPutIJoin.Children.FindIndex(j => j == join);
                if (index==-1)
                {
                    throw new Exception("简单来说动态插入的接头要和插入他的元素同一个方向，也就是谁点击插入就是谁，接口列表没有此接口，可能已经删除？");
                }
                if (!pos)
                {
                    index += 1;
                }
                if (IsAddList)
                {
                    _IntPutJoin.Insert(index, JoinControl);
                }
                InPutIJoin.Children.Insert(index, JoinControl.Item1);

            }
            else {
                InPutIJoin.Children.Add(JoinControl.Item1);
            }  
        }
        public void ClearIntPut()
        {
            foreach (var item in InPutIJoin.Children.Where(a => a is IJoinControl).ToArray())
            {
                InPutIJoin.Children.Remove(item);
            }
        }
        public void ClearOntPut()
        {
            foreach (var item in OuPutIJoin.Children.Where(a => a is IJoinControl).ToArray())
            {
                OuPutIJoin.Children.Remove(item);
            }
        }
        int intput_index = 0;
        int output_index = 0;
        /// <summary>
        /// 添加输入节点
        /// </summary>
        /// <param name="JoinControl">要插入的元素</param>
        /// <param name="join">指定元素位置插入</param>
        /// <param name="pos">true 之前，false 之后</param>
        /// <param name="IsAddList">是否添加到接口数据列表用于管理</param>
        public void AddOntPut((IJoinControl, Node_Interface_Data) JoinControl, IJoinControl join = null, bool pos = true, bool IsAddList = false)
        {
            JoinControl.Item1.Index = output_index++;
            JoinControl.Item1.SetType(JoinControl.Item2);
            JoinControl.Item1.Set(JoinControl.Item2);
            JoinControl.Item1.Render();
            JoinControl.Item1.Attacheds.Add(DockPanel.Dock, Dock.Right);

            if (join != null)
            {
                var index = OuPutIJoin.Children.FindIndex(j => j == join);
                if (index == -1)
                {
                    throw new Exception("接口列表没有此接口，可能已经删除？");
                }
                if (!pos)
                {
                    index += 1;
                }
                if (IsAddList)
                {
                    _OutPutJoin.Insert(index, JoinControl);
                }
                OuPutIJoin.Children.Insert(index, JoinControl.Item1);

            }
            else
            {
                OuPutIJoin.Children.Add(JoinControl.Item1);
            }
        }
        int TitleHeight = 25;
        
        [PropertyChanged(nameof(IsKeyboardFocusWithin))]
        void OnIsKeyboardFocusWithin(object newValue, object oldValue, PropertyMetadataAttribute attribute)
        {
            /*if ((bool)newValue)
            {
                BorderFill = Color.FromRgb(197, 131, 35);
                BorderStroke = "1";
            }
            else {
                BorderFill = Color.FromRgb(35, 38, 35);
                BorderStroke = "1";
            }
            */
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.MouseButton == MouseButton.Left)
            {
                if (!IsKeyboardFocusWithin)
                {
                    Focus(NavigationMethod.Click);
                }
                
                Mouxy = e.Location;
                isclick = true;
                (Parent as BluePrint).SelectThisInstance(this);
                CaptureMouse();
                //ZIndex = Parent.GetChildren();
            }
            
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            e.Handled = true;//吃掉鼠标消息不让他冒泡
            if (e.LeftButton == MouseButtonState.Released)
            {
                isclick = false;
                ReleaseMouseCapture();
            }
        }
        private Point Mouxy;
        private bool isclick = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //e.Handled = true;
            if (e == null)
            {
                RefreshDrawBezier();
                //为了刷新溢出了的线条
                Parent.Parent.Invalidate();
                MarginLeft += 1;
                MarginTop += 1;
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed && isclick)
            {
                RefreshDrawBezier();
                //为了刷新溢出了的线条
                Parent.Parent.Invalidate();
                var a = e.Location - Mouxy;
                MarginLeft += a.X;
                MarginTop += a.Y;
                // TransformPoint
                
            }
            
        }
        /// <summary>
        /// 通知此节点接口线条引用刷新
        /// </summary>
        public void RefreshDrawBezier() {
            foreach (var item in _IntPutJoin)
            {
                var lines = bParent.bluePrint.FildIutJoin(item.Item1);
                foreach (var line in lines)
                {
                    line.RefreshDrawBezier();
                    line.Invalidate();
                }
            }
            foreach (var item in _OutPutJoin)
            {
                var lines = bParent.bluePrint.FildOutJoin(item.Item1);
                foreach (var line in lines)
                {
                    line.RefreshDrawBezier();
                    line.Invalidate();
                }
            }
        }

        

        public enum NodePosition
        {
            Left, right
        }
    }
}
