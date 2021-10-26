using CPF;
using CPF.Controls;
using CPF.Drawing;
using CPF.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint.Join
{
    public class SearchTreeViewItem : TreeViewItem
    {
        protected override void InitializeComponent()
        {
            Foreground = "255,255,255";
            if (!string.IsNullOrWhiteSpace(DisplayMemberPath))
            {
                _ = this[nameof(Header)] <= DisplayMemberPath;
            }
            if (!string.IsNullOrWhiteSpace(ItemsMemberPath))
            {
                _ = this[nameof(Items)] <= ItemsMemberPath;
            }
            if (IsRootInDesignMode)
            {
                Width = 100;
                Height = 40;
            }
            else
            {
                Width = "100%";
            }
            var panel = ItemsPanel.CreateElement();
            panel.Name = "itemsPanel";
            panel.PresenterFor = this;
            panel.MarginLeft = 20;//子节点偏移
            panel.MarginRight = 0;
            panel[nameof(Visibility)] = (this, nameof(IsExpanded), a => (bool)a ? Visibility.Visible : Visibility.Collapsed);
            Children.Add(new StackPanel
            {
                MarginLeft = 0,
                Orientation = Orientation.Vertical,
                Width = "100%",
                Height = "100%",
                Children =
                {
                    new Panel
                    {
                        Name="treeViewItem",
                        PresenterFor=this,
                        Width="100%",
                        Height=40,
                        BorderType = BorderType.BorderThickness,
                        BorderThickness = new Thickness(0, 0, 0, 1),
                        BorderFill = "255,255,255,100",
                        Children =
                        {
                            new Panel
                            {
                                MarginLeft=5,
                                Width=12,
                                Children =
                                {
                                    new Polygon
                                    {
                                        StrokeFill = "255,255,255,160",
                                        IsAntiAlias=true,
                                        RenderTransformOrigin=new PointField("30%","70%"),
                                        Points=
                                        {
                                            new Point(2,2),
                                            new Point(2,10),
                                            new Point(6,6),
                                        },
                                        Bindings=
                                        {
                                            {
                                                nameof(Polygon.RenderTransform),
                                                nameof(IsExpanded),
                                                this,
                                                BindingMode.OneWay,
                                                a=>(bool)a?new RotateTransform(45):Transform.Identity
                                            },
                                        }
                                    }
                                },
                                Bindings =
                                {
                                    {
                                        nameof(Visibility),
                                        nameof(HasItems),
                                        this,
                                        BindingMode.OneWay,
                                        a=>(bool)a?Visibility.Visible:Visibility.Collapsed
                                    }
                                }
                            },
                            new ContentControl
                            {
                                MarginLeft = 23,
                                Bindings=
                                {
                                    {
                                        nameof(ContentControl.Content),
                                        nameof(TreeViewItem.Header),
                                        this
                                    },
                                    {
                                        nameof(ContentControl.ContentTemplate),
                                        nameof(TreeViewItem.HeaderTemplate),
                                        this
                                    }
                                }
                            },
                        },
                        Commands=
                        {
                            {
                                nameof(MouseDown),
                                (s,e)=>{
                                    IsExpanded=!IsExpanded;
                                    if (!HasItems)
                                    {
                                        SingleSelect();
                                    }
                                }
                            }
                        },
                        Triggers=
                        {
                            {
                                nameof(IsMouseOver),
                                Relation.Me,
                                null,
                                (nameof(Background),"47,47,47")
                            },
                        },
                    },
                    panel,
                },
            });
            this.Triggers.Add(nameof(IsSelected), Relation.Me.Find(a => a.Name == "treeViewItem" && a.PresenterFor == this), null, (nameof(Background), "48,48,48"));
        }
    }
}
