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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = MainVm.Instance;
            InitializeComponent();

            string sUAFilter = "adr:9.10.0~|ios:9.10.0~|adr:9.0.0~9.3.0|web:*|adr:9.4.0:huawei,xiaomi|pc:5.49.0.0~5.51.0.0|*:*|adr:9.10.0";
            if (sUAFilter.Contains("pc:"))
            {
                var kkk = sUAFilter.Split("|");
                foreach (var info in kkk)
                {
                    if (info.Contains("pc:"))
                    {
                        var trim = info.Trim();
                        if (trim.Length > 3)
                        {
                            var jj = trim.Substring(3, trim.Length - 3);
                        }
                    }
                }

                return;

                var index = sUAFilter.LastIndexOf("pc:", StringComparison.Ordinal);
                var tt = sUAFilter.Substring(index);
                var index1 = tt.IndexOf("|", StringComparison.Ordinal);

                var tt1 = tt.Substring(3, index1 - 3);

                int dd = 55000;

                if (tt1.Contains("~"))
                {
                    if (tt1.EndsWith("~"))
                    {
                        tt1 = tt1.Substring(0, tt1.Length - 1);
                    }
                    else
                    {
                        var replac1e = tt1.Replace(".", "");
                        var vec = replac1e.Split('~');
                        if (vec.Length > 1)
                        {
                            int.TryParse(vec[0], out var dd1);
                            int.TryParse(vec[1], out var dd2);
                            if (dd >= dd1 && dd <= dd2)
                            {
                                int temp = 1;
                            }
                        }
                    }
                }

                

                var replace = tt1.Replace(".", "");

                //int.TryParse(dd, out var ff);
                //int.TryParse(replace, out var ff1);

                //if (ff <= ff1)
                //{
                //    int temp = 0;
                //}
            }
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TreasurePopup.IsOpen = true;
            }
            catch /*(Exception exception)*/
            {
                //Console.WriteLine(exception);
                //throw;
            }
        }

        private void GiftBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TreasurePopup.IsOpen = true;
            }
            catch /*(Exception exception)*/
            {
                //Console.WriteLine(exception);
                //throw;
            }
        }

        bool _altKeyDown = false;
        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (key == Key.LeftAlt || key == Key.RightAlt || key == Key.F4)
            {
                _altKeyDown = true;
            }
            if (key == Key.F4 && _altKeyDown)
            {
                e.Handled = true;
            }
        }

        private void MainWindow_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (key == Key.LeftAlt || key == Key.RightAlt)
            {
                _altKeyDown = false;
                return;
            }
        }
    }
}
