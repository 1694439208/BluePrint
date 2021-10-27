﻿using CPF;
using CPF.Animation;
using CPF.Controls;
using CPF.Drawing;
using CPF.Input;
using CPF.Shapes;
using CPF.Styling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using 蓝图重制版.BluePrint.INode;

namespace 蓝图重制版.BluePrint
{
    public class BluePrint : Control
    {
        protected override void OnRender(DrawingContext dc)
        {

        }
        /// <summary>
        /// 更新当前焦点控件的z轴，也就是渲染置顶
        /// </summary>
        /// <param name="control"></param>
        public void SelectThisInstance(Control control)
        {

            Instances.Remove(control);
            Instances.Add(control);
            for (int i = 0; i < Instances.Count; i++)
            {
                Instances[i].ZIndex = i;
            }
        }
        /// <summary>
        /// 蓝图添加节点
        /// </summary>
        /// <param name="control"></param>
        public void AddChildren(Control control)
        {
            Instances.Add(control);
            Children.Add(control);
        }
        /// <summary>
        /// 蓝图添加节点
        /// </summary>
        /// <param name="control"></param>
        public void AddChildren1(Control control)
        {
            Children.Add(control);
        }
        /// <summary>
        /// 蓝图添加线节点
        /// </summary>
        /// <param name="control"></param>
        public void AddLineChildren(Control control)
        {
            AddChildren(control);
            Lines.Add(control as BP_Line);
        }
        /// <summary>
        /// 查询两个接口是否已经连接
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public bool FildLine(Control Star, Control End)
        {
            return Lines.Where(x => x.GetStarJoin() == Star&&x.GetEndJoin() == End).Count() > 0;
        }
        /// <summary>
        /// 查询输出接口的所有线条引用
        /// </summary>
        /// <param name="Star"></param>
        /// <returns></returns>
        public List<BP_Line> FildOutJoin(Control Star)
        {
            return Lines.Where(x => x.GetStarJoin() == Star).ToList();
        }
        /// <summary>
        /// 查询接口是否已经有引用
        /// </summary>
        /// <param name="Star"></param>
        /// <returns></returns>
        public bool FildIsJoinRef(Control Star)
        {
            if (Lines.Where(x => x.GetStarJoin() == Star).Count() > 0)
            {
                return true;
            }
            else if (Lines.Where(x => x.GetEndJoin() == Star).Count() > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 查询输入接口的所有线条引用
        /// </summary>
        /// <param name="Star"></param>
        /// <returns></returns>
        public List<BP_Line> FildIutJoin(Control Star)
        {
            return Lines.Where(x => x.GetEndJoin() == Star).ToList();
        }
        /// <summary>
        /// 从蓝图删除节点
        /// </summary>
        /// <param name="control"></param>
        public void RemoveNode(Control control) {
            //这个要重新做一下，遍历要删除节点的接口，再删除线条节点
            //Lines.Remove(control);
            var context = (control as Context);
            foreach (var item in context.OutPutJoin)
            {
                var Ls = Lines.Where(x => x.GetStarJoin() == item.IJoin).ToList();
                foreach (var item1 in Ls)
                {
                    Instances.Remove(item1);
                    Children.Remove(item1);
                    Lines.Remove(item1);
                }
            }
            foreach (var item in context.IntPutJoin)
            {
                var Ls = Lines.Where(x => x.GetEndJoin() == item.IJoin).ToList();
                foreach (var item1 in Ls)
                {
                    Instances.Remove(item1);
                    Children.Remove(item1);
                    Lines.Remove(item1);
                }
            }
            //目前只实现了单节点删除，于他关联的节点接口还没处理，有问题后面处理
            Instances.Remove(control);
            Children.Remove(control);
        }
        /// <summary>
        /// 删除线
        /// </summary>
        /// <param name="control"></param>
        public void RemoveLine(Control control) {
            var line = (BP_Line)control;
            ((Node.IJoinControl)line.GetEndJoin()).SetEnabled(true);

            //((Node.IJoinControl)line.GetEndJoin()).Focusable = false;
            Instances.Remove(control);
            Children.Remove(control);
            Lines.Remove(control as BP_Line);
        }
        /// <summary>
        /// 所有节点的列表记录
        /// </summary>
        List<Control> Instances = new List<Control>();
        /// <summary>
        /// 蓝图内所有节点的线
        /// </summary>
        List<BP_Line> Lines = new List<BP_Line>();

        protected override void InitializeComponent()
        {

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            return;
            
        }
    }
}
