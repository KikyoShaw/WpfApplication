using System;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
//using ToastNotifications;
//using ToastNotifications.Lifetime;
//using ToastNotifications.Position;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using UserControl = System.Windows.Forms.UserControl;

namespace WpfWin11Notify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Notifier notifier;

        public MainWindow()
        {
            InitializeComponent();

            //Init();

            Init();
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

        private void Notify()
        {
            var content = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Andrew sent you a picture")
                .AddText("Check this out, The Enchantments in Washington!")
                .AddInlineImage(new Uri("https://unsplash.com/photos/1Rm9GLHV8GQ"))
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like"))
                .AddButton(new ToastButton()
                    .SetContent("View")
                    .AddArgument("action", "viewImage"))
                .GetToastContent();

            var doc = new XmlDocument();
            doc.LoadXml(content.GetContent());
            var toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
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
            Notify2();

            //notifier.ShowSuccess("Hello World！");
        }
    }
}
