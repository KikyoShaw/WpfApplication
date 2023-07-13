using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows;

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

            CheckPc();
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
    }
}
