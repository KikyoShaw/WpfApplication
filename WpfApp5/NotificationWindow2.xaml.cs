using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// NotificationWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow2 : Window
    {
        private readonly double screenWidth = 0.0;
        private DispatcherTimer timer = null;

        public NotificationWindow2()
        {
            InitializeComponent();

            // 获取屏幕尺寸
            var screenHeight = SystemParameters.WorkArea.Height;
            screenWidth = SystemParameters.WorkArea.Width;

            // 确保窗口开始在屏幕的右下角
            this.Left = screenWidth;
            this.Top = screenHeight - this.Height;

            InitDispatcherTimer();
            this.Loaded += NotificationWindow2_Loaded;
        }

        private void NotificationWindow2_Loaded(object sender, RoutedEventArgs e)
        {
            // 创建向左滑动的动画
            var targetTop = screenWidth - this.Width - 4;
            var openAnimation = new DoubleAnimation(targetTop, new Duration(TimeSpan.FromSeconds(1)));
            openAnimation.Completed += (s, _) =>
            {
                this.BeginAnimation(Window.LeftProperty, null);
                timer?.Start();
            };
            this.BeginAnimation(Window.LeftProperty, openAnimation);
        }

        private void InitDispatcherTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += (s, e) =>
            {
                HideAnimation();
            };
        }

        private void HideAnimation()
        {
            // 向左滑出屏幕并关闭窗口
            var closeAnimation = new DoubleAnimation(screenWidth, new Duration(TimeSpan.FromSeconds(1)));
            closeAnimation.Completed += (ss, ee) => Close();
            BeginAnimation(Window.LeftProperty, closeAnimation);
            if (timer is { IsEnabled: true }) timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HideAnimation();
        }
    }
}
