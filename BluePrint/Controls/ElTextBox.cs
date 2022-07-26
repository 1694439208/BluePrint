using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using CPF;

namespace Hm_Controls
{
    public class ElTextBox : TextBox
    {

        protected override void InitializeComponent()
        {
            Classes.Add("textBox");
            HScrollBarVisibility = ScrollBarVisibility.Hidden;
            VScrollBarVisibility = ScrollBarVisibility.Hidden;
            Height = 23;
            FontSize = 16;
            this.Children.Add(new Border
            {
                BorderStroke = "0",
                Margin = "0",
                Child = new ScrollViewer
                {
                    Width = "100%",
                    Height = "100%",
                    Name = "scrollViewer",
                    PresenterFor = this,
                    Content = TextBoxView,
                    Bindings = {
                        { nameof(ScrollViewer.HorizontalScrollBarVisibility), nameof(TextBox.HScrollBarVisibility), this },
                        { nameof(ScrollViewer.VerticalScrollBarVisibility), nameof(TextBox.VScrollBarVisibility), this }
                    }
                }
            });
            Children.Add(new TextBlock
            {
                FontSize = 10,
                Name = "placeholder",
                MarginLeft = 10,
                MarginTop = 5,
                IsHitTestVisible = false,
                Text = "66666666",
                Foreground = "#9195a3",
                Bindings =
                {
                    {nameof(TextBlock.Text),nameof(Placeholder),this },
                    {nameof(TextBlock.Visibility),nameof(Text),this,BindingMode.OneWay,(string pl)=>string.IsNullOrEmpty(pl)?Visibility.Visible:Visibility.Collapsed },
                }
            });
        }

        public string Placeholder
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
