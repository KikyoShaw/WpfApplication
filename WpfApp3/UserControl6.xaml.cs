using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace WpfApp3
{
    /// <summary>
    /// UserControl6.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl6 : UserControl
    {
        public UserControl6()
        {
            InitializeComponent();
            StartProgressBarAnimation();
        }

        private void StartProgressBarAnimation()
        {
            var progressAnimation = new DoubleAnimation
            {
                From = 0,
                To = 280,
                Duration = new Duration(System.TimeSpan.FromSeconds(2)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            AnimatedProgressBar.BeginAnimation(RangeBase.ValueProperty, progressAnimation);
        }
    }
}
