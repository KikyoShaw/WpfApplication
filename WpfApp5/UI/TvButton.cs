using System.Windows;
using System.Windows.Controls;

namespace WpfApp5.UI
{
    public class TvButton : Button
    {
        static TvButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TvButton), new FrameworkPropertyMetadata(typeof(TvButton)));
        }

        //tips文案
        public static readonly DependencyProperty TipsTextProperty = DependencyProperty.Register("TipsText", typeof(string), typeof(TvButton));

        public string TipsText
        {
            get => (string)GetValue(TipsTextProperty);
            set => SetValue(TipsTextProperty, value);
        }

        //按钮文案
        public static readonly DependencyProperty BtnTextProperty = DependencyProperty.Register("BtnText", typeof(string), typeof(TvButton));

        public string BtnText
        {
            get => (string)GetValue(BtnTextProperty);
            set => SetValue(BtnTextProperty, value);
        }


    }
}
