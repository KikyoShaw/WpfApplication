using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using _2024_9_4_PictureScaleTransform.ViewModel;

namespace _2024_9_4_PictureScaleTransform
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
        }

        private void CardGrid_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            if (sender is Grid { DataContext: CardItem item } grid)
            {
                item.IsHover = true;
            }

            var h = MainViewModel.Instance.CardHeight - 12;
            var w = MainViewModel.Instance.CardWidth - 16;

            System.Diagnostics.Trace.WriteLine($"CardGrid_OnMouseEnter=> h:{h}, w:{w}");

            //Animation(grid,grid.ActualHeight,grid.ActualWidth, h + 96,w + 32,0.16);
        }

        private void CardGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            if (sender is Grid { DataContext: CardItem item } grid)
            {
                item.IsHover = false;

                var h = MainViewModel.Instance.CardHeight - 12;
                var w = MainViewModel.Instance.CardWidth - 16;

                System.Diagnostics.Trace.WriteLine($"CardGrid_OnMouseLeave=> h:{h}, w:{w}");

                //Animation(grid, grid.ActualHeight, grid.ActualWidth, h, w, 0.16);
            }
        }

        /// <summary>
        /// 动画效果
        /// </summary>
        /// <param name="IsMagnify"></param>
        private void Animation(object obj, double fromHeight, double fromWidth, double toHeight, double toWidth, double durationTime = 0.1)
        {
            if (obj == null) return;
            Storyboard storyboard = new Storyboard();
            var cardGrid = obj as Grid;
            try
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    From = fromHeight,
                    To = toHeight,
                    Duration = new Duration(TimeSpan.FromSeconds(durationTime)),
                };
                storyboard.Children.Add(heightAnimation);
                Storyboard.SetTarget(heightAnimation, cardGrid);
                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath("Height"));

                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    From = fromWidth,
                    To = toWidth,
                    Duration = new Duration(TimeSpan.FromSeconds(durationTime)),
                };
                storyboard.Children.Add(widthAnimation);
                Storyboard.SetTarget(widthAnimation, cardGrid);
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));

                var currentMargin = cardGrid.Margin;
                ThicknessAnimation marginAnimation = new ThicknessAnimation
                {
                    From = new Thickness(currentMargin.Left, currentMargin.Top, currentMargin.Right, currentMargin.Bottom),
                    To = new Thickness(currentMargin.Left - (toWidth - fromWidth) / 2, currentMargin.Top - (toHeight - fromHeight) / 2,
                                       currentMargin.Right - (toWidth - fromWidth) / 2, currentMargin.Bottom - (toHeight - fromHeight) / 1.5),
                    Duration = new Duration(TimeSpan.FromSeconds(durationTime))
                };
                storyboard.Children.Add(marginAnimation);
                Storyboard.SetTarget(marginAnimation, cardGrid);
                Storyboard.SetTargetProperty(marginAnimation, new PropertyPath("Margin"));

                // 开始动画
                storyboard.Begin();
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
        }

        // 这里的width和height包括marigin
        public const double DefaultCardWidth = 191d;
        public const double DefaultCardHeight = 321d;
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var iWidth = e.NewSize.Width;
                if (iWidth < 1)
                    return;

                var col = iWidth / DefaultCardWidth;
                var colFloor = Math.Floor(col);

                MainViewModel.Instance.CardWidth = iWidth / colFloor; ;
                MainViewModel.Instance.CardHeight = MainViewModel.Instance.CardWidth / DefaultCardWidth * DefaultCardHeight;

                //MainViewModel.Instance.InitGameSize();

                System.Diagnostics.Trace.WriteLine($"shaw=> MainViewModel.Instance.CardWidth:{MainViewModel.Instance.CardWidth}, MainViewModel.Instance.CardHeight:{MainViewModel.Instance.CardHeight}");
            }
            catch /*(Exception ex)*/
            {
                //
            }
        }
    }
}
