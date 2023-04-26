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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool result = false;

        private void Clear()
        {
            Storyboard?.Stop();
            Storyboard?.Children.Clear();
        }

        private bool aaa = true;
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            // XGrid.Visibility = !result ? Visibility.Collapsed : Visibility.Visible;
            Clear();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Clear();

            if (result)
            {
                var doubleAnimation = new DoubleAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    From = 0,
                    To = 180,
                };

                Storyboard.SetTargetName(doubleAnimation, XGrid.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Width"));

                Storyboard.Children.Add(doubleAnimation);
                Storyboard.Begin(this);
            }
            else
            {
                var doubleAnimation = new DoubleAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    From = 180,
                    To = 0,
                };

                Storyboard.SetTargetName(doubleAnimation, XGrid.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Width"));

                Storyboard.Children.Add(doubleAnimation);
                Storyboard.Begin(this);
            }

            result = !result;
            //if (!result)
            //{
            //    //var story = (Storyboard)this.Resources["HideWindow"];
            //    //if (story != null)
            //    //{
            //    //    story.Completed += delegate { XGrid.Visibility = Visibility.Collapsed; };
            //    //    story.Begin(this.XGrid);
            //    //}
            //    XGrid.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    //var story = (Storyboard)this.Resources["ShowWindow"];
            //    //if (story != null)
            //    //{
            //    //    story.Completed += delegate { XGrid.Visibility = Visibility.Visible; };
            //    //    story.Begin(this.XGrid);
            //    //}
            //    XGrid.Visibility = Visibility.Visible;
            //}


        }
    }
}
