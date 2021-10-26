using CPF;
using CPF.Controls;
using Hm_Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using 蓝图重制版.BluePrint.Controls;
using 蓝图重制版.BluePrint.INode;

namespace 蓝图重制版.BluePrint.Join
{
    public class SearchMenuItem : Control
    {
        List<Type> NodeTypes = new List<Type>();
        Dictionary<string, List<(IJoin.NodeBaseInfoAttribute,Type)>> valuePairs = new Dictionary<string, List<(IJoin.NodeBaseInfoAttribute, Type)>>();
        public SearchMenuItem()
        {
        }
        public SearchMenuItem(List<Type> nodetypes, BParent bParent) {
            NodeTypes = nodetypes;
            parent = bParent;
        }
        private BParent parent;
        private Popup _popup;

        public Collection<TreeViewItem> Nodes
        {
            get { return GetValue<Collection<TreeViewItem>>(); }
            set { SetValue(value); }
        }
        public void SetItems() {
            if (NodeTypes != null)
            {
                foreach (var item in NodeTypes)
                {
                    var args = item.GetCustomAttributes(typeof(IJoin.NodeBaseInfoAttribute),false);
                    if (args!=null&&args.Length>0)
                    {
                        var NodeBaseInfo = (args[0] as IJoin.NodeBaseInfoAttribute);
                        if (valuePairs.ContainsKey(NodeBaseInfo.NodeGroup))
                        {
                            valuePairs[NodeBaseInfo.NodeGroup].Add((NodeBaseInfo, item));
                        }
                        else {
                            valuePairs.Add(NodeBaseInfo.NodeGroup,new List<(IJoin.NodeBaseInfoAttribute, Type)>() {
                                (NodeBaseInfo, item),
                            });
                        }
                    }
                }
                Nodes = new Collection<TreeViewItem>();
                foreach (var item in valuePairs)
                {
                    var treev1 = new SearchTreeViewItem
                    {
                        Header = item.Key,
                    };
                    foreach (var item1 in item.Value)
                    {
                        treev1.Items.Add(new SearchTreeViewItem { 
                            Header = item1.Item1.NodeName,
                            Tag = item1.Item2,
                        });
                    }
                    Nodes.Add(treev1) ;
                }
                /*this.Delay(TimeSpan.FromSeconds(1),()=> {
                    Debug.WriteLine(tree.GetChildren().Count);
                    Debug.WriteLine(tree.Items.Count);
                });*/

            }
        }
        /// <summary>
        /// 展开所有节点
        /// </summary>
        public void Open() {
            var tree = FindPresenterByName<TreeView>("TreeView1");
            foreach (var item in tree.AllItems())
            {
                item.IsExpanded = true;
            }
        }
        /// <summary>
        /// 关闭所有节点
        /// </summary>
        public void close()
        {
            var tree = FindPresenterByName<TreeView>("TreeView1");
            foreach (var item in tree.AllItems())
            {
                item.IsExpanded = false;
            }
        }

        protected override void InitializeComponent()
        {
            _popup = Parent as Popup;
            MarginTop = 10;
            Width = 280;
            Height = 300;
            Background = "38,38,38";
            BorderType = BorderType.BorderThickness;
            BorderThickness = "1,1,1,1";
            BorderFill = "0,0,0";
            CornerRadius = "3,3,3,3";
            Children.Add(new StackPanel
            {
                MarginTop = 10,
                //MarginBottom = 10,
                Width = "100%",

                Height = "100",
                Children =
                {
                    new DockPanel
                    {
                        Width = "100%",
                        Children =
                        {
                            new CheckBox
                            {
                                Attacheds =
                                {
                                    {DockPanel.Dock,Dock.Right}
                                },
                                MarginRight = 5,
                                Content = "情景关联",
                                Foreground = "221,221,221,200",
                                FontSize = 15,
                            },
                            new TextBlock
                            {
                                Attacheds =
                                {
                                    {DockPanel.Dock,Dock.Left}
                                },
                                MarginLeft = 5,
                                Text = "此蓝图的所有操作",
                                Foreground = "221,221,221,200",
                                FontSize = 15,
                            }
                        },
                    },
                    new Panel
                    {
                        Children =
                        {
                            new ElTextBox
                            {
                                Name = "SearchElTextBox",
                                PresenterFor = this,
                                MarginRight = 20,
                                MarginLeft = 2f,
                                MarginTop = 0f,
                                MarginBottom = 0f,
                                Placeholder = "搜索",
                                Classes = "single",
                            },
                            new TextBlock
                            {
                                Name = "closetext",
                                Classes = "el-icon,el-icon-close",
                                MarginRight =0f,
                                Width = 20,
                                Foreground = "0,0,0",
                                Commands = {
                                    {nameof(TextBlock.MouseUp),(s,e)=>{
                                        var Searchtextbox = FindPresenterByName<ElTextBox>("SearchElTextBox");
                                        Searchtextbox.Text = "";
                                    }}
                                },
                                Triggers = {
                                    {nameof(IsMouseOver), Relation.Me, null, (nameof(Foreground), "0,0,0,100") }
                                },
                            },
                        },
                        Classes = "el-textbox",
                        Width = "100%",
                        Height = 27.1f,
                        MarginTop = 10,
                    },
                    new Panel
                    {
                        Children =
                        {
                            new TestTreeView
                            {
                                
                                MarginTop = 3,
                                Name = "TreeView1",
                                Width = "100%",
                                Height = "218",
                                //Size= SizeField.Fill,
                                //DisplayMemberPath=nameof(NodeData.Text),
                                //ItemsMemberPath=nameof(NodeData.Nodes),
                                PresenterFor = this,
                                Bindings = {
                                    {"Items","Nodes", this}
                                },
                                Commands = {
                                    {nameof(TreeView.ItemMouseUp),(s,e)=>{
                                        var itemview = e as TreeViewItemMouseEventArgs;
                                        if (itemview.Item is SearchTreeViewItem && itemview.Item.Tag!=null)
                                        {
                                            
                                            parent.CreateNode((Type)itemview.Item.Tag,parent.MousepanelPupopPos.MarginLeft.Value,parent.MousepanelPupopPos.MarginTop.Value);
                                             //var Control = System.Activator.CreateInstance((Type)itemview.Item.Tag,new object[]{ parent});
                                             //parent.bluePrint.AddChildren((CPF.Controls.Control)Control);
                                            _popup.Visibility = Visibility.Hidden;
                                        }
                                    }}
                                },
                            },
                        },
                        Size=SizeField.Fill,
                    }
                },
            }) ;
            //this.Triggers.Add(nameof(IsMouseOver), Relation.Me, null, (nameof(Background), "#fff"));
            SetItems();
        }
    }
}
