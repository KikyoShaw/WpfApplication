using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoadFileXaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = TestVm.instance;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           this.Test1.LoadFileXaml();
        }

        private bool bState = false;
        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            TestVm.instance.name = bState ?  "虎牙直播" : "斗鱼直播";
            TestVm.instance.url = bState ? @"https://www.huya.com/" : @"https://www.douyu.com/";
            bState = !bState;
        }
    }
}
