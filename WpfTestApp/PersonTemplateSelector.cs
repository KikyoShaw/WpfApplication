using System.Windows.Controls;
using System.Windows;

namespace WpfTestApp
{
    public class PersonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate YoungTemplate { get; set; }
        public DataTemplate ElderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Person person = item as Person;

            if (person != null)
            {
                if (person.Age < 30)
                    return YoungTemplate;
                else
                    return ElderTemplate;
            }

            return null;
        }
    }
}
