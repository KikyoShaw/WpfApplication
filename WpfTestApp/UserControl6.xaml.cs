using System;
using System.Windows.Controls;

namespace WpfTestApp
{
    /// <summary>
    /// UserControl6.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl6 : UserControl
    {
        private MainVm _vm = null;

        public UserControl6()
        {
            InitializeComponent();
            this.DataContext = _vm = MainVm.Instance;
        }

        // 这里的width和height包括marigin
        public const double DefaultCardWidth = 240d;
        public const double DefaultCardHeight = 426d;
        private void ItemsControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            try
            {
                var itemControlWidth = e.NewSize.Width;
                if (itemControlWidth < 1)
                    return;

                var col = itemControlWidth / DefaultCardWidth;
                var colFloor = Math.Floor(col);

                _vm.CardWidth = itemControlWidth / colFloor;
                _vm.CardHeight = _vm.CardWidth / DefaultCardWidth * DefaultCardHeight;
            }
            catch /*(Exception exception)*/
            {
                //Console.WriteLine(exception);
                //throw;
            }
        }
    }
}
