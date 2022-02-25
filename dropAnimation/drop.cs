using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace dropAnimation
{

	public class DropItem
	{
		//x轴位置
		public double iPosX { get; set; } = 0;

		//y轴位置
		public double iPosY { get; set; } = 0;

		//速度
		public double iSpeed { get; set; } = 1;
	}

	public class drop : Control
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();

		//刷新时间
		private const int iRefreshTime = 2;

		//元素组数
		private const int iDropGroupCount = 3;

		//每个通道间隔
		private const int iPassageSpace = 80;

		//随机位置
		public Random rItemRnd = new Random();

		//元素合集
		private Dictionary<int, DropItem> vDropItem = new Dictionary<int, DropItem>();

		BitmapImage bmp = null;

		//Rect tRect = new Rect(0, 0, 0, 0);

		private bool _start = false;


		public int curDelayTick { get; set; } = 0;


		public drop()
		{
			//CompositionTarget.Rendering += CompositionTarget_Rendering;
			//dispatcherTimer.Interval = TimeSpan.FromMilliseconds(iRefreshTime); 
			//dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
		}

		public void initRect()
		{
			//tRect = new Rect(0, -26, ActualWidth, ActualHeight + 26*2);
		}

		//根据屏幕尺寸初始化元素个数
		public void InitDropItem()
		{
			try
			{
				if (vDropItem == null)
					return;

				string filePath = @"pack://application:,,,/dropAnimation;Component/res/bk.png";
				bmp = new BitmapImage(new Uri(filePath));

				//if (ActualWidth < iPassageSpace) {
				//	return;
				//}

				Stop();
				vDropItem.Clear();

				int count = (int)ActualWidth / iPassageSpace;
				int temp = 0;
				for(int i = 0; i < count; i++)
				{
					for (int j = 0; j < iDropGroupCount; j++)
					{
						var item = new DropItem();
						item.iPosX = rItemRnd.Next((int)ActualWidth);//i * iPassageSpace;
						item.iPosY = -rItemRnd.Next(500);
						item.iSpeed = 2 + rItemRnd.Next(10)/10.0;
						if(!vDropItem.ContainsKey(temp))
							vDropItem.Add(temp, item);
						temp++;
					}
				}

				Start();
				//dispatcherTimer.Start();
			}
			catch { }
		}

		private void Start()
		{
			if (!_start)
			{
				_start = !_start;
				CompositionTarget.Rendering += CompositionTarget_Rendering;
			}
		}
		private void Stop()
		{
			if (_start)
			{
				_start = !_start;
				CompositionTarget.Rendering -= CompositionTarget_Rendering;
			}
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			try
			{
				if (vDropItem.Count <= 0)
				{
					//dispatcherTimer.Stop();
					Stop();
					return;
				}
				//遍历
				foreach (var key in vDropItem.Keys)
				{
					var item = vDropItem[key];
					item.iPosY += item.iSpeed;
					if (item.iPosY >= ActualHeight)
					{
						vDropItem.Remove(key);
						break;
					}
				}
				this.InvalidateVisual();
			}
			catch { }
		}

		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			if (vDropItem.Count <= 0)
			{
				dispatcherTimer.Stop();
				return;
			}
			//遍历
			foreach (var key in vDropItem.Keys)
			{
				var item = vDropItem[key];
				item.iPosY += item.iSpeed;
				if (item.iPosY >= ActualHeight)
				{
					vDropItem.Remove(key);
					break;
				}
			}
			this.InvalidateVisual();
		}

		protected override void OnRender(DrawingContext dc)
		{
			try
			{
				base.OnRender(dc);

				if (bmp == null)
					return;

				foreach (var key in vDropItem.Keys)
				{
					var item = vDropItem[key];
					Rect rect = new Rect(item.iPosX, item.iPosY + bmp.Height, bmp.Width, bmp.Height);
					Rect tRect = new Rect(0, 0, ActualWidth, ActualHeight + bmp.Height * 2);
					if (tRect.Contains(rect))
					{
						DrawDropImg(dc, item.iPosX, item.iPosY);
					}
				}
					
			}
			catch { }
		}

		private void DrawDropImg(DrawingContext dc, double iPosX, double iPosY)
		{
			try
			{
				if (bmp != null)
				{
					//double x = iPosY / (ActualHeight + 26 * 2);
					//double y = Sine(x);
					Rect SuffixRect = new Rect(iPosX, iPosY, bmp.Width, bmp.Height);
					dc.DrawImage(bmp, SuffixRect);
				}
			}
			catch { }
		}

		private static double Sine(double x)
		{
			return -(Math.Cos(Math.PI * x) - 1) / 2;
		}

		private void InitDrawDropImg()
		{
			try
			{

			}
			catch { }
		}
	}
}
