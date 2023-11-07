using System.Runtime.InteropServices;
using System.Windows;

namespace WpfWin11Notify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("shell32.dll")]
        static extern int SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppUserModelID);

        protected override void OnStartup(StartupEventArgs e)
        {
            SetCurrentProcessExplicitAppUserModelID("HuYa.HuyaClient");
            // 剩余的初始化代码...
            base.OnStartup(e);
        }
    }


}
