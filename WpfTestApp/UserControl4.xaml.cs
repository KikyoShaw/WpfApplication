using System.Windows.Controls;

namespace WpfTestApp
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// UserControl4.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
            Person person = new Person { Name = "John", Age = 75 };
            DataContext = person;
        }
    }
}
