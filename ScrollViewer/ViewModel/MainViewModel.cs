using GalaSoft.MvvmLight;

namespace ScrollViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		// 视频ID
		public long lVid { get; set; } = -1;

		// 视频封面
		private string _sVideoCover = "";
		public string sVideoCover
		{
			get { return _sVideoCover; }
			set { Set("sVideoCover", ref _sVideoCover, value); }
		}

		// 视频时长
		private string _sVideoDuration = "";
		public string sVideoDuration
		{
			get { return _sVideoDuration; }
			set { Set("sVideoDuration", ref _sVideoDuration, value); }
		}

		// 作者名字
		private string _sNickname = "";
		public string sNickname
		{
			get { return _sNickname; }
			set { Set("sNickname", ref _sNickname, value); }
		}

		// 视频标题
		private string _sVideoName = "";
		public string sVideoName
		{
			get { return _sVideoName; }
			set { Set("sVideoName", ref _sVideoName, value); }
		}

		// 播放次数
		private long _lPlayCount = 0;
		public long lPlayCount
		{
			get { return _lPlayCount; }
			set { Set("lPlayCount", ref _lPlayCount, value); }
		}
	}
}