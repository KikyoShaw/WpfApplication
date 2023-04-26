using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp5
{
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

    }
}
