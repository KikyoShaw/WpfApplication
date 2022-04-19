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

namespace WindowChrome
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var JoinTime = TimeZoneInfo.ConvertTime(
					   new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1649407918),
					   TimeZoneInfo.Local);

			var times = DateTimeOffset.FromUnixTimeMilliseconds(1649407918).DateTime.ToLocalTime();


		}

		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			try
			{

			}
			catch { }
		}

		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (e.ButtonState == MouseButtonState.Pressed)
					this.DragMove();
			}
			catch { }
		}
	}
}
