﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// NewBoxDetails.xaml 的交互逻辑
    /// </summary>
    public partial class NewBoxDetails : UserControl
    {
        public NewBoxDetails()
        {
            InitializeComponent();
        }

        private void ButtonClose_OnClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void TaskBtn_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
