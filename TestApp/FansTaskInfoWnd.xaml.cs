using System;
using System.Collections.Generic;
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

namespace TestApp
{
	/// <summary>
	/// FansTaskInfoWnd.xaml 的交互逻辑
	/// </summary>
	public partial class FansTaskInfoWnd : UserControl
	{
		public FansTaskInfoWnd()
		{
			InitializeComponent();
		}

		private void IntimacyUpBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.IntimacyViewModule.Visibility = Visibility.Visible;
			}
			catch { }
		}
	}
}
