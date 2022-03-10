using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
	/// 
	public partial class FansTaskInfoWnd : UserControl
	{

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out POINT lpPoint);

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;
			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}
		}

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

		private void Btn_Down(object sender, MouseButtonEventArgs e)
		{
			try
			{
				POINT mousestart = new POINT();
				GetCursorPos(out mousestart);

				Point p2 = Mouse.GetPosition(this as FrameworkElement);

				Point p = Mouse.GetPosition(e.Source as FrameworkElement);

				Point p3 = Mouse.GetPosition(sender as FrameworkElement);

				Point p4 = PointToScreen(p3);

				//Window window = Window.GetWindow(this.btn);

				//Point point = this.btn.TransformToAncestor(this).Transform(new Point(0, 0));

				Point p5 = this.btn.PointToScreen(new Point(0, 0));

				System.Diagnostics.Trace.WriteLine($"mousestart.x={mousestart.X}, mousestart.y={mousestart.Y}, p3={p3}, p4={p4},p5={p5}");
			}
			catch { }
		}
	}
}
