using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace LoadFileXaml
{
    /// <summary>
    /// WeekView.xaml 的交互逻辑
    /// </summary>
    public partial class Test1 : UserControl
    {
        public Test1()
        {
            InitializeComponent();
            //this.Loaded += Test1_Loaded;
        }

        //private void Test1_Loaded(object sender, RoutedEventArgs e)
        //{
        //    LoadFileXaml();
        //}

        public void LoadFileXaml()
       {
            try
            {
                //MyContrl
                //test
                using (var reader = new StreamReader(@"E:\shaw\demo\WPF-demo\WpfApplication\LoadFileXaml\test.xaml"))
                {
                    UIElement ctrl = XamlReader.Load(reader.BaseStream) as UIElement;
                    GrContent.Children.Add(ctrl);
                }
            }
            catch { }
       }
    }
}
