using System.Windows.Controls;
using System.Windows;

namespace WpfTestApp
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HighTemplate { get; set; }

        public DataTemplate LowTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var stu = (Student)item;
            if (stu.Result > 60)
                return HighTemplate;
            else
                return LowTemplate;
        }
    }
}
