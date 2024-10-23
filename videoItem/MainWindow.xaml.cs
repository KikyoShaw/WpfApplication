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

namespace videoItem
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

            this.DpiChanged += MainWindow_DpiChanged;
		}

        private void MainWindow_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            if (e.NewDpi.DpiScaleX == e.OldDpi.DpiScaleX && e.NewDpi.DpiScaleY == e.OldDpi.DpiScaleY)
                return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("clicked");
		}

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(Xb.Visibility == Visibility.Collapsed)
                Xb.Visibility = Visibility.Visible;
            else if(Xb.Visibility == Visibility.Visible)
                Xb.Visibility = Visibility.Collapsed;
        }
    }
}
