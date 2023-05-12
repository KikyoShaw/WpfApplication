using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPopupAni
{
    /// <summary>
    /// UserControl3.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseClickPosition = e.GetPosition(MainGrid);

            Ellipse ripple = new Ellipse
            {
                Width = 0,
                Height = 0,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            MainGrid.Children.Add(ripple);
            ripple.RenderTransformOrigin = new Point(0.5, 0.5);
            ripple.RenderTransform = new TranslateTransform(mouseClickPosition.X - ripple.Width / 2, mouseClickPosition.Y - ripple.Height / 2);

            DoubleAnimation sizeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromMilliseconds(1000)
            };

            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1000)
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(sizeAnimation);
            storyboard.Children.Add(opacityAnimation);

            Storyboard.SetTarget(sizeAnimation, ripple);
            Storyboard.SetTarget(opacityAnimation, ripple);

            Storyboard.SetTargetProperty(sizeAnimation, new PropertyPath("(Ellipse.Width)"));
            Storyboard.SetTargetProperty(sizeAnimation, new PropertyPath("(Ellipse.Height)"));
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(Ellipse.Opacity)"));

            storyboard.Completed += (s, a) =>
            {
                MainGrid.Children.Remove(ripple);
            };

            storyboard.Begin();
        }
    }
}
