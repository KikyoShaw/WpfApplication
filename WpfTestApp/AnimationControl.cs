using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfTestApp
{
    public class AnimationControl : UserControl
    {
        private int _imageIndex = 0;

        private bool _start = false;

        private int _resourceCount = 30;

        private readonly Dictionary<int, BitmapImage> _resourceMap = new Dictionary<int, BitmapImage>();

        private readonly System.Timers.Timer _animationTimer = null;

        public AnimationControl()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                LoadResources();
            }));

            _animationTimer = new System.Timers.Timer();
            _animationTimer.Interval = 50;
            _animationTimer.Elapsed += AnimationTimer_Elapsed;
        }

        public static readonly DependencyProperty IsStartProperty = DependencyProperty.Register(
            "IsStart", typeof(bool), typeof(AnimationControl),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsStartPropertyChange)));

        public bool IsStart
        {
            get => (bool)GetValue(IsStartProperty);
            set => SetValue(IsStartProperty, value);
        }

        private static void OnIsStartPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AnimationControl obj = ((AnimationControl)(AnimationControl)d);
            if (obj != null)
                obj.DoStartStageChange();
        }

        private void DoStartStageChange()
        {
            if (IsStart)
            {
                if (!_start)
                {
                    _start = true;
                    _animationTimer?.Start();
                }
            }
            else
            {
                if (_start)
                {
                    _start = false;
                    Stop();
                    _imageIndex = 0;
                    InvalidateVisual();
                }
            }
        }

        private void LoadResources()
        {
            for (int i = 0; i < _resourceCount; i++)
            {
                string path = $"pack://application:,,,/WpfTestApp;component/Resources/{i}.png";
                var bmp = new BitmapImage(new Uri(path));
                bmp.Freeze();
                _resourceMap.Add(i, bmp);
            }
        }

        private BitmapImage GetResource(int index)
        {
            if (_resourceMap.ContainsKey(index))
                return _resourceMap[index];
            else
                _imageIndex = 0;
            return null;
        }

        private void Start()
        {
            if (!_start)
            {
                _start = !_start;
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
        }
        private void Stop()
        {
            if (_start)
            {
                _start = !_start;
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
            }
        }

        private void AnimationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher?.Invoke(new Action(() =>
            {
                _imageIndex++;
                _imageIndex = _imageIndex % _resourceCount;
                InvalidateVisual();
            }));
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            _imageIndex++;
            _imageIndex = _imageIndex % _resourceCount;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            var img = GetResource(_imageIndex);
            if (img != null)
                dc.DrawImage(img, new Rect(0, 0, ActualWidth, ActualHeight));
        }
    }
}
