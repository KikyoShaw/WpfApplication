using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoadDemo
{
	/// <summary>
	/// Child.xaml 的交互逻辑
	/// </summary>
	public partial class Child : UserControl
	{
		public Child()
		{
			InitializeComponent();
			Loaded += Child_Loaded; 
		}

		private void Child_Loaded(object sender, RoutedEventArgs e)
		{

			

			var talkList_result = talkList.IsVisible;
			var talkList_index = System.Windows.Media.VisualTreeHelper.GetChildrenCount(talkList);
			var index2 = System.Windows.LogicalTreeHelper.GetChildren(talkList);

			var stackPanel_result = stackPanel.IsVisible;
			var stackPanel_index = System.Windows.Media.VisualTreeHelper.GetChildrenCount(stackPanel);

			//var index1 = System.Windows.Media.VisualTreeHelper.GetChild(talkList, 0) as Border;
			//Border border = System.Windows.Media.VisualTreeHelper.GetChild(talkList, 0) as Border;
			System.Diagnostics.Trace.WriteLine($"talkList.IsVisible = {talkList_result}, talkList_index = {talkList_index}" + "\n" +
				$"stackPanel.IsVisible = {stackPanel_result}, stackPanel_index = {stackPanel_index}");
		}

		private void Btn1_Click(object sender, RoutedEventArgs e)
		{
			talkList.Visibility = Visibility.Visible;
			var index = System.Windows.Media.VisualTreeHelper.GetChildrenCount(talkList);
			System.Diagnostics.Trace.WriteLine($"index = {index}");
			var index2 = System.Windows.LogicalTreeHelper.GetChildren(talkList);
			var index1 = System.Windows.Media.VisualTreeHelper.GetChild(talkList, 0) as Border;
		}

		private void Btn2_Click(object sender, RoutedEventArgs e)
		{
			talkList.Visibility = Visibility.Collapsed;
			var index = System.Windows.Media.VisualTreeHelper.GetChildrenCount(talkList);
			System.Diagnostics.Trace.WriteLine($"index = {index}");
		}
	}
}
