using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Animation
{
    public class AnimationFrameControl : Image
    {
        private class FrameInfo
        {
            public BitmapImage? Bmp { get; set; }

            public TimeSpan Duration { get; set; }
        }

        private ClockController? _clockController;
        private AnimationClock? _clock;
        //每帧的时间
        private TimeSpan _frameTime = TimeSpan.FromMilliseconds(50);
        //动画播放次数
        RepeatBehavior _repeatBehavior = RepeatBehavior.Forever;

        public AnimationFrameControl()
        {
        }

        /// <summary>
        /// 设置每帧的时间 - 默认50ms一帧
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

        /// <summary>
        /// 设置动画的图片
        /// </summary>
        public async void InitAnimationAsync(string sPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPath))
                    return;

                var animationSources = await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var imageList = GetBitmapListFromPath(sPath);

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
                    catch /*(Exception e)*/
                    {
                        //Console.WriteLine(e);
                        //throw;
                    }
                    return null;
                });

                if (animationSources == null)
                    return;

                var keyFrames = new ObjectKeyFrameCollection();
                var totalDuration = TimeSpan.Zero;
                foreach (var image in animationSources)
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
            catch /*(Exception e)*/
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        private static List<BitmapImage>? GetBitmapListFromPath(string sPath)
        {
            try
            {
                if (!Directory.Exists(sPath))
                    return null;

                DirectoryInfo cacheDir = new DirectoryInfo(sPath);
                FileInfo[] cacheFiles = cacheDir.GetFiles();
                if (cacheFiles.Length == 0)
                {
                    Directory.Delete(sPath, true);
                    return null;
                }

                List<BitmapImage> vResultList = new List<BitmapImage>();
                foreach (var f in cacheFiles)
                {
                    if (vResultList.Count >= 35)
                        break;

                    string fileType = f.Extension.ToLower();
                    if (fileType != ".png" && fileType != ".bmp" && fileType != ".jpg" &&
                        fileType != ".jpeg" && fileType != ".jpe")
                    {
                        continue;
                    }

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.UriSource = new Uri(f.FullName);
                    bmp.EndInit();
                    bmp.Freeze();
                    vResultList.Add(bmp);
                }

                return vResultList;
            }
            catch /*(Exception e)*/
            {
                //Console.WriteLine(e);
                //throw;
            }

            return null;
        }

    }
}
