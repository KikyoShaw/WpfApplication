using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WpfApp5
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

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var ellipse = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.LightBlue,
                    Opacity = 1,
                };

                var point = e.GetPosition(Canvas);
                var translateTransform = new TranslateTransform(point.X, point.Y);
                ellipse.RenderTransform = translateTransform;
                Canvas.Children.Add(ellipse);

                //波纹扩散动画特效
                //宽度从10到200
                var widthAnimation = new DoubleAnimation
                {
                    From = 10,
                    To = 200,
                    Duration = TimeSpan.FromSeconds(1.5),
                    AutoReverse = false,
                };
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Ellipse.WidthProperty));
                Storyboard.SetTarget(widthAnimation, ellipse);
                //高度从10到200
                var heightAnimation = new DoubleAnimation
                {
                    From = 10,
                    To = 200,
                    Duration = TimeSpan.FromSeconds(1.5),
                    AutoReverse = false,
                };
                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Ellipse.HeightProperty));
                Storyboard.SetTarget(heightAnimation, ellipse);

                //波纹扩散淡出动画特效
                var opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1.5),
                };
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Ellipse.OpacityProperty));
                Storyboard.SetTarget(opacityAnimation, ellipse);

                //波纹从鼠标点击中心开始扩散动画特效
                //x轴上开始扩散
                //ReSharper disable once PossibleLossOfFraction
                var posX = translateTransform.X - (200 - 10) / 2;
                var posXAnimation = new DoubleAnimation
                {
                    To = posX,
                    Duration = TimeSpan.FromSeconds(1.5),
                };
                Storyboard.SetTargetProperty(posXAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                Storyboard.SetTarget(posXAnimation, ellipse);
                //y轴上开始扩散
                // ReSharper disable once PossibleLossOfFraction
                var posY = translateTransform.Y - (200 - 10) / 2;
                var posYAnimation = new DoubleAnimation
                {
                    To = posY,
                    Duration = TimeSpan.FromSeconds(1.5),
                };
                Storyboard.SetTargetProperty(posYAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                Storyboard.SetTarget(posYAnimation, ellipse);
                //添加动画组
                var storyboard = new Storyboard();
                storyboard.Children.Add(widthAnimation);
                storyboard.Children.Add(heightAnimation);
                storyboard.Children.Add(opacityAnimation);
                storyboard.Children.Add(posXAnimation);
                storyboard.Children.Add(posYAnimation);
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
