using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfTestApp
{
    /// <summary>
    /// FloatingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingWindow : Window
    {
        public FloatingWindow()
        {
            InitializeComponent();
            var margin = new Thickness(0, 0, 20, 0);
            AddTopArrow(margin);

            //AddRightArrow(margin);
        }

        /// <summary>
        /// 顶部箭头
        /// </summary>
        private void AddTopArrow(Thickness margin)
        {
            try
            {
                //先设置Grid位置
                xGrid.HorizontalAlignment = HorizontalAlignment.Center;
                xGrid.VerticalAlignment = VerticalAlignment.Top;
                xGrid.Margin = margin;

                // 创建一个 PathGeometry 对象
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure { StartPoint = new Point(30, 0) };
                figure.Segments.Add(new LineSegment(new Point(30, 8), true));
                figure.Segments.Add(new LineSegment(new Point(22, 8), true));
                figure.Segments.Add(new LineSegment(new Point(30, 0), true));
                figure.Segments.Add(new LineSegment(new Point(38, 8), true));
                figure.Segments.Add(new LineSegment(new Point(38, 8), true));
                figure.Segments.Add(new LineSegment(new Point(30, 8), true));
                geometry.Figures.Add(figure);

                // 创建一个 Path 对象并设置相关属性
                Path path = new Path
                {
                    Fill = Brushes.White,
                    Data = geometry,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // 添加 Path 到 Grid
                xGrid.Children.Add(path);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        private void AddRightArrow(Thickness margin)
        {
            try
            {
                //先设置Grid位置
                xGrid.HorizontalAlignment = HorizontalAlignment.Right;
                xGrid.VerticalAlignment = VerticalAlignment.Center;
                xGrid.Margin = margin;

                // 创建 PathGeometry 对象
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure { StartPoint = new Point(8, 8) };
                figure.Segments.Add(new LineSegment(new Point(0, 0), true));
                figure.Segments.Add(new LineSegment(new Point(0, 16), true));
                geometry.Figures.Add(figure);

                // 创建 Path 对象并设置属性
                Path path = new Path
                {
                    Data = geometry,
                    Fill = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // 将 Path 对象添加到 Grid
                xGrid.Children.Add(path);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        /// <summary>
        /// 设置箭头位置
        /// </summary>
        private void SetArrowPos(double l = 0, double r = 0, double t = 0, double b = 0)
        {
            xGrid.Margin = new Thickness(left: l, top: t, right: r, bottom: b);
        }
    }
}
