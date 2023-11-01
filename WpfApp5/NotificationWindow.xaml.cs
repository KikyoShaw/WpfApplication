using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;


        private double screenHeight = 0.0;
        private DispatcherTimer timer = null;


        public NotificationWindow()
        {
            InitializeComponent();

            //// 获取主显示器的工作区
            //var screen = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            ////窗口初始化在右下角
            //this.Left = screen.Right;
            //this.Top = screen.Bottom - this.Height;

            // 获取屏幕尺寸
            screenHeight = SystemParameters.WorkArea.Height;
            var screenWidth = SystemParameters.WorkArea.Width;
            // 确保窗口开始在屏幕的右下角
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight;

            InitDispatcherTimer();
            this.Loaded += NotificationWindow_Loaded;
        }

        private void InitDispatcherTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromSeconds(10)
            };
            timer.Tick += (s, e) =>
            {
                HideAnimation();
            };
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //double from = workingArea.Right;
            //double to = workingArea.Right - this.Width;
            //DoubleAnimation doubleAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(500)));
            //Storyboard.SetTarget(doubleAnimation, this);
            //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.LeftProperty));
            //Storyboard sb = new Storyboard();
            //sb.Children.Add(doubleAnimation);
            //sb.Begin();

            // 设置目标顶部位置
            var targetTop = screenHeight - this.Height - 4;
            var openAnimation = new DoubleAnimation(targetTop, new Duration(TimeSpan.FromSeconds(1)));
            openAnimation.Completed += (s, _) =>
            {
                this.BeginAnimation(Window.TopProperty, null);
                timer?.Start();
            };
            this.BeginAnimation(Window.TopProperty, openAnimation);
        }

        private void HideAnimation()
        {
            var closeAnimation = new DoubleAnimation(screenHeight, new Duration(TimeSpan.FromSeconds(1)));
            closeAnimation.Completed += (ss, ee) => Close();
            BeginAnimation(Window.TopProperty, closeAnimation);
            if (timer is { IsEnabled: true }) timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HideAnimation();
        }
    }
}
