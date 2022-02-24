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
using System.Windows.Threading;

namespace dropAnimation
{
	/// <summary>
	/// Drop.xaml 的交互逻辑
	/// </summary>
	public partial class Drop : UserControl
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		public Drop()
		{
			InitializeComponent();
			dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100); ;
			dispatcherTimer.Tick += DispatcherTimer_Tick;
			dispatcherTimer.Start();
		}

		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			this.InvalidateVisual();
		}

		protected override void OnRender(DrawingContext dc)
		{

			//var currentcolor = Colors.White;
			//Brush brush = new RadialGradientBrush(currentcolor,
			//		Color.FromArgb(0, currentcolor.R, currentcolor.G, currentcolor.B));
			//Random r = new Random();

			//for (int i = 0; i < 100; i++)
			//{
			//	var w = 35 * r.NextDouble();
			//	var rect =
			//	new RectangleGeometry(
			//		new Rect(new Point(r.Next(10, (int)this.Width), r.Next(10, (int)this.Height)),
			//			new Size(w, w)));
			//	drawingContext.DrawGeometry(brush, null, rect);
			//}

			{
				string filePath = @"pack://application:,,,/dropAnimation;Component/res/bk.png";
				var bmp = new BitmapImage(new Uri(filePath));
				if (bmp != null)
				{
					Rect SuffixRect = new Rect(0, 0, 26, 26);
					dc.DrawImage(bmp, SuffixRect);
				}
			}
		}
	}
}
