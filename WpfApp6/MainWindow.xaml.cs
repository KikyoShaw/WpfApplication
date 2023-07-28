using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Interop;
using System.Windows.Media;
using System.Management;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TestDictionary();
            //var slider = new Slider
            //{
            //    Width = 150,
            //    Height = 30,
            //    Opacity = 0.75,

            //    VerticalAlignment = VerticalAlignment.Center,
            //    Margin = new Thickness(0)
            //};

            //var adornerLayer = AdornerLayer.GetAdornerLayer(mainGrid);
            //adornerLayer?.Add(new SliderAdorner(mainGrid, slider));

            // CheckPc();
            //TaskTest();

            //for (int i = 0; i < 30; i++)
            //{
            //    string sUrl = $"https://ffilelogweb2.huya.com/#/feedback?uidType=0&collectStatus=0&contentKeyword=%E6%91%B8&logBeginTime=1687249415823&appId=200&isNotAutoFeedback=1&stop=0&queryDistinct=0{i}/";
            //    var stopWatch = new Stopwatch();
            //    stopWatch.Start();
            //    string sMd5 = GetMd5(sUrl);
            //    stopWatch.Stop();
            //    System.Diagnostics.Trace.WriteLine($"sMd5:{sMd5}, 接口耗时: {stopWatch.ElapsedTicks}");

            //    stopWatch.Start();
            //    string sHash256 = GetHash256(sUrl);
            //    stopWatch.Stop();
            //    System.Diagnostics.Trace.WriteLine($"sHash256:{sHash256}, 接口耗时: {stopWatch.ElapsedTicks}");

            //    stopWatch.Start();
            //    string sHash1 = GetHash1(sUrl);
            //    stopWatch.Stop();
            //    System.Diagnostics.Trace.WriteLine($"sHash1:{sHash1}, 接口耗时: {stopWatch.ElapsedTicks}");
            //}


            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            //foreach (ManagementObject queryObj in searcher.Get())
            //{
            //    System.Diagnostics.Trace.WriteLine($"CPU 制造商: {queryObj["Manufacturer"]}");
            //    System.Diagnostics.Trace.WriteLine($"CPU 描述: {queryObj["Name"]}");
            //}

            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            //foreach (ManagementObject obj in searcher.Get())
            //{
            //    string manufacturer = obj["Manufacturer"].ToString();
            //    string serialNumber = obj["ProcessorID"].ToString();
            //    string socketDesignation = obj["SocketDesignation"].ToString();
            //    DateTime installDate;

            //    if (obj["InstallDate"] != null)
            //    {
            //        installDate = ManagementDateTimeConverter.ToDateTime(obj["InstallDate"].ToString());
            //    }
            //    else
            //    {
            //        installDate = DateTime.MinValue;
            //    }

            //    System.Diagnostics.Trace.WriteLine("Manufacturer: " + manufacturer);
            //    System.Diagnostics.Trace.WriteLine("Serial Number: " + serialNumber);
            //    System.Diagnostics.Trace.WriteLine("Socket Designation: " + socketDesignation);
            //    System.Diagnostics.Trace.WriteLine("Install Date: " + installDate);
            //}
            CpuHandler();
        }

        private async void AAA()
        {
            string cpuModel = "Intel Core i7-9700K"; // 提供您要查询的 CPU 型号
            string cpuWorldSearchUrl = $"https://www.cpu-world.com/cgi-bin/Search.cgi?Search={Uri.EscapeDataString(cpuModel)}";

            using HttpClient httpClient = new HttpClient();
            string searchPageContent = await httpClient.GetStringAsync(cpuWorldSearchUrl);

            HtmlDocument searchPageDocument = new HtmlDocument();
            searchPageDocument.LoadHtml(searchPageContent);

            HtmlNodeCollection searchResults = searchPageDocument.DocumentNode.SelectNodes("//table[contains(@class, 'results_table')]//a");
            if (searchResults == null || searchResults.Count == 0)
            {
                Console.WriteLine("找不到匹配的结果");
                return;
            }

            string cpuPageUrl = searchResults[0].GetAttributeValue("href", "");
            string cpuPageContent = await httpClient.GetStringAsync(cpuPageUrl);

            HtmlDocument cpuPageDocument = new HtmlDocument();
            cpuPageDocument.LoadHtml(cpuPageContent);

            HtmlNodeCollection cpuDetails = cpuPageDocument.DocumentNode.SelectNodes("//table[contains(@id, 'details')]//tr");
            if (cpuDetails == null)
            {
                Console.WriteLine("找不到 CPU 详细信息");
                return;
            }

            string productionDate = "生产日期未知";
            foreach (HtmlNode detailRow in cpuDetails)
            {
                if (detailRow.SelectSingleNode("td")?.InnerText?.Trim().ToLower() == "introduction date")
                {
                    string dateText = detailRow.SelectSingleNode("td[2]")?.InnerText?.Trim();
                    Match dateMatch = Regex.Match(dateText, @"\b\d{4}[,-]\s*\w{3}\b");
                    if (dateMatch.Success)
                    {
                        productionDate = dateMatch.Value;
                    }
                    break;
                }
            }

            Console.WriteLine($"CPU 型号: {cpuModel}");
            Console.WriteLine($"生产日期: {productionDate}");
        }

        private async void CpuHandler()
        {
            string cpuModel = GetCpuInfo();
            if (string.IsNullOrEmpty(cpuModel))
            {
                System.Diagnostics.Trace.WriteLine("未能获取 CPU 型号");
                return;
            }

            string cpuWorldSearchUrl = $"https://www.cpu-world.com/cgi-bin/Search.cgi?Search={Uri.EscapeDataString(cpuModel)}";

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
            httpClient.Timeout = TimeSpan.FromSeconds(3000);
            string searchPageContent = await httpClient.GetStringAsync(cpuWorldSearchUrl);

            HtmlDocument searchPageDocument = new HtmlDocument();
            searchPageDocument.LoadHtml(searchPageContent);

            HtmlNodeCollection searchResults = searchPageDocument.DocumentNode.SelectNodes("//table[contains(@class, 'results_table')]//a");
            if (searchResults == null || searchResults.Count == 0)
            {
                System.Diagnostics.Trace.WriteLine("找不到匹配的结果");
                return;
            }

            string cpuPageUrl = searchResults[0].GetAttributeValue("href", "");
            string cpuPageContent = await httpClient.GetStringAsync(cpuPageUrl);

            HtmlDocument cpuPageDocument = new HtmlDocument();
            cpuPageDocument.LoadHtml(cpuPageContent);

            HtmlNodeCollection cpuDetails = cpuPageDocument.DocumentNode.SelectNodes("//table[contains(@id, 'details')]//tr");
            if (cpuDetails == null)
            {
                System.Diagnostics.Trace.WriteLine("找不到 CPU 详细信息");
                return;
            }

            string productionDate = "生产日期未知";
            foreach (HtmlNode detailRow in cpuDetails)
            {
                if (detailRow.SelectSingleNode("td")?.InnerText?.Trim().ToLower() == "introduction date")
                {
                    string dateText = detailRow.SelectSingleNode("td[2]")?.InnerText?.Trim();
                    Match dateMatch = Regex.Match(dateText, @"\b\d{4}[,-]\s*\w{3}\b");
                    if (dateMatch.Success)
                    {
                        productionDate = dateMatch.Value;
                    }
                    break;
                }
            }

            System.Diagnostics.Trace.WriteLine($"CPU 型号: {cpuModel}");
            System.Diagnostics.Trace.WriteLine($"生产日期: {productionDate}");
        }

        private string GetCpuInfo()
        {
            string cpuModel = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                cpuModel = obj["Name"]?.ToString()?.Trim();
                break; // 获取第一个处理器（CPU）产生的模型
            }

            return cpuModel;
        }

        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="sUrl"></param>
        /// <returns></returns>
        private string GetMd5(string sUrl)
        {
            if (string.IsNullOrWhiteSpace(sUrl))
                return "";

            using System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] ret = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sUrl));

            StringBuilder sBuilder = new StringBuilder();
            foreach (var t in ret)
                sBuilder.Append(t.ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取哈希256
        /// </summary>
        /// <param name="sUrl"></param>
        /// <returns></returns>
        private string GetHash256(string sUrl)
        {
            if (string.IsNullOrWhiteSpace(sUrl))
                return "";

            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(sUrl);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder hashBuilder = new StringBuilder();
            foreach (var t in hashBytes)
                hashBuilder.Append(t.ToString("x2"));

            return hashBuilder.ToString();
        }

        private string GetHash1(string sUrl)
        {
            if (string.IsNullOrWhiteSpace(sUrl))
                return "";

            using var sha1 = new SHA1Managed();
            byte[] inputBytes = Encoding.UTF8.GetBytes(sUrl);
            byte[] hashBytes = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Task 测试
        /// </summary>
        private void TaskTest()
        {
            // 设置固定的线程数
            int fixedWorkerThreads = 2;

            // 获取当前线程池中异步I/O线程数量
            //ThreadPool.GetMinThreads(out _, out int currentMinCompletionThreads);
            //ThreadPool.GetMaxThreads(out _, out int currentMaxCompletionThreads);

            ThreadPool.SetMinThreads(fixedWorkerThreads, fixedWorkerThreads);
            ThreadPool.SetMaxThreads(fixedWorkerThreads, fixedWorkerThreads);

            ManualResetEvent enEvent = new ManualResetEvent(false);
            List<Task> tasks = new List<Task>();
            // 使用 Task.Run 运行任务 (默认将在 ThreadPool 中运行)
            for (int i = 1; i <= 30; i++)
            {
                int taskNumber = i;
                tasks.Add(Task.Run(() =>
                {
                    enEvent.WaitOne();
                    int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    System.Diagnostics.Trace.WriteLine($"{nameof(Task)} {taskNumber} running in ThreadPool with {fixedWorkerThreads} threads. currentThreadId:{currentThreadId}");
                    //Thread.Sleep(1000);
                }));
                tasks.Add(Task.Run(() =>
                {
                    enEvent.WaitOne();
                    int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    System.Diagnostics.Trace.WriteLine($"{nameof(Task)} {taskNumber} running2 in ThreadPool with {fixedWorkerThreads} threads. currentThreadId:{currentThreadId}");
                    //Thread.Sleep(1000);
                }));
                //Task.Run(() =>
                //{
                //    int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                //    System.Diagnostics.Trace.WriteLine($"{nameof(Task)} {taskNumber} running in ThreadPool with {fixedWorkerThreads} threads. currentThreadId:{currentThreadId}");
                //    Thread.Sleep(1000);
                //});
            }

            enEvent.Set();
            Task.WaitAll(tasks.ToArray());
            enEvent.Dispose();

            //// 任务数量
            //int taskCount = 30;

            //// 创建并初始化CountdownEvent实例
            //CountdownEvent countdown = new CountdownEvent(taskCount);

            //// 使用 Task.Run 运行任务 (默认将在 ThreadPool 中运行)
            //for (int i = 1; i <= taskCount; i++)
            //{
            //    int taskNumber = i;
            //    Task.Run(() =>
            //    {
            //        // 等待挡板
            //        countdown.Signal();
            //        countdown.Wait();

            //        int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            //        System.Diagnostics.Trace.WriteLine($"{nameof(Task)} {taskNumber} running in ThreadPool with {fixedWorkerThreads} threads. currentThreadId:{currentThreadId}");

            //        Thread.Sleep(1000);
            //    });
            //}

            //// 确保主线程不会在完成任务前退出
            //countdown.Wait();
            //countdown.Dispose();

            System.Diagnostics.Trace.WriteLine("...");
        }

        private void TestDictionary()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                // 添加项到字典中
                { "item1", 1 },
                { "sitem3", 7 },
                { "item5", 3 },
                { "item4", 4 }
            };

            foreach (var item in dict)
            {
                dict.Remove(item.Key);
            }

            List<int> list = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            for (int i = 0; i < list.Count; i++)
            {
                list.Remove(i);
            }

            foreach (var item in list)
            {
                if (item % 2 == 0)
                {
                    list.Remove(item);
                }
            }
        }

        /// <summary>
        /// 检测设备的CPU或网络负载
        /// </summary>
        private void CheckPc()
        {
            // 获取当前进程的CPU使用率
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float cpuUsage = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            cpuUsage = cpuCounter.NextValue();
            System.Diagnostics.Trace.WriteLine($"CPU使用率: {cpuUsage}");

            //网络
            //var networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", "Network Interface Name");
            //float receivedBytesPerSec = networkCounter.NextValue();
            //System.Diagnostics.Trace.WriteLine($"receivedBytesPerSec:{receivedBytesPerSec}");
            // 获取当前网络带宽使用率
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    IPv4InterfaceStatistics statistics =
                        ni.GetIPv4Statistics();

                    long bytesSentSpeed = 0;
                    if (ni.Speed > 0)
                    {
                        bytesSentSpeed = (long)(statistics.BytesSent * 8) / ni.Speed * 100;
                    }

                    long bytesReceivedSpeed = 0;
                    if (ni.Speed > 0)
                    {
                        bytesReceivedSpeed = (long)(statistics.BytesReceived * 8) / ni.Speed * 100;
                    }

                    System.Diagnostics.Trace.WriteLine($"网络接口名称：{ni.Name}");
                    System.Diagnostics.Trace.WriteLine($"已发送数据速率：{bytesSentSpeed}");
                    System.Diagnostics.Trace.WriteLine($"已接收数据速率：{bytesReceivedSpeed}");
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string sUrl = "https://stackify.com/nullreferenceexception-object-reference-not-set/";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string sMd5 = GetMd5(sUrl);
            stopWatch.Stop();
            string sHash256 = GetHash256(sUrl);
            System.Diagnostics.Trace.WriteLine($"sMd5:{sMd5}, 接口耗时: {stopWatch.ElapsedMilliseconds} 毫秒");
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            string sUrl = "https://stackify.com/nullreferenceexception-object-reference-not-set/";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string sHash256 = GetHash256(sUrl);
            stopWatch.Stop();
            System.Diagnostics.Trace.WriteLine($"sHash256:{sHash256}, 接口耗时: {stopWatch.ElapsedMilliseconds} 毫秒");
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            string sUrl = "https://stackify.com/nullreferenceexception-object-reference-not-set/";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string sHash1 = GetHash1(sUrl);
            stopWatch.Stop();
            System.Diagnostics.Trace.WriteLine($"sHash1:{sHash1}, 接口耗时: {stopWatch.ElapsedMilliseconds} 毫秒");
        }
    }
}
