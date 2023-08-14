using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace WpfApp3
{
    public class NewTreasureBoxIVm : ViewModelBase
    {
        private static readonly Lazy<NewTreasureBoxIVm>
            Lazy = new Lazy<NewTreasureBoxIVm>(() => new NewTreasureBoxIVm());
        public static NewTreasureBoxIVm Instance => Lazy.Value;

        private NewTreasureBoxIVm()
        {
            //测试代码
            var banners = new ObservableCollection<BannerTextItem>();
            for (int i = 0; i < 3; i++)
            {
                var item = new BannerTextItem();
                if (i == 0)
                {
                    item.ItemText = "每日上线可多领";
                }
                else if (i == 1)
                {
                    item.TextIcon = "/WpfApp3;Component/Resources/box.png";
                }
                else if (i == 2)
                {
                    item.ItemText = "x4";
                }
                banners.Add(item);
            }

            BannerTexts = banners;
        }

        #region 百宝箱展示区域相关数据

        
        //toast提示文案

        #endregion

        #region 宝箱奖励介绍

        //宝箱奖励窗口是否显示
        private bool _awardDetailsVisible = false;

        public bool AwardDetailsVisible
        {
            get => _awardDetailsVisible;
            set => Set("AwardDetailsVisible", ref _awardDetailsVisible, value);
        }

        #endregion

        #region 宝箱相关数据



        #endregion

        #region banner相关数据

        //banner ICON
        private string _bannerIcon = "";

        public string BannerIcon
        {
            get => _bannerIcon;
            set => Set("BannerIcon", ref _bannerIcon, value);
        }

        //banner背景
        private string _bannerBg = "";

        public string BannerBg
        {
            get => _bannerBg;
            set => Set("BannerBg", ref _bannerBg, value);
        }

        //文案组成
        private ObservableCollection<BannerTextItem> _bannerTexts = new ObservableCollection<BannerTextItem>();
        public ObservableCollection<BannerTextItem> BannerTexts
        {
            get => _bannerTexts;
            set => Set("BannerTexts", ref _bannerTexts, value);
        }

        //按钮
        private string _bannerBtn = "";

        public string BannerBtn
        {
            get => _bannerBtn;
            set => Set("BannerBtn", ref _bannerBtn, value);
        }

        #endregion

        #region Toast弹窗相关数据

        //记录上一次toast文案
        private string _lastToastText = "";

        //控制弹窗出现时机
        private bool _toastShow = false;
        public bool ToastShow
        {
            get => _toastShow;
            set
            {
                Set("ToastShow", ref _toastShow, value);
                if (value)
                {
                    //var needStop = _lastToastText != ToastText;
                    //InitStartToastTimer(needStop);
                    InitStartToastTimer();
                }
                else
                {
                    _toastTimer?.Stop();
                    _shouldStop = true;
                }
            }
        }

        //弹窗文案
        private string _toastText = "";
        public string ToastText
        {
            get => _toastText;
            set
            {
                Set("ToastText", ref _toastText, value);
                ToastShow = !string.IsNullOrEmpty(value);
                _lastToastText = value;
            }
        }

        //控制toast 3s消失的定时器
        private System.Timers.Timer _toastTimer = null;

        private volatile bool _shouldStop;

        public void InitStartToastTimer(bool needStop = true)
        {
            if (_toastTimer == null)
            {
                _toastTimer = new System.Timers.Timer(10000);
                _toastTimer.Elapsed += (s, args) =>
                {
                    Thread.Sleep(5000);
                    if(_shouldStop)
                        return;
                    ToastText = "";
                };
            }

            if (needStop)
            {
                if (_toastTimer.Enabled)
                    _toastTimer.Stop();
                _toastTimer.Start();
                _shouldStop = false;
            }
            else
            {
                if (_toastTimer?.Enabled == false)
                    _toastTimer?.Start();
            }
        }

        #endregion
    }
}
