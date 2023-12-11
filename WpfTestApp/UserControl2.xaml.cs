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

namespace WpfTestApp
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

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var BButton = sender as Button;
            var AGrid = BButton?.Parent as Grid;

            Storyboard storyboard = new Storyboard();
            Duration duration = new Duration(TimeSpan.FromSeconds(5));

            DoubleAnimation animationX = new DoubleAnimation(0, AGrid.ActualWidth / 2 - (BButton.ActualWidth / 2) - 10, duration);
            DoubleAnimation animationY = new DoubleAnimation(0, AGrid.ActualHeight / 2 - (BButton.ActualHeight / 2) - 10, duration);
            DoubleAnimation animationWidth = new DoubleAnimation(150, 75, duration);
            DoubleAnimation animationHeight = new DoubleAnimation(150, 75, duration);
            DoubleAnimation animationOpacity = new DoubleAnimation(1.0, 0.5, duration);

            Storyboard.SetTarget(animationX, BTransform);
            Storyboard.SetTarget(animationY, BTransform);
            Storyboard.SetTarget(animationWidth, BButton);
            Storyboard.SetTarget(animationHeight, BButton);
            Storyboard.SetTarget(animationOpacity, BButton);

            Storyboard.SetTargetProperty(animationX, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetProperty(animationY, new PropertyPath(TranslateTransform.YProperty));
            Storyboard.SetTargetProperty(animationWidth, new PropertyPath(WidthProperty));
            Storyboard.SetTargetProperty(animationHeight, new PropertyPath(HeightProperty));
            Storyboard.SetTargetProperty(animationOpacity, new PropertyPath(OpacityProperty));

            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Children.Add(animationWidth);
            storyboard.Children.Add(animationHeight);
            storyboard.Children.Add(animationOpacity);

            storyboard.Begin();
        }
    }
}
