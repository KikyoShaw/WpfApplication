using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Animation
{
    /// <summary>
    /// 动画类
    /// </summary>
    public class AnimationFrameView : Image
    {
        /// <summary>
        /// 动画帧信息
        /// </summary>
        private class FrameInfo
        {
            public BitmapImage Bmp { get; set; } = null;

            public TimeSpan Duration { get; set; }
        }

        public AnimationFrameView() { }

        static AnimationFrameView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimationFrameView), new FrameworkPropertyMetadata(typeof(AnimationFrameView)));
        }

        //动画组件
        private ClockController _clockController = null;
        private AnimationClock _clock = null;

        //每帧的时间 - 默认80ms
        private TimeSpan _frameTime = TimeSpan.FromMilliseconds(80);
        //动画播放次数
        RepeatBehavior _repeatBehavior = RepeatBehavior.Forever;

        /// <summary>
        /// 设置每帧的时间 - 默认80ms一帧
        /// </summary>
        public void SetFrameTime(TimeSpan frameTime)
        {
            _frameTime = frameTime;
        }

        /// <summary>
        /// 设置动画播放次数 - 默认无限循环
        /// </summary>
        public void SetRepeatBehavior(int count)
        {
            _repeatBehavior = new RepeatBehavior(count);
        }

        /// <summary>
        /// 启动动画
        /// </summary>
        public void StartAnimation()
        {
            _clockController?.Begin();
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public void StopAnimation()
        {
            _clockController?.Stop();
        }

        public string IconSource
        {
            get => (string)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(string), typeof(AnimationFrameView), new PropertyMetadata(ResourceChangePropertyCallBack));

        private static void ResourceChangePropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((AnimationFrameView)d is { } obj)
                    obj.RefreshResource();
            }
            catch (Exception e1)
            {
                System.Diagnostics.Debug.Write(e1.Message);
            }
        }

        public static readonly DependencyProperty IsVisibilityProperty = 
            DependencyProperty.Register("IsVisibility", typeof(bool), typeof(AnimationFrameView), new PropertyMetadata(IsVisibilityChangePropertyCallBack));

        private static void IsVisibilityChangePropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((AnimationFrameView)d is { } obj)
                {
                    var ret = obj.IsVisibility;
                    if(ret)
                        obj.StartAnimation();
                    else
                        obj.StopAnimation();
                }
            }
            catch (Exception e1)
            {
                System.Diagnostics.Debug.Write(e1.Message);
            }
        }

        public bool IsVisibility
        {
            get => (bool)GetValue(IsVisibilityProperty);
            set => SetValue(IsVisibilityProperty, value);
        }

        private void RefreshResource()
        {
            if (string.IsNullOrEmpty(IconSource))
                return;

            var sp = IconSource.Split("#");
            if (sp.Length >= 2)
            {
                var sUrl = sp[0];
                var sCount = sp[1];
                int.TryParse(sCount, out int frameCount);
                LoadAnimation(sUrl, frameCount);
            }

        }

        /// <summary>
        /// 加载动画 - 从资源中加载
        /// </summary>
        public async void LoadAnimation(string path, int frameCount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return;

                if (!path.Contains(@"pack://application"))
                    return;

                var sources = await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var imageList = GetImageListFromApplication(path, frameCount);

                        if (imageList == null)
                            return null;

                        var ret = new List<FrameInfo>();
                        foreach (var bmp in imageList)
                        {
                            ret.Add(new FrameInfo()
                            {
                                Bmp = bmp,
                                Duration = _frameTime
                            });
                        }
                        return ret;
                    }
                    catch /*(Exception ex)*/
                    {
                        //LogHelper.LogError(ex.Message);
                    }
                    return null;
                });

                Start(sources);
            }
            catch /*(Exception ex)*/
            {
                //LogHelper.LogError(ex.Message);
            }
        }

        /// <summary>
        /// 获取获取应用程序自带资源
        /// </summary>
        private List<BitmapImage> GetImageListFromApplication(string path, int frameCount)
        {
            try
            {
                var ret = new List<BitmapImage>();
                for (int i = 0; i < frameCount; i++)
                {
                    var uriString = string.Format(path, i);
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(uriString, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    bmp.Freeze();
                    ret.Add(bmp);
                }
                return ret;
            }
            catch /*(Exception ex)*/
            {
                //LogHelper.LogError(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 启动动画
        /// </summary>
        private void Start(List<FrameInfo> sources)
        {
            if (sources == null)
                return;

            var keyFrames = new ObjectKeyFrameCollection();
            var totalDuration = TimeSpan.Zero;
            foreach (var image in sources)
            {
                keyFrames.Add(new DiscreteObjectKeyFrame(image.Bmp, totalDuration));
                totalDuration += image.Duration;
            }

            var objectAnimation = new ObjectAnimationUsingKeyFrames()
            {
                KeyFrames = keyFrames,
                Duration = totalDuration,
                RepeatBehavior = _repeatBehavior
            };

            _clock = objectAnimation.CreateClock();
            _clockController = _clock.Controller;
            _clock.Completed += (sender, args) => { _clockController?.Stop(); };
            this.ApplyAnimationClock(SourceProperty, _clock);
        }

    }
}
