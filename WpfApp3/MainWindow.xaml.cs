﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json.Linq;

namespace WpfApp3
{
    public class A
    {
        public Dictionary<string, string> myDictionary { get; set; } = new Dictionary<string, string>();
        public string name { get; set; } = "AAAA";
        public int age { get; set; } = 11;
    }

    public class LiveBoxAward
    {
        int _iSourceType = 0;
        public int iSourceType
        {
            get
            {
                return _iSourceType;
            }
            set
            {
                _iSourceType = value;
            }
        }

        int _iType = 0;
        public int iType
        {
            get
            {
                return _iType;
            }
            set
            {
                _iType = value;
            }
        }

        int _iCount = 0;
        public int iCount
        {
            get
            {
                return _iCount;
            }
            set
            {
                _iCount = value;
            }
        }

        string _sName = "";
        public string sName
        {
            get
            {
                return _sName;
            }
            set
            {
                _sName = value;
            }
        }

        string _sIcon = "";
        public string sIcon
        {
            get
            {
                return _sIcon;
            }
            set
            {
                _sIcon = value;
            }
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<List<LiveBoxAward>> _allNewBoxAwards = new List<List<LiveBoxAward>>();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = NewTreasureBoxIVm.Instance;

            var sUrl = @"http://te�Ef";
            var result = CheckIsUrlFormat(sUrl);

            bool result2 = Uri.IsWellFormedUriString(sUrl, UriKind.RelativeOrAbsolute);

            Test();
             
            int tt = 39;
            int kk = tt / 60;
            var hh = tt % 60;

            bool result1 = true;

            string str = $"1234567890_{result1}";
            if (str.Contains("True"))
            {
                // 找到 "_" 的位置
                int underscoreIndex = str.IndexOf('_');

                // 从 "_" 位置开始截取剩下的字符串
                string boolStr = str.Substring(underscoreIndex + 1);

                // 将字符串转换为布尔值
                bool.TryParse(boolStr, out bool result3);
            }

            //测试svg
            var images = new List<DrawingImage>();
            var x = NumSvgDrawingTools.Instance.GetDrawingImage(10);
            var x1 = NumSvgDrawingTools.Instance.GetDrawingImage(1);
            var x2 = NumSvgDrawingTools.Instance.GetDrawingImage(2);
            var x3 = NumSvgDrawingTools.Instance.GetDrawingImage(3);
            var x4 = NumSvgDrawingTools.Instance.GetDrawingImage(4);
            var x5 = NumSvgDrawingTools.Instance.GetDrawingImage(5);
            var x6 = NumSvgDrawingTools.Instance.GetDrawingImage(6);
            var x7 = NumSvgDrawingTools.Instance.GetDrawingImage(7);
            var x8 = NumSvgDrawingTools.Instance.GetDrawingImage(8);
            var x9 = NumSvgDrawingTools.Instance.GetDrawingImage(9);
            images.Add(x);
            images.Add(x1);
            images.Add(x2);
            images.Add(x3);
            images.Add(x4);
            images.Add(x5);
            images.Add(x6);
            images.Add(x7);
            images.Add(x8);
            //images.Add(x9);
            var image = NumSvgDrawingTools.Instance.MergeDrawingImages(-4, images);
            ImageSvg.Source = image;

            var bytes = GetA() ?? GetB();
        }

        private byte[] GetA()
        {
            return null;
        }

        private byte[] GetB()
        {
            return null;
        }

        private void Test()
        {
            for (int k = 0; k < 3; k++)
            {
                var ttt = new List<LiveBoxAward>();
                for (int i = 1; i < 7; i++)
                {
                    var item = new LiveBoxAward();
                    item.iType = i;
                    item.iSourceType = 2 * i;
                    item.iCount = i * 2;
                    item.sName = $"测试{i}";
                    ttt.Add(item);
                }
                _allNewBoxAwards.Add(ttt);
            }

            List<LiveBoxAward> flatList = _allNewBoxAwards.SelectMany(x => x).ToList();
            var groups = flatList.GroupBy(x => new { x.iSourceType, x.iType });

            List<LiveBoxAward> mergedList = new List<LiveBoxAward>();

            foreach (var group in groups)
            {
                mergedList.Add(new LiveBoxAward
                {
                    iType = group.Key.iType,
                    iSourceType = group.Key.iSourceType,
                    iCount = group.Sum(x => x.iCount),
                    sName = group.First().sName,
                    sIcon = group.First().sIcon
                });
            }


        }

        /// <summary>
        /// 检测串值是否为合法的网址格式
        /// </summary>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsUrlFormat(string strValue)
        {
            return CheckIsFormat(@"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", strValue);
        }

        /// <summary>
        /// 检测串值是否为合法的格式
        /// </summary>
        /// <param name="strRegex">正则表达式</param>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsFormat(string strRegex, string strValue)
        {
            try
            {
                if (strValue != null && strValue.Trim() != "")
                {
                    Regex re = new Regex(strRegex);
                    if (re.IsMatch(strValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        /// <summary>
        /// 检测链接是否为合法的网址格式
        /// </summary>
        /// <param name="uri">待检测的链接</param>
        /// <returns></returns>
        public bool CheckUrlIsValid(string uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                    return false;

                var regex = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                Regex re = new Regex(regex);
                return re.IsMatch(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        private System.Timers.Timer _toastTimer = null;

        private Window2 wnd = null;

        private NewTreasureBoxWnd _newTreasureBoxWnd { get; set; } = null;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //if (_newTreasureBoxWnd == null)
            //{
            //    _newTreasureBoxWnd = new NewTreasureBoxWnd();
            //    _newTreasureBoxWnd.Closed += (o, args) => { _newTreasureBoxWnd = null; };
            //    _newTreasureBoxWnd.ShowDialog();
            //}

            test();

            //if (wnd == null)
            //{
            //    wnd = new Window2();
            //    wnd.Closed += (o, args) => { wnd.Visibility = Visibility.Collapsed; };
            //}
            //wnd.Show();

            //A a = new A();
            //a.myDictionary.Add("Key1", "Value1");
            //a.myDictionary.Add("Key2", "Value2");
            //a.myDictionary.Add("Key3", "Value3");
            //System.Diagnostics.Trace.WriteLine($"{JsonSerializer.Serialize(a)}");
            //NewTreasureBoxIVm.Instance.ToastShow = true;
            //NewTreasureBoxIVm.Instance.ToastText = "1111";

            //TextBlock1.Text = "1111";
            //StackPanel1.Visibility = Visibility.Visible;
            //if (_toastTimer != null)
            //{
            //    if (_toastTimer.Enabled)
            //        _toastTimer.Stop();
            //    _toastTimer.Start();
            //}

            //if (_toastTimer == null)
            //{
            //    _toastTimer = new System.Timers.Timer(10000);
            //    _toastTimer.Elapsed += OnMsgChatElapsed;
            //    _toastTimer.Start();
            //}
        }

        private Mutex _mutexUpdateNotice = null;

        private Semaphore _semaphore = null;

        private void test()
        {
            //var mutexUpdateNotice = new Mutex(true, "HuYaPc-Update-Notice-{09C278EE-7933-4919-986E-7F1CDE84C489}", out var bFirstCreate);
            //if (bFirstCreate)
            //{
            //    int i = 0;
            //    i++;
            //    _mutexUpdateNotice = mutexUpdateNotice;

            //    Debug.WriteLine("1111111");
            //}
            //else
            //{
            //    mutexUpdateNotice?.Dispose();
            //}
            //

            //_semaphore = new Semaphore(1, 1, "HuYaPc-Update-Notice-{666666EE-7933-4919-986E-7F1CDE84C489}", out var bFirstCreate);
            //if (bFirstCreate)
            //{
            //    int i = 0;
            //    i++;
            //}
        }

        private Dictionary<string, int> UpdateCountDic = new Dictionary<string, int>();

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            string json = "{\"UpdateCount\":4,\"UpdateInterval\":24,\"LastUpdateDateTime\":1695371567,\"UpdateCountDic\":null}";

            var jsObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            if (jsObj == null)
            {
                return;
            }

            var playUri = jsObj.GetValue("playUrl");


            //UpdateCountDic.Add("5.58.1.0", 1);
            //UpdateCountDic.Add("5.57.1.0", 1);
            //UpdateCountDic.Add("5.59.1.0", 1);
            //UpdateCountDic.Add("5.57.2.0", 1);
            //UpdateCountDic.Add("5.60.1.0", 1);
            //UpdateCountDic.Add("5.55.1.0", 1);
            //UpdateCountDic.Add("5.53.1.0", 1);
            //UpdateCountDic.Add("5.59.3.0", 1);

            ////UpdateCountDic.OrderBy(pair => pair.Key, CompareVersion);
            //UpdateCountDic = UpdateCountDic.OrderBy(pair => pair.Key, new VersionComparer()).Skip(UpdateCountDic.Count - 5).ToDictionary(pair => pair.Key, pair => pair.Value);

            //bool t = false;
            //if (_mutexUpdateNotice != null)
            //{
            //    t = _mutexUpdateNotice.WaitOne(0);
            //}

            //if (t)
            //{
            //_mutexUpdateNotice?.ReleaseMutex();
            // _mutexUpdateNotice?.Dispose();
            //}

            //try
            //{
            //    _mutexUpdateNotice?.Dispose();
            //}
            //catch
            //{

            //}

            //_semaphore?.Release(1);
        }

        public class VersionComparer : IComparer<string>
        {
            public int CompareVersion(string version1, string version2)
            {
                try
                {
                    var isVersion1Empty = string.IsNullOrEmpty(version1);
                    var isVersion2Empty = string.IsNullOrEmpty(version2);

                    if (isVersion1Empty || isVersion2Empty)
                    {
                        return (isVersion1Empty ? -1 : 1) - (isVersion2Empty ? -1 : 1);
                    }

                    var version1List = version1.Split('.');
                    var version2List = version2.Split('.');

                    var commonVersionCount = Math.Min(version1List.Length, version2List.Length);

                    for (var i = 0; i < commonVersionCount; i++)
                    {
                        int.TryParse(version1List[i], out var iVerNum1);
                        int.TryParse(version2List[i], out var iVerNum2);
                        if (iVerNum1 != iVerNum2)
                            return iVerNum1 > iVerNum2 ? 1 : -1;
                    }

                    return version1List.Length - version2List.Length;
                }
                catch { }

                return 0;
            }

            public int Compare(string x, string y)
            {
                return CompareVersion(x, y);
            }
        }

        //private void OnMsgChatElapsed(object sender, ElapsedEventArgs e)
        //{
        //    //await Task.Delay(5000);
        //    if(_toastTimer.Enabled) 
        //        _toastTimer.Stop();

        //    NewTreasureBoxIVm.Instance.ToastShow = false;
        //    //Dispatcher.Invoke(new Action(() =>
        //    //{
        //    //    TextBlock1.Text = "";
        //    //    StackPanel1.Visibility = Visibility.Collapsed;
        //    //}), DispatcherPriority.Normal);
        //}
    }
}
