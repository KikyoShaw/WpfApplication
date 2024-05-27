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
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private Storyboard storyboard = new Storyboard();

        private void Init()
        {
            //var transformA = new TranslateTransform();
            //transformA.Y = 0;
            //transformA.X = 0;
            //A.RenderTransform = transformA;

            var animA = new DoubleAnimationUsingKeyFrames();
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0.6)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(-24, TimeSpan.FromSeconds(1.2)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(-24, TimeSpan.FromSeconds(3.2)));
            animA.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(3.8)));

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
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(3.2)));
            animB.KeyFrames.Add(new LinearDoubleKeyFrame(24, TimeSpan.FromSeconds(3.8)));
            //transformB.BeginAnimation(TranslateTransform.YProperty, animB);

            Storyboard.SetTarget(animA, A);
            Storyboard.SetTargetProperty(animA, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(animA);

            Storyboard.SetTarget(animB, B);
            Storyboard.SetTargetProperty(animB, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(animB);

            storyboard.Begin();
        }

        private void ClearAnimation()
        {
            
        }
    }
}
