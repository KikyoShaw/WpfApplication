using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace WpfApp5
{
    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

        public NotificationWindow()
        {
            InitializeComponent();

            // 设置窗口出现为屏幕的右下角
            //this.Left = SystemParameters.WorkArea.Width;
            //this.Top = SystemParameters.WorkArea.Height - this.Height;

            // 获取主显示器的工作区
            var screen = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            //窗口初始化在右下角
            this.Left = screen.Right;
            this.Top = screen.Bottom - this.Height;

            this.Loaded += NotificationWindow_Loaded;
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //double from = SystemParameters.WorkArea.Width;
            //double to = SystemParameters.WorkArea.Width - this.Width;

            double from = workingArea.Right;
            double to = workingArea.Right - this.Width;

            DoubleAnimation doubleAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(500)));

            Storyboard.SetTarget(doubleAnimation, this);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.LeftProperty));

            Storyboard sb = new Storyboard();
            sb.Children.Add(doubleAnimation);
            sb.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 点击按钮关闭窗口
            this.Close();
        }
    }
}
