using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfTestApp.Attribute;

namespace WpfTestApp
{
    [MyClass("SHAW", 1)]
    public class MyClass
    {

    }

    [MyClass("SHAW", 1)]
    public partial class UserControl10 : UserControl
    {
        public UserControl10()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("点击了。。。");
            //Type type = typeof(MyClass);
            //object[] attArr = type.GetCustomAttributes(false);
            //foreach (var att in attArr)
            //{
            //    var myCustomAttribute = att as MyClassAttribute;
            //}
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("1111");
            ButtonBase_OnClick(Button1, new RoutedEventArgs(ButtonBase.ClickEvent));
        }
    }
}
