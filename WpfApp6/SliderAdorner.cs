using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfApp6
{
    public class SliderAdorner : Adorner
    {
        private readonly Slider _slider;

        public SliderAdorner(UIElement adornedElement, Slider slider)
            : base(adornedElement)
        {
            _slider = slider;
            AddVisualChild(_slider);
            AddLogicalChild(_slider);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return _slider;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _slider.Measure(constraint);
            return _slider.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _slider.Arrange(new Rect(finalSize));
            return _slider.RenderSize;
        }
    }
}
