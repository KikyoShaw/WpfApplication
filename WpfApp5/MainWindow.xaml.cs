using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp5
{
    public class MoreLiveItem
    {
        /// <summary>
        /// 主播昵称
        /// </summary>
        public string AnchorNick { get; set; } = "";

        /// <summary>
        /// 显示tag标志 0-不显示，1-显示关注，2-显示粉丝徽章
        /// </summary>
        public int TagFlag { get; set; } = 0;

        /// <summary>
        /// 粉丝徽章亲密度 - 用于排序
        /// </summary>
        public int BadgeIntimacy { get; set; } = 0;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // See "https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetcolorizationcolor"
        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization, out bool pfOpaqueBlend);

        public MainWindow()
        {
            InitializeComponent();

            List<MoreLiveItem> temp = new List<MoreLiveItem>
            {
                new MoreLiveItem { AnchorNick = "主播A", TagFlag = 0, BadgeIntimacy = 0 },
                new MoreLiveItem { AnchorNick = "主播B", TagFlag = 0, BadgeIntimacy = 0 },
                new MoreLiveItem { AnchorNick = "主播C", TagFlag = 1, BadgeIntimacy = 0 },
                new MoreLiveItem { AnchorNick = "主播D", TagFlag = 0, BadgeIntimacy = 0 },
                new MoreLiveItem { AnchorNick = "主播E", TagFlag = 0, BadgeIntimacy = 0 },
            };

            // 按优先粉丝牌（TagFlag=2）、亲密度（BadgeIntimacy），其次关注（TagFlag=1）排序。
            var sortedList = temp
                .OrderByDescending(item => item.TagFlag == 2) // 粉丝牌优先
                .ThenByDescending(item => item.BadgeIntimacy) // 按亲密度排序
                .ThenByDescending(item => item.TagFlag == 1) // 关注其次
                .ThenBy(item => item.TagFlag)               // 剩余普通情况按 TagFlag
                .ToList();


            string tt = @"C:\Users\shaw\AppData\Local\Temp\hytemp\cp\90-0-ee5d791f15c29c1336b096fefa7ff34f";
            string pp = System.IO.Path.GetDirectoryName(tt);

            var b = GetWindowsTheme();

            try
            {
                DwmGetColorizationColor(out int pcrColorization, out _);
                var color = GetColor(pcrColorization);
                var brush = new SolidColorBrush(color);
                this.Xgrid.Background = brush;
            }
            catch /*(Exception e)*/
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        //public static System.Drawing.Color GetColor(int argb) => System.Drawing.Color.FromArgb(argb);
        public static System.Windows.Media.Color GetColor(int argb) => new System.Windows.Media.Color()
        {
            A = (byte)(argb >> 24),
            R = (byte)(argb >> 16),
            G = (byte)(argb >> 8),
            B = (byte)(argb)
        };

        
        // true为深色模式 反之false
        private bool GetWindowsTheme()
        {
            const string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string registryValueName = "AppsUseLightTheme";
            // 这里也可能是LocalMachine(HKEY_LOCAL_MACHINE)
            // see "https://www.addictivetips.com/windows-tips/how-to-enable-the-dark-theme-in-windows-10/"
            object registryValueObject = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKeyPath)?.GetValue(registryValueName);
            if (registryValueObject is null) return false;
            return (int)registryValueObject <= 0;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var wnd = new NotificationWindow3();
            wnd.Closed += (s, e) => { wnd = null; };
            wnd.Show();
        }
    }
}
