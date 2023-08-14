using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// NewTreasureBoxWnd.xaml 的交互逻辑
    /// </summary>
    public partial class NewTreasureBoxWnd : Window
    {
        public NewTreasureBoxWnd()
        {
            InitializeComponent();
            this.DataContext = NewTreasureBoxIVm.Instance;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonRuler_OnClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
