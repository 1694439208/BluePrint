using System;
using System.Collections.Generic;
using System.Text;
using CPF;
using CPF.Drawing;
using CPF.Controls;
using CPF.Animation;
using System.Linq;

namespace 蓝图重制版.BluePrint.IJoin
{
    public static class UIElementTool
    {
        public static T Get<T>(this List<object> element, int name)
        {
            return (T)(element[name]);
        }
        /// <summary>
        /// 获取id变量名
        /// </summary>
        /// <param name="element"></param>
        /// <param name="IsD">是否加入前缀表示使用变量</param>
        /// <returns></returns>
        public static string GetID(this int element, bool IsD = true)
        {
            return $"{(IsD ? "$" : "")}temp_{element}";
        }
        public static string join(this List<string> element, string str)
        {
            return string.Join(str, element.Where(a => a != "").ToList());
        }
        /// <summary>
        /// 递归获取相对于父元素的坐标
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Point GetPosition(this UIElement element,Point point, UIElement parent)
        {
            return GetParentPosition(element, point, parent);
        }
        public static Point GetParentPosition(UIElement element,Point point, UIElement parent) {
            if (element.Parent == parent)//element.Parent == null||
            {
                return element.TransformPoint(point);
            }

            return GetParentPosition(element.Parent,element.TransformPoint(point), parent);
        }
        static List<ToastControl> controls = new List<ToastControl>();
        public static void Toast(BluePrint control,string title, Point point,float time = 0.3f) {
            if (controls.Count > 0)
            {
                foreach (var item in controls)
                {
                    item.Start(title, point, time);
                    return;
                }
            }
            else {
                var toast = new ToastControl(title, point, time);
                toast.ZIndex = 100;
                control.AddChildren1(toast);
                controls.Add(toast);
            }
            
        }
    }
}
