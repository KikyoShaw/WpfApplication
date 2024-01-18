using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfTestApp.Btn
{
    public enum ButtonType
    {
        normal = 0,
        download = 1,
        hoverDownload = 2,
    }

    public class DownLoadBtnBaseCtrl : Button
    {
        protected Action<double, double> OnDownloadProgressChanged = null;
        protected Action<ButtonType, ButtonType> OnButtonTypeChanged = null;
        protected bool ProgressChanging = false;

        public DownLoadBtnBaseCtrl()
        {
            this.Loaded += BaseDownloadBtn_Loaded;
            this.MouseEnter += BaseDownloadBtn_MouseEnter;
            this.MouseLeave += BaseDownloadBtn_MouseLeave;
            this.IsVisibleChanged += DownLoadBtnBaseCtrl_IsVisibleChanged;
            this.SizeChanged += DownLoadBtnBaseCtrl_SizeChanged;
        }

        


        #region Download

        /// <summary>
        /// 按钮类型
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(ButtonType.normal, PropertyCallback));
        public ButtonType ButtonType
        {
            get => (ButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        /// <summary>
        /// 下载按钮背景颜色
        /// </summary>
        public static readonly DependencyProperty DownloadBackgroundProperty =
            DependencyProperty.Register("DownloadBackground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush DownloadBackground
        {
            get => (Brush)GetValue(DownloadBackgroundProperty);
            set => SetValue(DownloadBackgroundProperty, value);
        }

        /// <summary>
        /// 下载进度
        /// </summary>
        public static readonly DependencyProperty DownloadProgressProperty =
            DependencyProperty.Register("DownloadProgress", typeof(double), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata((double)0, PropertyCallback));
        public double DownloadProgress
        {
            get => (double)GetValue(DownloadProgressProperty);
            set 
            {
                value = LimitProgress(value);
                SetValue(DownloadProgressProperty, value);
            }
        }

        /// <summary>
        /// 下载进度裁剪区
        /// </summary>
        public static readonly DependencyProperty ClipRectProperty =
            DependencyProperty.Register("ClipRect", typeof(RectangleGeometry), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(new RectangleGeometry(new Rect(0, 0, 0, 0)), null));
        public RectangleGeometry ClipRect
        {
            get => (RectangleGeometry)GetValue(ClipRectProperty);
            set => SetValue(ClipRectProperty, value);
        }

        #endregion

        #region Border

        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(new CornerRadius(0), null));
        public CornerRadius BorderRadius
        {
            get => (CornerRadius)GetValue(BorderRadiusProperty);
            set => SetValue(BorderRadiusProperty, value);
        }

        public static readonly DependencyProperty BtnBackgroundProperty =
            DependencyProperty.Register("BtnBackground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush BtnBackground
        {
            get => (Brush)GetValue(BtnBackgroundProperty);
            set => SetValue(BtnBackgroundProperty, value);
        }

        public new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(new Thickness(0), null));
        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush HoverBorderBrush
        {
            get => (Brush)GetValue(HoverBorderBrushProperty);
            set => SetValue(HoverBorderBrushProperty, value);
        }

        public static readonly DependencyProperty PushBorderBrushProperty =
            DependencyProperty.Register("PushBorderBrush", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush PushBorderBrush
        {
            get => (Brush)GetValue(PushBorderBrushProperty);
            set => SetValue(PushBorderBrushProperty, value);
        }

        #endregion

        #region Background

        /// <summary>
        /// Hover 背景
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush HoverBackground
        {
            get => (Brush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        /// <summary>
        /// Push 背景
        /// </summary>
        public static readonly DependencyProperty PushBackgroundProperty =
            DependencyProperty.Register("PushBackground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush PushBackground
        {
            get => (Brush)GetValue(PushBackgroundProperty);
            set => SetValue(PushBackgroundProperty, value);
        }

        /// <summary>
        /// Disable 背景
        /// </summary>
        public static readonly DependencyProperty DisableBackgroundProperty =
            DependencyProperty.Register("DisableBackground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Transparent, null));
        public Brush DisableBackground
        {
            get => (Brush)GetValue(DisableBackgroundProperty);
            set => SetValue(DisableBackgroundProperty, value);
        }

        #endregion

        #region Text

        /// <summary>
        /// Hover 文字颜色
        /// </summary>
        public static readonly DependencyProperty FontOpacityProperty =
            DependencyProperty.Register("FontOpacity", typeof(double), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata((double)1, null));
        public double FontOpacity
        {
            get => (double)GetValue(FontOpacityProperty);
            set => SetValue(FontOpacityProperty, value);
        }

        /// <summary>
        /// Hover 文字颜色
        /// </summary>
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Black, null));
        public Brush HoverForeground
        {
            get => (Brush)GetValue(HoverForegroundProperty);
            set => SetValue(HoverForegroundProperty, value);
        }

        /// <summary>
        /// Push 文字颜色
        /// </summary>
        public static readonly DependencyProperty PushForegroundProperty =
            DependencyProperty.Register("PushForeground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Black, null));
        public Brush PushForeground
        {
            get => (Brush)GetValue(PushForegroundProperty);
            set => SetValue(PushForegroundProperty, value);
        }

        /// <summary>
        /// Disable 文字颜色
        /// </summary>
        public static readonly DependencyProperty DisableForegroundProperty =
            DependencyProperty.Register("DisableForeground", typeof(Brush), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(Brushes.Black, null));
        public Brush DisableForeground
        {
            get => (Brush)GetValue(DisableForegroundProperty);
            set => SetValue(DisableForegroundProperty, value);
        }

        /// <summary>
        /// 文字边距
        /// </summary>
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(new Thickness(0), null));
        public Thickness TextMargin
        {
            get => (Thickness)GetValue(TextMarginProperty);
            set => SetValue(TextMarginProperty, value);
        }

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public static readonly DependencyProperty TextHoriAlignmentProperty =
            DependencyProperty.Register("TextHoriAlignment", typeof(HorizontalAlignment), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(HorizontalAlignment.Center, null));
        public HorizontalAlignment TextHoriAlignment
        {
            get => (HorizontalAlignment)GetValue(TextHoriAlignmentProperty);
            set => SetValue(TextHoriAlignmentProperty, value);
        }

        public static readonly DependencyProperty TextVerAlignmentProperty =
            DependencyProperty.Register("TextVerAlignment", typeof(VerticalAlignment), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(VerticalAlignment.Center, null));
        public VerticalAlignment TextVerAlignment
        {
            get => (VerticalAlignment)GetValue(TextVerAlignmentProperty);
            set => SetValue(TextVerAlignmentProperty, value);
        }

        public static readonly DependencyProperty TextBtnTrimmingProperty = DependencyProperty.Register("TextBtnTrimming", typeof(TextTrimming), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata(TextTrimming.None, null));
        public TextTrimming TextBtnTrimming
        {
            get => (TextTrimming)GetValue(TextBtnTrimmingProperty);
            set => SetValue(TextBtnTrimmingProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DownLoadBtnBaseCtrl), new FrameworkPropertyMetadata("", null));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region event

        private void BaseDownloadBtn_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateClipRect();
        }

        Brush _saveForeground = null;
        Brush _saveBtnBackground = null;
        Brush _saveBorderBrush = null;

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Foreground = HoverForeground;
            BtnBackground = HoverBackground;
            BorderBrush = HoverBorderBrush;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Foreground = PushForeground;
            BtnBackground = PushBackground;
            BorderBrush = PushBorderBrush;
        }


        private void BaseDownloadBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Foreground = _saveForeground;
            BtnBackground = _saveBtnBackground;
            BorderBrush = _saveBorderBrush;
        }

        private void BaseDownloadBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            _saveForeground = Foreground;
            _saveBtnBackground = BtnBackground;
            _saveBorderBrush = BorderBrush;
            Foreground = HoverForeground;
            BtnBackground = HoverBackground;
            BorderBrush = HoverBorderBrush;
        }


        private void DownLoadBtnBaseCtrl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateClipRect();
        }

        private void DownLoadBtnBaseCtrl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateClipRect();
        }


        #endregion

        private void UpdateClipRect()
        {
            ClipRect = new RectangleGeometry( new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            if (this.ButtonType != ButtonType.normal)
            {
                var progress = LimitProgress(DownloadProgress);
                ClipRect = new RectangleGeometry(new Rect(0, 0, ActualWidth * progress / 100, ActualHeight));
            }
        }

        private static void PropertyCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                // 加一个进度条动画组件
                var btn = depObj as DownLoadBtnBaseCtrl;

                if (btn == null)
                {
                    return;
                }

                switch (e.Property.Name)
                {
                    case "DownloadProgress":
                        var newProgress = LimitProgress((double)e.NewValue);
                        var oldProgress = LimitProgress((double)e.OldValue);
                        

                        if (btn.ButtonType == ButtonType.normal)
                        {
                            if ((double)e.NewValue < 0)
                            {
                                btn.DownloadProgress = 0;
                            }
                            else if ((double)e.NewValue > 100)
                            {
                                btn.DownloadProgress = 100;
                            }
                            return;
                        }

                        if (btn.ProgressChanging == true)
                        {
                            btn.ClipRect = new RectangleGeometry(new Rect(0, 0, btn.ActualWidth * newProgress / 100, btn.ActualHeight));
                            return;
                        }
                        btn.OnDownloadProgressChanged?.Invoke(oldProgress, newProgress);

                        if ((double)e.NewValue < 0)
                        {
                            btn.DownloadProgress = 0;
                        }
                        else if ((double)e.NewValue > 100)
                        {
                            btn.DownloadProgress = 100;
                        }
                        
                        break;

                    case "ButtonType":
                        if (btn.ButtonType == ButtonType.download || btn.ButtonType == ButtonType.hoverDownload)
                        {
                            var progress2 = LimitProgress(btn.DownloadProgress);
                            btn.ClipRect = new RectangleGeometry(new Rect(0, 0, btn.ActualWidth * progress2 / 100, btn.ActualHeight));
                        }
                        else
                        {
                            btn.ClipRect = new RectangleGeometry(new Rect(0, 0, btn.ActualWidth, btn.ActualHeight));
                        }
                        btn.OnButtonTypeChanged?.Invoke((ButtonType)e.OldValue, (ButtonType)e.NewValue);
                        break;


                }
            }
            catch /*(Exception ex)*/
            {
                //LogHelper.LogError(ex.Message);
            }
        }

        private static double LimitProgress(double newProgress)
        {
            if (newProgress < 0)
            {
                return 0;
            }
            else if (newProgress > 100)
            {
                return 100;
            }
            return newProgress;

        }

    }
}
