using System.Windows;

namespace WpfAppNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = MainVm.Instance;
            InitializeComponent();
        }
    }
}
