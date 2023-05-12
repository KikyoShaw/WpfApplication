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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void FadeInButtonClick(object sender, RoutedEventArgs e)
        {
            // Scale animation
            DoubleAnimation scaleAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));

            BorderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            BorderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            // Opacity animation
            DoubleAnimation opacityAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));

            MyBorder.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void FadeOutButtonClick(object sender, RoutedEventArgs e)
        {
            // Scale animation
            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(500)));

            BorderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            BorderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            // Opacity animation
            DoubleAnimation opacityAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(500)));

            MyBorder.BeginAnimation(OpacityProperty, opacityAnimation);
        }
    }
}
