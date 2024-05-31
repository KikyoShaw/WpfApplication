using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfSvg
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            Init();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //_clockControllerA?.Begin();
            //_clockControllerB?.Begin();

            storyboard.Begin();
        }

        private Storyboard storyboard = new Storyboard();

        private ClockController _clockControllerA;

        private ClockController _clockControllerB;

        private void Init()
        {
            //var transformA = new TranslateTransform();
            //transformA.Y = 0;
            //transformA.X = 0;
            //A.RenderTransform = transformA;

            var animA = new DoubleAnimationUsingKeyFrames();
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0.6)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(-24, TimeSpan.FromSeconds(1.2)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(-24, TimeSpan.FromSeconds(11.2)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(11.8)));

            //var animA = new DoubleAnimation(0, -54, TimeSpan.FromMilliseconds(600)) { BeginTime = TimeSpan.FromSeconds(2) };
            //transformA.BeginAnimation(TranslateTransform.YProperty, animA);

            //var transformB = new TranslateTransform();
            //transformB.Y = 24;
            //transformB.X = 0;
            //B.RenderTransform = transformB;
            //var animB = new DoubleAnimation(54, 0, TimeSpan.FromMilliseconds(600)) { BeginTime = TimeSpan.FromSeconds(2) };

            var animB = new DoubleAnimationUsingKeyFrames();
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(24, TimeSpan.FromSeconds(0.6)));
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(1.2)));
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(11.2)));
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(24, TimeSpan.FromSeconds(11.8)));

            //创建并应用动画A的Clock
            //animA.Freeze();
            //AnimationClock clockA = animA.CreateClock();
            //A.RenderTransform = new TranslateTransform() { X = 0, Y = 0 };
            //A.RenderTransform.ApplyAnimationClock(TranslateTransform.YProperty, clockA);
            //_clockControllerA = clockA.Controller;

            //创建并应用动画B的Clock
            //animB.Freeze();
            //AnimationClock clockB = animB.CreateClock();
            //B.RenderTransform = new TranslateTransform() { X = 0, Y = 24 };
            //B.RenderTransform.ApplyAnimationClock(TranslateTransform.YProperty, clockB);
            //_clockControllerB = clockB.Controller;

            //transformB.BeginAnimation(TranslateTransform.YProperty, animB);

            Storyboard.SetTarget(animA, A);
            Storyboard.SetTargetProperty(animA, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(animA);

            Storyboard.SetTarget(animB, B);
            Storyboard.SetTargetProperty(animB, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(animB);

        }

        private void ClearAnimation()
        {
            
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            //_clockControllerA?.Stop();
            //_clockControllerB?.Stop();

            storyboard.Stop();
        }
    }
}
