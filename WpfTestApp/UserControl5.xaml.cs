using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Logging;

namespace WpfTestApp
{
    /// <summary>
    /// UserControl5.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl5 : UserControl
    {
        private readonly ILogger<UserControl5> _logger;
        public UserControl5(ILogger<UserControl5> logger)
        {
            InitializeComponent();
            _logger = logger;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _logger.LogInformation("Button clicked.");
        }
    }
}
