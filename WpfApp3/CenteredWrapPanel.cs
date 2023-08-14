using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public class CenteredWrapPanel : WrapPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = base.ArrangeOverride(finalSize);

            double num = 0.0;
            double num2 = 0.0;

            foreach (UIElement element in base.InternalChildren)
            {
                if (element.Visibility != Visibility.Collapsed)
                {
                    num2 = Math.Max(num2, element.DesiredSize.Height);
                    if ((element.DesiredSize.Width + num) > finalSize.Width)
                    {
                        ArrangeLine(num2, num);
                        num = element.DesiredSize.Width;
                        num2 = element.DesiredSize.Height;
                    }
                    else
                    {
                        num += element.DesiredSize.Width;
                    }
                }
            }

            ArrangeLine(num2, num);

            return size;
        }

        private void ArrangeLine(double lineHeight, double lineWidth)
        {
            double num = (this.Orientation == Orientation.Horizontal) ? ((this.Width - lineWidth) / 2.0) : 0.0;
            double num2 = (this.Orientation == Orientation.Vertical) ? ((this.Height - lineHeight) / 2.0) : 0.0;

            foreach (UIElement element in base.InternalChildren)
            {
                Rect finalRect = new Rect(new Point(num, num2), element.DesiredSize);
                element.Arrange(finalRect);
                if (this.Orientation == Orientation.Horizontal)
                {
                    num += element.DesiredSize.Width;
                }
                else
                {
                    num2 += element.DesiredSize.Height;
                }
            }
        }
    }
}
