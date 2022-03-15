using ShaderEffectTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;
namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int iMediaIndex = 0;
        public MainWindow()
        {

            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.CanResize;

            WindowChrome.SetWindowChrome(this,
                new WindowChrome { GlassFrameThickness = WindowChrome.GlassFrameCompleteThickness, CaptionHeight = 0 });

            InitializeComponent();
            Loaded += MainWindow_Loaded;

            timer.Interval = TimeSpan.FromMilliseconds(70);
            timer.Tick += new EventHandler(timer_tick);

        }
        public static OperatingSystem GetEdition()
        {
            return System.Environment.OSVersion;
        }
        public static bool IsLowVersionWindows()
        {
            var os = System.Environment.OSVersion;

            if (os.Version.Major > 6)
                return false;

            if (os.Version.Major < 6)
                return true;

            if (os.Version.Minor < 2)
                return true;

            return false;
        }
        public class MonitorHelper
        {
            private static Lazy<MonitorHelper> lazyInstance = new Lazy<MonitorHelper>();
            public static MonitorHelper Instance => lazyInstance.Value;

            public delegate bool MONITORENUMPROC(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData);

            [StructLayout(LayoutKind.Sequential, Size = 8)]
            public struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [DllImport("User32.dll")]
            private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr pprcClip, MONITORENUMPROC monitorEnumProc, IntPtr dwData);

            private MONITORENUMPROC monitorEnumProc;

            public List<Rect> _moitorInfos = new List<Rect>();

            public int GetMoitorCount()
            {
                return _moitorInfos.Count;
            }
            public Rect GetMoitorRect(int index)
            {
                if (index < _moitorInfos.Count)
                {
                    return _moitorInfos.ElementAt(index);
                }
                return new Rect(0, 0, 0, 0);
            }
            public MonitorHelper()
            {
                EnumMonitors();
            }
            private void EnumMonitors()
            {
                monitorEnumProc = MonitorEnumProc;
                EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, monitorEnumProc, IntPtr.Zero);
            }
            private bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(lprcMonitor, typeof(RECT));
                Rect moitor = new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                _moitorInfos.Add(moitor);
                return true;
            }
        }
        public Uri GetIndexMedia()
        {
            iMediaIndex++;
            iMediaIndex %= 1;
            return new Uri(System.AppDomain.CurrentDomain.BaseDirectory + $"{iMediaIndex}.mp4");
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                var hwndTarget = hwndSource.CompositionTarget;
                if (hwndTarget != null)
                    hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            }

            var item = IsLowVersionWindows();
            ggggg.Source = GetIndexMedia();//new Uri(@"F:\1.mp4");
            //Utilities.GetResourcePackUri(@"F:\1.mp4");
            //ggggg.Source = GetIndexMedia();
            ggggg.Play();

            
        }

        private void MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = ggggg.NaturalDuration.TimeSpan.TotalSeconds;
            sliderPosition.Value = 0;

            //ggggg.Source = new Uri(@"https://jakearchibald.com/scratch/alphavid/compressed.mp4");
            //ggggg.Play();

            //ggggg.Source = Utilities.GetResourcePackUri(@"F:\1.mp4");
            timer.Start();

            ggggg.Play();
        }

        bool bFirst = true;
        int iTick = 0;
        private void timer_tick(object sender, EventArgs e)
        {
            iTick++;
            sliderPosition.Value = ggggg.Position.TotalSeconds;
            /*if(bFirst && sliderPosition.Value*1000 > 300)
            {
                bFirst = false;
                ggggg.Stop();
                ggggg.Play();
            }*/
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("11111");
            //ggggg.Stop();
            ggggg.Stop();
            ggggg.Close();
            ggggg.Source = null;

            ggggg.Source = GetIndexMedia();
            ggggg.Play();
        }

        static int i = 0;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(i==0)
            {
                ggggg.Play();
                i = 1;
            }
            else
            {
                ggggg.Stop();
                i = 0;
            }
        }

        private void ggggg_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("播放失败, 请检查解码器"); 
        }

        private void ggggg_BufferingStarted(object sender, RoutedEventArgs e)
        {
            ggggg.Pause();
        }

        private void ggggg_BufferingEnded(object sender, RoutedEventArgs e)
        {
            ggggg.Play();
        }
    }
}
