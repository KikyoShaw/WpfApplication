using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TestApp
{
	public class FansVM: ViewModelBase
	{
		private static readonly Lazy<FansVM> lazy = new Lazy<FansVM>(() => new FansVM());
		public static FansVM Instance => lazy.Value;

        // 未登录时，码率只可选择小于此值
        public static int iMaxRateWithoutLogin = 4000;

        private BitmapImage _bAnimation = null;
		public BitmapImage bAnimation
		{
			get { return _bAnimation; }
			set { Set("bAnimation", ref _bAnimation, value); }
		}

		private bool _bAnimationShow = false;
		public bool bAnimationShow
		{
			get { return _bAnimationShow; }
			set { Set("bAnimationShow", ref _bAnimationShow, value); }
		}

		public DispatcherTimer showAnimationTimer { get; set; } = null;
		private int iCurHoverAnimationShowFrameIndex = 0;
		static int kHoverAnimationFrameCount = 72;
		static List<BitmapImage> vHotIconHoverImages = new List<BitmapImage>();
		public FansVM()
		{
			try
			{
				for (var i = 1; i <= kHoverAnimationFrameCount; i++)
				{

					var filePath = @"pack://application:,,,/TestApp;Component/Resources/hot_hover/" + i.ToString() + @".png";
					var image = new BitmapImage(new Uri(filePath));
					vHotIconHoverImages.Add(image);
				}
				if (showAnimationTimer == null)
				{
					showAnimationTimer = new DispatcherTimer();
					showAnimationTimer.Tick += showAnimationTimer_Tick;
					showAnimationTimer.Interval = TimeSpan.FromMilliseconds(30);
				}
			}
			catch { }
		}

		private void showAnimationTimer_Tick(object? sender, EventArgs e)
		{
			try
			{
				bAnimation = vHotIconHoverImages[iCurHoverAnimationShowFrameIndex];
				//if (iCurHoverAnimationShowFrameIndex == (kHoverAnimationFrameCount-1))
				//	showAnimationTimer.Stop();
				//else
					iCurHoverAnimationShowFrameIndex = (iCurHoverAnimationShowFrameIndex + 1) % kHoverAnimationFrameCount;
				//if (iCurHoverAnimationShowFrameIndex == 0)
				//{
				//	showAnimationTimer?.Stop();
				//}
			}
			catch { }
		}
	}
}
