using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TestApp
{

	public class AnaimationImg
	{
		public int iSpeed { get; set; } = 0;

		public double dt { get; set; } = 0.0;
		public BitmapImage bmp { get; set; } = null;

		public Point pMovement { get; set; } = new Point(0, 0);

		public Point pStartPoint { get; set; } = new Point(0, 0);

		public Point pEndPoint { get; set; } = new Point(0, 0);

	}

	public partial class ACIntimacyEffect1 : UserControl
	{
		private DispatcherTimer animationTimer = null;

		private Dictionary<int, AnaimationImg> anaimationImgs = new Dictionary<int, AnaimationImg>();

		private int index = 0;

		//随机位置
		public Random rItemRnd = new Random();

		public Point pStartPoint { get; set; } = new Point(0, 0);

		public Point pEndPoint { get; set; } = new Point(0, 0);

		public ACIntimacyEffect1()
		{
			InitializeComponent();
			animationTimer = new DispatcherTimer();
			animationTimer.Interval = TimeSpan.FromMilliseconds(10);
			animationTimer.Tick += new EventHandler(DispatcherTimer_Tick);
			this.Loaded += ACIntimacyEffect1_Loaded;
		}

		private void ACIntimacyEffect1_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				//pStartPoint = this.BtnGird.PointToScreen(new Point(0, 0));
				//pEndPoint = this.Grids.PointToScreen(new Point(0, 0));
			}
			catch { }
		}

		public void StartT()
		{
			if (!animationTimer.IsEnabled)
			{
				for (int i = 0; i < 9; i++)
				{
					var info = new AnaimationImg();
					if (i % 2 == 0)
					{
						string filePath = @"pack://application:,,,/TestApp;Component/Resources/th-c4.png";
						info.bmp = new BitmapImage(new Uri(filePath));
					}
					else
					{
						string filePath = @"pack://application:,,,/TestApp;Component/Resources/th-c6.png";
						info.bmp = new BitmapImage(new Uri(filePath));
					}

					info.pStartPoint = this.PointFromScreen(this.PointToScreen(new Point(0, 0)));
					info.pEndPoint = this.PointFromScreen(this.PointToScreen(new Point(0, 0)));
					info.iSpeed = rItemRnd.Next(60, 90);
					int _randdt = rItemRnd.Next(6, 8);
					info.dt = _randdt * 0.1;
					anaimationImgs.Add(i, info);
				}
				animationTimer.Start();
			}
		}

		public void StartT1(Point pStartPos, Point pEndPos)
		{
			if (!animationTimer.IsEnabled)
			{
				for (int i = 0; i < 9; i++)
				{
					var info = new AnaimationImg();
					if (i % 2 == 0)
					{
						string filePath = @"pack://application:,,,/TestApp;Component/Resources/th-c4.png";
						info.bmp = new BitmapImage(new Uri(filePath));
					}
					else
					{
						string filePath = @"pack://application:,,,/TestApp;Component/Resources/th-c6.png";
						info.bmp = new BitmapImage(new Uri(filePath));
					}

					info.pStartPoint = pStartPos;
					info.pEndPoint = pEndPos;
					info.iSpeed = rItemRnd.Next(60, 90);
					int _randdt = rItemRnd.Next(6, 8);
					info.dt = _randdt * 0.1;
					anaimationImgs.Add(i, info);
				}
				animationTimer.Start();
			}
		}

		private int iKeyIndex = 0;
		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (anaimationImgs.Count <= 0)
				{
					index = 0;
					iKeyIndex = 0;
					animationTimer.Stop();
					this.InvalidateVisual();
					return;
				}

				foreach (var key in anaimationImgs.Keys)
				{
					iKeyIndex++;
					var item = anaimationImgs[key];
					double t = (double)index / item.iSpeed;
					item.pMovement = GetBezierPoint(item.pStartPoint, item.pEndPoint, t, item.dt);

					if (item.pMovement.X <= item.pEndPoint.X && item.pMovement.Y <= item.pEndPoint.Y)
					{
						System.Diagnostics.Trace.WriteLine($"结束：item.pMovement.X={item.pMovement.X}, item.pMovement.Y={item.pMovement.Y}, item.pEndPoint.X={item.pEndPoint.X}, pitem.pEndPoint.Y={item.pEndPoint.Y}");
						if (anaimationImgs.ContainsKey(key))
							anaimationImgs.Remove(key);
					}
					else
					{
						System.Diagnostics.Trace.WriteLine($"item.pMovement.X={item.pMovement.X}, item.pMovement.Y={item.pMovement.Y}, item.pEndPoint.X={item.pEndPoint.X}, pitem.pEndPoint.Y={item.pEndPoint.Y}");
						this.InvalidateVisual();
					}
				}
				index++;
			}
			catch { }
		}

		private static Point GetBezierPointByT(Point begin, Point handle, Point end, double t)
		{
			var pow = Math.Pow(1 - t, 2);

			// B = (1-t)^2*P0 + 2t(1-t)P1 + t^2*P2 , t∈[0,1]
			var x = pow * begin.X + 2 * t * (1 - t) * handle.X + t * t * end.X;
			var y = pow * begin.Y + 2 * t * (1 - t) * handle.Y + t * t * end.Y;

			return new Point((int)x, (int)y);
		}

		private static Point GetBezierPoint(Point p1, Point p2, double t, double dt)
		{
			double tp = 1.0 - t;
			int x = 0;
			int y = 0;

			//Point c1 = new Point(p1.X, p1.Y + 20 * i);
			//Point c2 = new Point(p2.X + 20 * i, p2.Y);

			//if (i % 3 == 1)
			//{
			//	c1 = new Point(p1.Y + 20 * i, p1.X * 0.7);
			//	c2 = new Point(p2.Y * 0.7, p2.X + 20 * i);
			//}
			//else if (i % 3 == 2)
			//{
			//	c1 = new Point(p1.X * 0.7, p1.Y + 20 * i);
			//	c2 = new Point(p2.X + 20 * i, p2.Y * 0.7);
			//}

			Point c1 = new Point(p1.X * dt, p1.Y);
			Point c2 = new Point(p2.X, p2.Y * dt);

			//Point c1 = new Point(p1.X * 0.7, p1.Y);
			//Point c2 = new Point(p2.X, p2.Y * 0.7);


			x = (int)(p1.X * tp * tp * tp + 3 * c1.X * tp * tp * t +
				3 * c2.X * t * t * tp + p2.X * t * t * t);
			y = (int)(p1.Y * tp * tp * tp + 3 * c1.Y * tp * tp * t +
				3 * c2.Y * t * t * tp + p2.Y * t * t * t);

			return new Point((int)x, (int)y);
		}

		private static Point GetBezierPoint1(Point p1, Point p2, double t)
		{
			double tp = 1.0 - t;
			int x = 0;
			int y = 0;
			Point c1 = new Point(p1.X, p1.Y);
			Point c2 = new Point(p2.X, p2.Y);

			x = (int)(p1.X * tp * tp * tp + 3 * c1.X * tp * tp * t +
				3 * c2.X * t * t * tp + p2.X * t * t * t);
			y = (int)(p1.Y * tp * tp * tp + 3 * c1.Y * tp * tp * t +
				3 * c2.Y * t * t * tp + p2.Y * t * t * t);

			return new Point((int)x, (int)y);
		}

		private static Point GetBezierPoint2(Point p1, Point p2, double t)
		{
			double tp = 1.0 - t;
			int x = 0;
			int y = 0;
			Point c1 = new Point(p1.Y, p1.X * 0.7);
			Point c2 = new Point(p2.Y * 0.7, p2.X);

			x = (int)(p1.X * tp * tp * tp + 3 * c1.X * tp * tp * t +
				3 * c2.X * t * t * tp + p2.X * t * t * t);
			y = (int)(p1.Y * tp * tp * tp + 3 * c1.Y * tp * tp * t +
				3 * c2.Y * t * t * tp + p2.Y * t * t * t);

			return new Point((int)x, (int)y);
		}

		private void GetThreePointInPath(double NoticeX, double NoticeY, out double stX, out double stY, out double midX, out double midY, out double endX, out double endY)
		{
			Point ToolWndPoint = PointToScreen(new Point(0, 0));

			//如果点不在客户端窗口范围内，则选择飞出点为视频区域的右侧下侧
			if (NoticeX < ToolWndPoint.X || NoticeX > ToolWndPoint.X + this.ActualWidth)
			{
				NoticeX = (ToolWndPoint.X + ActualWidth * 0.8);
			}
			stX = NoticeX - ToolWndPoint.X;
			stY = NoticeY - ToolWndPoint.Y;

			endX = this.ActualWidth / 2;
			//动态终点，起点到透明层顶部的距离的二分之一
			endY = (NoticeY - ToolWndPoint.Y) / 2;

			//算等腰三角坐标形成弧度抛物
			double dtXYlen = Math.Sqrt((endY - stY) * (endY - stY) + (endX - stX) * (endX - stX));
			double angle = Math.Atan(Math.Abs(endY - stY) / Math.Abs(endX - stX));
			double tlen = dtXYlen * 0.3 / Math.Tan(angle);
			double rlen = dtXYlen / 2 - tlen;
			double detaX = rlen * Math.Sin(angle) + tlen / Math.Cos(angle);
			double detaY = rlen * Math.Cos(angle);

			midX = endX;
			midY = endY;

			if (stX < endX)
			{
				midX -= detaX;
				midY += detaY;
			}
			else
			{
				midX += detaX;
				midY += detaY;
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);

			try
			{
				//Rect SuffixRect = new Rect(pPoint.X, pPoint.Y, 30, 30);
				//dc.DrawImage(bmp, SuffixRect);

				//Rect SuffixRect1 = new Rect(pPoint1.X, pPoint1.Y, 30, 30);
				//dc.DrawImage(bmp1, SuffixRect1);

				//Rect SuffixRect2 = new Rect(pPoint2.X, pPoint2.Y, 30, 30);
				//dc.DrawImage(bmp2, SuffixRect2);

				foreach (var key in anaimationImgs.Keys)
				{
					var item = anaimationImgs[key];
					if (item.bmp != null)
					{
						Rect SuffixRect = new Rect(item.pMovement.X, item.pMovement.Y, 30, 30);
						dc.DrawImage(item.bmp, SuffixRect);
					}
				}

				Point curPoint = new Point(0, 0);
				Color brushColor = Color.FromArgb(128, 0, 0, 255);
				SolidColorBrush colorBrush = new SolidColorBrush(brushColor);
				Pen pen = new Pen(colorBrush, 1.0);
				for (int i = 0; i < 100; i++)
				{
					double t = (double)i / 100;
					double tp = 1.0 - t;
					int x = 0;
					int y = 0;
					Point p1 = new Point(50, 50);
					Point p2 = new Point(400, 400);

					Point c1 = new Point(100, 150);
					Point c2 = new Point(250, 300);

					x = (int)(p1.X * tp * tp * tp + 3 * c1.X * tp * tp * t +
						3 * c2.X * t * t * tp + p2.X * t * t * t);
					y = (int)(p1.Y * tp * tp * tp + 3 * c1.Y * tp * tp * t +
						3 * c2.Y * t * t * tp + p2.Y * t * t * t);

					curPoint.X = x;
					curPoint.Y = y;
					dc.DrawEllipse(colorBrush, pen, curPoint, 1, 1);
				}


			}
			catch { }
		}

		private void btn_clicked(object sender, RoutedEventArgs e)
		{
			StartT();
		}
	}
}
