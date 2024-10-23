using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfBorder
{
    public class InnerBorder : Border
    {
        static InnerBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InnerBorder), new FrameworkPropertyMetadata(typeof(InnerBorder)));
        }

        public Brush InnerBorderBrush
        {
            get => (Brush)GetValue(InnerBorderBrushProperty);
            set => SetValue(InnerBorderBrushProperty, value);
        }

        public static readonly DependencyProperty InnerBorderBrushProperty =
            DependencyProperty.Register("InnerBorderBrush", typeof(Brush), typeof(InnerBorder), new PropertyMetadata(Brushes.Transparent));

        public Thickness InnerBorderThickness
        {
            get => (Thickness)GetValue(InnerBorderThicknessProperty);
            set => SetValue(InnerBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty InnerBorderThicknessProperty =
            DependencyProperty.Register("InnerBorderThickness", typeof(Thickness), typeof(InnerBorder), new PropertyMetadata(new Thickness(0)));

        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            //var posX = InnerBorderThickness.Left / 2.0d;
            //var posY = InnerBorderThickness.Top / 2.0d;
            //var w = ActualWidth - posX;
            //var h = ActualHeight - posY;
            //var innerBorderRect = new Rect(posX, posY, w, h);

            var cr = CornerRadius.TopLeft;
            //先绘制border原生背景
            var rect = new Rect(0, 0, ActualWidth, ActualHeight);
            dc.PushClip(new RectangleGeometry(rect, cr, cr));
            if (Background != null)
                dc.DrawRectangle(Background, null, new Rect(0, 0, ActualWidth, ActualHeight));
            //绘制内描边
            var left = InnerBorderThickness.Left;
            dc.DrawRoundedRectangle(null, new Pen(InnerBorderBrush, InnerBorderThickness.Left), rect, cr, cr);
            dc.Pop();
        }

    }
}
