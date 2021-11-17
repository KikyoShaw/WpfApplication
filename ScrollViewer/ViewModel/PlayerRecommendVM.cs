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
			set { Set("vRecommendVideos", ref _vRecommendVideos, value); }
		}
	}
}
