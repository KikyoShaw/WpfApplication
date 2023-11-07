using System;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Windows.Foundation.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace WpfWin11Notify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Notifier notifier;

        private string _currentPath;

        public MainWindow()
        {
            InitializeComponent();

            // 获取项目bin DEBUG或RELEASE路径
            string binPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            // 回退两次获得项目的根目录
            string projectRootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(binPath).FullName).FullName).FullName;
            // 拼接得到 Resources 目录
            _currentPath = Path.Combine(projectRootDirectory, "Resources");
            //Init();
            Init();

            // 监听通知激活(点击)
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // 通知参数
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
                // 获取任何用户输入
                ValueSet userInput = toastArgs.UserInput;

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show("Toast被激活（点击），参数是: " + toastArgs.Argument);
                });
            };
        }

        private void Init()
        {
            //// 打开快捷方式
            //string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            //                      "\\Microsoft\\Windows\\Start Menu\\Programs\\WpfWin11Notify.exe.lnk";

            //dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            //var shortcut = shell.CreateShortcut(shortcutPath);

            //shortcut.TargetPath = @"E:\\shaw\\demo\\WPF-demo\\WpfApplication\\WpfWin11Notify\\bin\\Debug\\netcoreapp3.1";
            //shortcut.Description = "My App";
            //shortcut.AppUserModelID = "MyAppAUMID";
            //shortcut.Save();
        }

        //private void Init()
        //{
        //    notifier = new Notifier(cfg =>
        //    {
        //        cfg.PositionProvider = new WindowPositionProvider(
        //            parentWindow: System.Windows.Application.Current.MainWindow,
        //            corner: Corner.BottomRight,
        //            offsetX: 10,
        //            offsetY: 10);

        //        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
        //            notificationLifetime: TimeSpan.FromSeconds(3),
        //            maximumNotificationCount: MaximumNotificationCount.FromCount(5));

        //        cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
        //    });
        //}

        private void NotifyOne()
        {
            var path = Path.Combine(_currentPath, "1.jpg");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like"))
                .AddButton(new ToastButton()
                    .SetContent("View")
                    .AddArgument("action", "viewImage"))

                .Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddSeconds(10); //设置通知的过期时间
                    toast.Tag = "huya";
                    toast.Group = "hy";
                });
        }

        private void Notify()
        {
            var path = Path.Combine(_currentPath, "1.jpg");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like"))
                .AddButton(new ToastButton()
                    .SetContent("View")
                    .AddArgument("action", "viewImage"))
                .Show();
        }

        private void NotifyTest()
        {
            var path= Path.Combine(_currentPath, "1.jpg");
            //var urlPath = "https://huyaimg.msstatic.com/avatar/1031/64/ef633c67bf261723683647ebd14356_180_135.jpg?1650617437";

            var content = new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like"))
                .AddButton(new ToastButton()
                    .SetContent("View")
                    .AddArgument("action", "viewImage"))
                .GetToastContent();

            var toast = new ToastNotification(content.GetXml());
            toast.Dismissed += ToastNotification_Dismissed;
            ToastNotificationManager.CreateToastNotifier("HuYa.HuyaClient").Show(toast);
        }

        private void ToastNotification_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            System.Windows.MessageBox.Show("Toast被关闭了!" );
        }

        private void Change()
        {
            var path = Path.Combine(_currentPath, "th-c6.png");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("坏空空开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .Show(toast =>
                {
                    toast.Tag = "huya";
                    toast.Group = "hy";
                });
        }

        private void Notify1()
        {
            Hardcodet.Wpf.TaskbarNotification.TaskbarIcon tbi = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();
            tbi.Icon = new System.Drawing.Icon("shortcut_icon.ico");
            tbi.ToolTipText = "ToolTipText";

            tbi.ShowBalloonTip("Title", "BalloonText", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
        }

        private NotifyContent _userControl = null;

        private void Notify2()
        {
            NotifyIcon fyIcon = new NotifyIcon();
            fyIcon.Icon = new Icon("shortcut_icon.ico");/*找一个ico图标将其拷贝到 debug 目录下*/
            fyIcon.BalloonTipText = "Hello World！";/*必填提示内容*/
            fyIcon.BalloonTipTitle = "通知";
            fyIcon.BalloonTipIcon = ToolTipIcon.Info;
            fyIcon.Visible = true;/*必须设置显隐，因为默认值是 false 不显示通知*/
            fyIcon.ShowBalloonTip(0);

            fyIcon.BalloonTipClosed += FyIcon_BalloonTipClosed;
            fyIcon.BalloonTipClicked += FyIcon_BalloonTipClicked;
            fyIcon.BalloonTipShown += FyIcon_BalloonTipShown;
            fyIcon.MouseClick += FyIcon_Click;
        }

        private void Notify3()
        {
            _userControl ??= new NotifyContent();
            Hardcodet.Wpf.TaskbarNotification.TaskbarIcon tbi = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();
            tbi.Icon = new System.Drawing.Icon("shortcut_icon.ico");
            tbi.ToolTipText = "虎牙直播";
            //System.Windows.Controls.Label label = new System.Windows.Controls.Label()
            //{
            //    Content = "Your content here",
            //    Background = System.Windows.SystemColors.WindowBrush
            //};
            //tbi.ToolTip = label;
            tbi.ShowCustomBalloon(_userControl, System.Windows.Controls.Primitives.PopupAnimation.Slide, 4000);
        }

        private void FyIcon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("fyIcon is click....");
        }

        private void FyIcon_BalloonTipShown(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("fyIcon is show.");
        }

        private void FyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("fyIcon is clicked.");
        }

        private void FyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("fyIcon is close.");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // Notify();
            Notify();

            //notifier.ShowSuccess("Hello World！");
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            Change();
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            ToastNotificationManagerCompat.History.Remove("huya", "hy");
        }

        private void ButtonBase4_OnClick(object sender, RoutedEventArgs e)
        {
            NotifyOne();
        }

        private void ButtonBase5_OnClick(object sender, RoutedEventArgs e)
        {
            ToastNotificationManagerCompat.History.Clear();
        }

        private void ButtonBase6_OnClick(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(_currentPath, "th-c6.png");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .Show();
        }

        private void ButtonBase7_OnClick(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(_currentPath, "th-c6.png");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like"))
                .AddButton(new ToastButton()
                    .SetContent("View")
                    .AddArgument("action", "viewImage"))
                .Show();
        }

        private void ButtonBase8_OnClick(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(_currentPath, "th-c6.png");
            var path2 = Path.Combine(_currentPath, "1.jpg");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("周星星开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .AddInlineImage(new Uri(path2))
                .Show();
        }

        private void ButtonBase9_OnClick(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(_currentPath, "th-c6.png");

            new ToastContentBuilder()
                .AddText("虎牙发来一条消息") // 标题文本
                .AddText("坏空空开播啦，赶紧过来围观!")
                .AddAppLogoOverride(new Uri(path), ToastGenericAppLogoCrop.Circle)
                .Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddSeconds(5); //设置通知的过期时间
                    toast.Tag = "huya";
                    toast.Group = "hy";
                });
        }

        private void ButtonBase10_OnClick(object sender, RoutedEventArgs e)
        {
            NotifyTest();
        }

        private void ButtonBase11_OnClick(object sender, RoutedEventArgs e)
        {
            var urlPath = "https://huyaimg.msstatic.com/avatar/1031/64/ef633c67bf261723683647ebd14356_180_135.jpg?1650617437";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(urlPath, UriKind.Absolute);
            bitmap.EndInit();

            TestImage.Source = bitmap;
        }
    }
}
