using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace ScrollViewer.ViewModel
{
	public partial class PlayerVM : ViewModelBase
	{
		private static Lazy<PlayerVM> lazyVM = new Lazy<PlayerVM>(() => new PlayerVM());
		public static PlayerVM Instance => lazyVM.Value;
		// 推荐视频列表
		private ObservableCollection<MainViewModel> _vRecommendVideos = new ObservableCollection<MainViewModel>();
		public ObservableCollection<MainViewModel> vRecommendVideos
		{
			get { return _vRecommendVideos; }
			set 
			{
				_vRecommendVideos = value;
				RaisePropertyChanged();
			}
		}

		public void init()
		{
			ObservableCollection<MainViewModel> items = new ObservableCollection<MainViewModel>();
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三修罗神唐三",
				sVideoName = "斗罗大陆绝世唐门龙王传说终极斗罗重生唐三",
				lPlayCount = 10000,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "久爱-预见【吕德的胜多负少",
				sVideoName = "加强的边惩亚瑟-下赛季T0 的存在金牌讲师",
				lPlayCount = 13456378,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 2340000,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 13450000,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 10500,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 10000,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 10000,
			});
			items.Add(new MainViewModel()
			{
				sVideoDuration = "03:20",
				sNickname = "海神唐三",
				sVideoName = "斗罗大陆",
				lPlayCount = 10000,
			});

			vRecommendVideos = items;
		}
	}
}
