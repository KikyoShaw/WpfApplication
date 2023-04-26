using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace dropAnimation
{

	public class DropItem1
	{
		//x轴位置
		public double DPosX { get; set; } = 0;

		//y轴位置
		public double DPosY { get; set; } = 0;

		//速度
		public double DSpeed { get; set; } = 1;
	}

	public class EasterEggEffectBase : Control
	{
		//元素组数
		private const int DropGroupCount = 2;

		//每个通道间隔
		private const int PassageSpace = 120;

		//随机位置
		public Random RItemRnd = new Random();

		//元素合集
		private readonly Dictionary<int, DropItem1> _vDropItem = new Dictionary<int, DropItem1>();

		BitmapImage bmp = null;

		private readonly System.Timers.Timer _animationTimer = null;

		public EasterEggEffectBase()
		{
			_animationTimer = new System.Timers.Timer();
			_animationTimer.Interval = 30;
			_animationTimer.Elapsed += DispatcherTimer_Tick;
		}

		public void ClearData()
		{
			try
			{
				_animationTimer.Stop();
				lock (_vDropItem)
				{
					_vDropItem.Clear();
					this.InvalidateVisual();
				}
			}
			catch { }
		}

		//根据屏幕尺寸初始化元素个数
		public void InitDropItem()
		{
			try
			{
				if (_vDropItem == null)
					return;

				//每次都清除一次数据
				ClearData();
				//图片资源
                string filePath = @"pack://application:,,,/dropAnimation;Component/res/bk.png";
                bmp = new BitmapImage(new Uri(filePath));

				lock (_vDropItem)
				{
					int count = (int)ActualWidth / PassageSpace;
					int temp = 0;
					for (int i = 0; i < count; i++)
					{
						for (int j = 0; j < DropGroupCount; j++)
						{
							var item = new DropItem1
							{
                                DPosX = RItemRnd.Next((int)ActualWidth),
                                DPosY = -(RItemRnd.Next(500)),
                                DSpeed = 2.5 + RItemRnd.Next(20) / 10.0 + ActualHeight / 700.0
                            };

                            if (!_vDropItem.ContainsKey(temp))
								_vDropItem.Add(temp, item);
							temp++;
						}
					}
					_animationTimer.Start();
				}
			}
			catch { }
		}

		//获取图片
		private bool GetImageFromFile(string sFilePath)
		{
			try
			{
				if (File.Exists(sFilePath))
				{
					BitmapImage bi = new BitmapImage();
					bi.BeginInit();
					bi.UriSource = new Uri(sFilePath);
					bi.CacheOption = BitmapCacheOption.OnLoad;
					bi.EndInit();
					bi.Freeze();
                    bmp = bi;
					return true;
				}
			}
			catch { }
			bmp = null;
			return false;
		}

		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (_vDropItem.Count <= 0)
				{
					_animationTimer.Stop();
					return;
				}


                Application.Current.Dispatcher.InvokeAsync(() =>
				{
					try
					{
                        //遍历
                        foreach (var key in _vDropItem.Keys)
                        {
                            var item = _vDropItem[key];
                            item.DPosY += item.DSpeed;
                            if (item.DPosY >= ActualHeight)
                            {
                                _vDropItem.Remove(key);
                                break;
                            }
                        }
						this.InvalidateVisual();
					}
					catch { }
				});
			}
			catch { }
		}

		protected override void OnRender(DrawingContext dc)
		{
			try
			{
				base.OnRender(dc);

				if (bmp == null)
					return;

				lock (_vDropItem)
                {
                    foreach (var key in _vDropItem.Keys)
                    {
                        var item = _vDropItem[key];
                        Rect rect = new Rect(item.DPosX, item.DPosY + bmp.PixelHeight, bmp.PixelWidth, bmp.PixelHeight);
                        Rect tRect = new Rect(0, 0, ActualWidth, ActualHeight + bmp.PixelHeight * 2);
                        if (tRect.Contains(rect))
                            DrawDropImg(dc, item.DPosX, item.DPosY);
                    }
                }
					
			}
			catch { }
		}

		private void DrawDropImg(DrawingContext dc, double dPosX, double dPosY)
		{
			try
			{
				if (bmp != null)
				{
					Rect suffixRect = new Rect(dPosX, dPosY, bmp.PixelWidth, bmp.PixelHeight);
					dc.DrawImage(bmp, suffixRect);
				}
			}
			catch { }
		}

		private static double Sine(double x)
		{
			return -(Math.Cos(Math.PI * x) - 1) / 2;
		}
	}
}
