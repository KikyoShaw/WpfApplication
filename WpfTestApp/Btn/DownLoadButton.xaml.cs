using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfTestApp.Btn
{
    /// <summary>
    /// DownLoadButton.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoadButton : DownLoadBtnBaseCtrl
    {
        private const double AMIN_TIME = 0.3;

        private readonly Storyboard _progressStoryboard = new Storyboard();
        private Border BlackMask = null;
        private TextBlock tb = null;
        private TextBlock progressTb = null;
        private Border brRoot = null;
        public DownLoadButton()
        {
            InitializeComponent();
            this.Loaded += DownLoadButton_Loaded;
            this.Unloaded += DownLoadButton_Unloaded;
        }

        private void DownLoadButton_Unloaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeave -= DownloadButton_MouseLeave;
            this.MouseEnter -= DownloadButton_MouseEnter;
            this.OnDownloadProgressChanged -= Amin_Progress;
            this.OnButtonTypeChanged -= ButtonTypeChangedHandle;
        }

        private void DownLoadButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeave += DownloadButton_MouseLeave;
            this.MouseEnter += DownloadButton_MouseEnter;
            this.OnDownloadProgressChanged += Amin_Progress;
            this.OnButtonTypeChanged += ButtonTypeChangedHandle;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ButtonTypeChangedHandle(ButtonType, ButtonType);

        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (BlackMask == null)
                BlackMask = (Border)this.Template.FindName("BlackMask", this);
            if (BlackMask == null)
                return;
            BlackMask.Visibility = Visibility.Collapsed;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (BlackMask == null)
                BlackMask = (Border)this.Template.FindName("BlackMask", this);
            if (BlackMask == null)
                return;
            BlackMask.Visibility = Visibility.Visible;
        }

        private void DownloadButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DownLoadButton { ButtonType: ButtonType.download } btn)
            {
                btn.ButtonType = ButtonType.hoverDownload;
            }
        }

        private void DownloadButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is DownLoadButton { ButtonType: ButtonType.hoverDownload } btn)
            {
                btn.ButtonType = ButtonType.download;
                if (BlackMask == null)
                    BlackMask = (Border)btn.Template.FindName("BlackMask", btn);
                if (BlackMask == null)
                    return;
                BlackMask.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonTypeChangedHandle(ButtonType oldValue, ButtonType newValue)
        {
            if (tb == null)
                tb = (TextBlock)this.Template.FindName("tb", this);
            if (progressTb == null)
                progressTb = (TextBlock)this.Template.FindName("progress", this);
            if (brRoot == null)
                brRoot = (Border)this.Template.FindName("brRoot", this);

            if (tb == null || progressTb == null || brRoot == null)
                return;

            if (newValue == ButtonType.normal)
            {
                tb.Visibility = Visibility.Visible;
                brRoot.Visibility = Visibility.Visible;
                progressTb.Visibility = Visibility.Collapsed;
            }
            else if (newValue == ButtonType.download)
            {
                tb.Visibility = Visibility.Collapsed;
                brRoot.Visibility = Visibility.Visible;
                progressTb.Visibility = Visibility.Visible;
            }
            else if (newValue == ButtonType.hoverDownload)
            {
                tb.Visibility = Visibility.Visible;
                brRoot.Visibility = Visibility.Visible;
                progressTb.Visibility = Visibility.Collapsed;
            }
        }

        private void Amin_Progress(double oldValue, double newValue)
        {
            try
            {
                if (this.ButtonType != ButtonType.download)
                    return;

                this.ProgressChanging = true;
                _progressStoryboard.Stop();
                _progressStoryboard.Children.Clear();

                DoubleAnimation progressAnimation = new DoubleAnimation
                {
                    From = oldValue,
                    To = newValue,
                    Duration = TimeSpan.FromSeconds(AMIN_TIME)
                };

                progressAnimation.Completed += ProgressAnimation1_Completed;
                _progressStoryboard.Children.Add(progressAnimation);
                Storyboard.SetTarget(progressAnimation, this);
                Storyboard.SetTargetProperty(progressAnimation, new PropertyPath(DownloadProgressProperty));
                _progressStoryboard.Begin();
            }
            catch /*(Exception ex)*/
            {
                //LogHelper.LogError(ex.Message);
            }
        }

        private void ProgressAnimation1_Completed(object sender, EventArgs e)
        {
            try
            {
                this.ProgressChanging = false;
                _progressStoryboard.Stop();
                _progressStoryboard.Children.Clear();
            }
            catch/* (Exception ex)*/
            {
                //LogHelper.LogError(ex.Message);
            }
        }

    }
}
