using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WpfTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _bMouseHover = false;
        private FloatingWindow _popup = null;
        private bool _bMouseHoverPopup = false;
        private DispatcherTimer _checkShowTimer = null;
        private DispatcherTimer _checkHideTimer = null;

        public MainWindow()
        {
            InitializeComponent();
            InitTimer();
            InitPopup();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_popup != null)
            {
                _popup.Close();
                _popup = null;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string tempPath = System.IO.Path.GetTempPath();
            var path = System.IO.Path.GetPathRoot(tempPath);
            if (string.IsNullOrWhiteSpace(path))
                return;
            DriveInfo driveInfo = new DriveInfo(path);
            var availableFreeSpace = driveInfo.AvailableFreeSpace / Math.Pow(1024, 3); ;
            System.Diagnostics.Trace.WriteLine($"availableFreeSpace:{availableFreeSpace}");
        }

        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_checkShowTimer.IsEnabled)
                _checkShowTimer.Start();
            _bMouseHover = true;
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_checkHideTimer.IsEnabled)
                _checkHideTimer.Start();
            _bMouseHover = false;
        }

        private void InitTimer()
        {
            try
            {
                _checkShowTimer = new DispatcherTimer(DispatcherPriority.Background);
                _checkShowTimer.Interval = TimeSpan.FromSeconds(0.5);
                _checkShowTimer.Tick += (s, e) =>
                {
                    if (_bMouseHover)
                    {
                        var pt = Mouse.GetPosition(TestButton);
                        if (pt.X > 0 && pt.Y > 0 && pt.X < TestButton.ActualWidth && pt.Y < TestButton.ActualHeight)
                        {
                            var ptBtn = TestButton.PointToScreen(new System.Windows.Point(0, 0));
                            double dpi = 1;
                            _popup.Left = ptBtn.X / dpi + TestButton.ActualWidth / 2 - 144/*192*/;
                            _popup.Top = (ptBtn.Y / dpi) + TestButton.ActualHeight + 8;
                            _popup.Show();
                        }
                    }
                    if (_checkShowTimer.IsEnabled)
                        _checkShowTimer.Stop();
                };

                _checkHideTimer = new DispatcherTimer(DispatcherPriority.Background);
                _checkHideTimer.Interval = TimeSpan.FromSeconds(0.2);
                _checkHideTimer.Tick += (s, e) =>
                {
                    if (!_bMouseHoverPopup && !_bMouseHover)
                        _popup?.Hide();
                    if (_checkHideTimer.IsEnabled)
                        _checkHideTimer.Stop();
                };
            }
            catch (Exception ex)
            {
               
            }
        }

        private void InitPopup()
        {
            if (_popup == null)
            {
                _popup = new FloatingWindow();
                _popup.MouseLeave += (s, args) =>
                {
                    _bMouseHoverPopup = false;
                    _popup?.Hide();
                };
                _popup.MouseEnter += (s, args) => { _bMouseHoverPopup = true; };
            }
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            // 创建一个 Stopwatch 实例
            Stopwatch stopwatch = new Stopwatch();
            // 开始计时
            stopwatch.Start();
            var path = @"D:\HuyaClient.exe";
            var tt = CalculateMD5(path);
            stopwatch.Stop();
            // 获取经过的时间
            var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            System.Diagnostics.Trace.WriteLine($"PackageMd5:{tt}, elapsedTime:{elapsedTime}");
        }

        private string CalculateMD5(string filePath)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);
            byte[] hash = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        public enum EType
        {
            EAD_XXX,
            EAD_YYY,
            EAD_ZZZ,
        }
        public EType LandingType = EType.EAD_XXX;
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            string s = "EAD_XXX1";
            Enum.TryParse(s, out LandingType);

            System.Diagnostics.Trace.WriteLine($"LandingType:{LandingType}");
            //LandingType = (EAdLandingType)Enum.Parse(typeof(EAdLandingType), s);
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
