using System;
using System.Windows;
using System.Windows.Controls;

namespace Animation
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                var path = dialog.SelectedPath;
                //AnimationControl.SetRepeatBehavior(1);
                AnimationControl.InitAnimationAsync(path);
            }
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            AnimationControl.StartAnimation();
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            AnimationControl.StopAnimation();
        }
    }
}
