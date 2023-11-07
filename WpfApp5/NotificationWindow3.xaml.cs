using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// NotificationWindow3.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow3 : Window
    {
        private DispatcherTimer timer = null;
        private readonly double contentWidth;
        private readonly double hidePosition;

        public NotificationWindow3()
        {
            InitializeComponent();

            // 设置窗口在屏幕右下角。
            contentWidth = ContentGrid.Width;
            hidePosition = contentWidth + 10;
            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.Top = SystemParameters.WorkArea.Height - this.Height;
            ContentGrid.Margin = new Thickness(hidePosition, 0, 0, 0);
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var showAnimation = new ThicknessAnimation(new Thickness(hidePosition, 0, 0, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromMilliseconds(200));
            showAnimation.Completed += ShowAnimation_Completed;
            ContentGrid.BeginAnimation(Grid.MarginProperty, showAnimation);
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                timer.Tick += (s, e) => { HideContent(); };
            }

            timer.Start();
        }

        private void HideContent()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation(contentWidth, 0, TimeSpan.FromMilliseconds(200));
            ThicknessAnimation marginAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(contentWidth, 0, 0, 0), TimeSpan.FromMilliseconds(200));
            marginAnimation.Completed += (s, _) => Close();
            ContentGrid.BeginAnimation(Grid.WidthProperty, widthAnimation);
            ContentGrid.BeginAnimation(Grid.MarginProperty, marginAnimation);
            //var hideAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(hidePosition, 0, 0, 0), TimeSpan.FromMilliseconds(200));
            //hideAnimation.Completed += (s, _) => Close();
            //ContentGrid.BeginAnimation(Grid.MarginProperty, hideAnimation);
            if (timer is { IsEnabled: true }) timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HideContent();
        }
    }
}
