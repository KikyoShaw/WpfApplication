using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Management;
using System.Security.AccessControl;
using System.Threading.Tasks;

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

            int totalPages = (int)Math.Ceiling((double)0 / 96);


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

        private void ButtonBase4_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sUrl = "https://ttc.gdt.qq.com/gdt_click.fcg?viewid=Eq4PydrpQuGdu_AQ_U6Ln_xSWUhOGSltbGcXBvPgXpfp9asR!n_t6IjWvrc3Z3G8TYPxLE1CsBSHhWvEOLUnbpIcFzjzgZcsDDhrEsvzFlsPZn2JcOglFLliOIcjZXBv9qfkdXp_XxGRjnWUxu0KdPzZxnoE0ZV5puu8NBvwOLE&jtype=0&wspm=1&i=1&os=2&clklpp=__CLICK_LPP__&cdnxj=1&xp=2&pid=2037989259468491&vto=__VIDEO_PLAY_TIME__&report_source=__REPORT_SOURCE__&device_os_type=android&tl=1&video=%7b%22bt%22%3a%220%22%2c%22et%22%3a%226225%22%2c%22bf%22%3a%221%22%2c%22ef%22%3a%220%22%2c%22pp%22%3a%220%22%2c%22pa%22%3a%2211%22%2c%22ft%22%3a%222%22%7d";
                //加个防御
                if (string.IsNullOrEmpty(sUrl) || !sUrl.StartsWith("http")
                                               || !Uri.IsWellFormedUriString(sUrl, UriKind.RelativeOrAbsolute))
                    return;

                //绕过证书验证
                //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                // 根据uri创建HttpWebRequest对象 
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(sUrl);
                //对发送的数据不使用缓存 
                httpReq.AllowWriteStreamBuffering = false;
                httpReq.Timeout = 4000;
                httpReq.Method = "GET";
                //获取服务器端的响应 
                HttpWebResponse webResponse = (HttpWebResponse)httpReq.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                if (stream == null)
                    return;
                StreamReader streamReader = new StreamReader(stream);
                //读取服务器端返回的消息 
                var sResponse = streamReader.ReadToEnd();
                stream.Close();
                streamReader.Close();
                int iStatusCode = (int)webResponse.StatusCode;
                if ((iStatusCode / 100) != 2)
                {
                    System.Diagnostics.Trace.WriteLine("VideoMgr do GetRequest url:" + sUrl + ", status code: " + iStatusCode.ToString() + " msg:" + sResponse);
                }

                System.Diagnostics.Trace.WriteLine($"第三方上报请求完成：iStatusCode:{iStatusCode}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Exception:{ex.Message}");
            }
        }

        private void ButtonBase5_OnClick(object sender, RoutedEventArgs e)
        {
            GetCpuCores();
            GetGraphicsCard();
        }

        private void GetCpuCores()
        {
            var coreCount = "";
            var logicalProcessorCount = "";
            var maxClockSpeed = "";

            foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += item["NumberOfCores"].ToString();
                logicalProcessorCount += item["ThreadCount"].ToString();
                maxClockSpeed = item["MaxClockSpeed"].ToString();
            }

            System.Diagnostics.Trace.WriteLine("CPU Physical Cores(CPU 物理核心): " + coreCount);
            System.Diagnostics.Trace.WriteLine("CPU Logical Processors(CPU逻辑处理器): " + logicalProcessorCount);
            System.Diagnostics.Trace.WriteLine("CPU Max Speed (MHz)(CPU最大频率): " + maxClockSpeed);
        }

        private void GetGraphicsCard()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_VideoController");

            var videoControllers = searcher.Get();
            if (videoControllers.Count <= 0)
            {
                System.Diagnostics.Trace.WriteLine("本机没装显卡");
                return;
            }
            System.Diagnostics.Trace.WriteLine("本机有装显卡");
            foreach (var o in videoControllers)
            {
                var mo = (ManagementObject)o;
                System.Diagnostics.Trace.WriteLine("Name(名称): " + mo["Name"]);
                System.Diagnostics.Trace.WriteLine("Status(状态): " + mo["Status"]);
                System.Diagnostics.Trace.WriteLine("Caption(标题): " + mo["Caption"]);
                System.Diagnostics.Trace.WriteLine("DeviceID(设备ID): " + mo["DeviceID"]);
                System.Diagnostics.Trace.WriteLine("AdapterRAM(适配器RAM): " + mo["AdapterRAM"]);
                System.Diagnostics.Trace.WriteLine("AdapterDACType(适配器DAC类型): " + mo["AdapterDACType"]);
                System.Diagnostics.Trace.WriteLine("Monochrome(单色模式): " + mo["Monochrome"]);
                System.Diagnostics.Trace.WriteLine("InstalledDisplayDrivers(已安装的显示驱动) : " + mo["InstalledDisplayDrivers"]);
                System.Diagnostics.Trace.WriteLine("DriverVersion(驱动版本): " + mo["DriverVersion"]);
                System.Diagnostics.Trace.WriteLine("VideoProcessor(视频处理器): " + mo["VideoProcessor"]);
                System.Diagnostics.Trace.WriteLine("VideoArchitecture(视频架构): " + mo["VideoArchitecture"]);
                System.Diagnostics.Trace.WriteLine("VideoMemoryType(视频内存类型): " + mo["VideoMemoryType"]);
            }
        }

        private void GetCpuName()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                var capabilities = mo["Name"].ToString();
                //foreach (var capability in capabilities)
                //{
                //    if (capability.Contains("avx2"))
                //    {
                //        System.Diagnostics.Trace.WriteLine("Your CPU supports H.265 media");
                //        return;
                //    }
                //}
            }

            System.Diagnostics.Trace.WriteLine("Your CPU not supports H.265 media");
        }

        private void ButtonBase6_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // 创建一个WMI查询来获取Hyperv的状态
                var query = new ObjectQuery("SELECT * FROM Win32_Service WHERE Name = 'vmms'");

                // 创建一个ManagementObjectSearcher对象来执行WMI查询
                var searcher = new ManagementObjectSearcher(query);

                // 获取查询到的服务对象（应该只有一个）
                var services = searcher.Get();

                foreach (var o in services)
                {
                    var service = (ManagementObject)o;
                    string serviceName = service["Name"].ToString();
                    string serviceState = service["State"].ToString();

                    if (serviceState == null)
                    {
                        System.Diagnostics.Trace.WriteLine("检测Hyperv状态时出错：serviceState is null.");
                        return;
                    }

                    // 检查服务状态是否为"Running"，表示Hyperv已开启
                    System.Diagnostics.Trace.WriteLine(
                        serviceState.Equals("Running", StringComparison.OrdinalIgnoreCase) ? "Hyperv已开启" : "Hyperv未开启");
                }
            }
            catch (ManagementException ex)
            {
                System.Diagnostics.Trace.WriteLine("检测Hyperv状态时出错：" + ex.Message);
            }
        }

        private void ButtonBase7_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(CloseHyperV);
        }

        private void CloseHyperV()
        {
            try
            {
                // 创建一个新的进程对象
                Process process = new Process();

                // 定义进程的启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo();

                // 设置启动信息的属性
                startInfo.FileName = "cmd.exe"; // 指定要执行的命令行程序
                startInfo.Arguments = "/C dism.exe /Online /Disable-Feature:Microsoft-Hyper-V"; // 指定要执行的命令行参数
                startInfo.Verb = "runas"; // 以管理员权限运行
                startInfo.RedirectStandardOutput = true; // 将输出重定向到标准输出流
                startInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动进程
                startInfo.CreateNoWindow = true; // 不创建新窗口显示进程

                // 将启动信息设置到进程对象中
                process.StartInfo = startInfo;

                // 启动进程
                process.Start();

                // 读取进程的标准输出流
                string output = process.StandardOutput.ReadToEnd();

                // 等待进程结束
                process.WaitForExit();

                // 输出命令的执行结果
                System.Diagnostics.Trace.WriteLine(output);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("调用cmd指令时出错：" + ex.Message);
            }
        }

        private void ButtonBase8_OnClick(object sender, RoutedEventArgs e)
        {
            ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Processor");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (var o in searcher.Get())
            {
                var mobj = (ManagementObject)o;
                bool? virtualizationEnabled = mobj["SecondLevelAddressTranslationExtensions"] as bool?;
                System.Diagnostics.Trace.WriteLine("VT Enabled: " + virtualizationEnabled);
            }
        }
    }
}
