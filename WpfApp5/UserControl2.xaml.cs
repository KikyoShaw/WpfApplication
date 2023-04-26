using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        private int size1 = 20;
        private int size2 = 150;

        public UserControl2()
        {
            InitializeComponent();
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var ellipse = new Ellipse()
                {
                    Width = size1,
                    Height = size1,
                    Fill = Brushes.Red
                };

                var point = e.GetPosition(Canvas);
                var translateTransform = new TranslateTransform(point.X, point.Y);
                ellipse.RenderTransform = translateTransform;
                Canvas.Children.Add(ellipse);

                var storyboard = new Storyboard();

                //波纹扩散动画
                var widthAnimation = new DoubleAnimation(toValue: size2, new Duration(TimeSpan.FromSeconds(1)));
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
                Storyboard.SetTarget(widthAnimation, ellipse);
                storyboard.Children.Add(widthAnimation);
                var heightAnimation = new DoubleAnimation(toValue: size2, new Duration(TimeSpan.FromSeconds(1)));
                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath("Height"));
                Storyboard.SetTarget(heightAnimation, ellipse);
                storyboard.Children.Add(heightAnimation);
                //波纹扩散淡出特效
                var opacityAnimation = new DoubleAnimation(toValue: 0, new Duration(TimeSpan.FromSeconds(1)));
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                Storyboard.SetTarget(opacityAnimation, ellipse);
                storyboard.Children.Add(opacityAnimation);
                //波纹从鼠标点击中心开始扩散
                //ReSharper disable once PossibleLossOfFraction
                var translateTransformX = translateTransform.X - (size2 - size1) / 2;
                var xAnimation = new DoubleAnimation(toValue: translateTransformX, new Duration(TimeSpan.FromSeconds(1)));
                Storyboard.SetTargetProperty(xAnimation,
                    new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                Storyboard.SetTarget(xAnimation, ellipse);
                storyboard.Children.Add(xAnimation);
                // ReSharper disable once PossibleLossOfFraction
                var translateTransformY = translateTransform.Y - (size2 - size1) / 2;
                var yAnimation = new DoubleAnimation(toValue: translateTransformY, new Duration(TimeSpan.FromSeconds(1)));
                Storyboard.SetTargetProperty(yAnimation,
                    new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                Storyboard.SetTarget(yAnimation, ellipse);
                storyboard.Children.Add(yAnimation);

                storyboard.Completed += (o, args) =>
                {
                    Canvas.Children.Remove(ellipse);
                    storyboard.Stop();
                    storyboard.Children.Clear();
                };
                storyboard.Begin(this);
            }
            catch /*(Exception exception)*/
            {
                //Console.WriteLine(exception);
                //throw;
            }
        }
    }
}
