using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.ComponentModel;

namespace WpfAppTheme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref int attrValue, int attrSize);



        public MainWindow()
        {
            InitializeComponent();

            //IntPtr wptr = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            //var result = EnableDarkModeForWindow(wptr, true);

            // GetThemeColor();
            //SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
            // XGrid.Background = new SolidColorBrush(SystemParameters.WindowGlassColor);
        }

        private void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SystemParameters.WindowGlassColor))
            {
                // 执行响应主题颜色变化的代码
                ThemeColorChanged();
            }
        }

        private void GetThemeColor()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (key != null)
            {
                int theme = (int)key.GetValue("AppsUseLightTheme", -1);
                if (theme == 0)
                {
                    XGrid.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }
                else if (theme == 1)
                {
                    XGrid.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                else
                {
                    XGrid.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                key.Close();
            }
        }

        //private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        //{
        //    if (e.Category == UserPreferenceCategory.Color)
        //    {
        //        // 执行响应主题颜色变化的代码
        //        ThemeColorChanged();
        //    }
        //}


        private void ThemeColorChanged()
        {
            // 这里可以执行需要响应的代码，例如更改背景颜色或文本颜色等。
            // 示例：更改窗口背景颜色
            XGrid.Background = new SolidColorBrush(SystemParameters.WindowGlassColor);
        }

        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);
        //    SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
        //}

        public static bool EnableDarkModeForWindow(IntPtr hWnd, bool enable)
        {
            int darkMode = enable ? 1 : 0;
            int hr = DwmSetWindowAttribute(hWnd, DwmWindowAttribute.UseImmersiveDarkMode, ref darkMode, sizeof(int));
            return hr >= 0;
        }
    }
}
