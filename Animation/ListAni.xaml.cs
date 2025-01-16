using System.Windows;
using System.Windows.Controls;

namespace Animation
{
    /// <summary>
    /// ListAni.xaml 的交互逻辑
    /// </summary>
    public partial class ListAni : UserControl
    {
        public ListAni()
        {
            InitializeComponent();
            DataContext = ViewModel.MainVm.Instance;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.MainVm.Instance.BtnClick();
        }
    }
}
