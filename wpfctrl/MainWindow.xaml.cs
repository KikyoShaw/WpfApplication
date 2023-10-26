using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace wpfctrl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = MainVm.Instance;
            InitializeComponent();

            string dest = "Http";

            if (!dest.StartsWith("http"))
            {
                int i = 0;
            }
        }

        private void ModifyUI()
        {
            // 模拟一些工作正在进行
            Thread.Sleep(TimeSpan.FromSeconds(2));
            MainVm.Instance.InsertItemInfo();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Thread thread = new Thread(ModifyUI);
                //thread.Start();
                MainVm.Instance.test1();

            }
            catch /*(Exception exception)*/
            {
                //Console.WriteLine(exception);
                //throw;
            }
        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            MainVm.Instance.test2();
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            MainVm.Instance.test3();
        }
    }
}
