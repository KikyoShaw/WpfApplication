using GalaSoft.MvvmLight;

namespace ScrollViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		// ��ƵID
		public long lVid { get; set; } = -1;

		// ��Ƶ����
		private string _sVideoCover = "";
		public string sVideoCover
		{
			get { return _sVideoCover; }
			set { Set("sVideoCover", ref _sVideoCover, value); }
		}

		// ��Ƶʱ��
		private string _sVideoDuration = "";
		public string sVideoDuration
		{
			get { return _sVideoDuration; }
			set { Set("sVideoDuration", ref _sVideoDuration, value); }
		}

		// ��������
		private string _sNickname = "";
		public string sNickname
		{
			get { return _sNickname; }
			set { Set("sNickname", ref _sNickname, value); }
		}

		// ��Ƶ����
		private string _sVideoName = "";
		public string sVideoName
		{
			get { return _sVideoName; }
			set { Set("sVideoName", ref _sVideoName, value); }
		}

		// ���Ŵ���
		private long _lPlayCount = 0;
		public long lPlayCount
		{
			get { return _lPlayCount; }
			set { Set("lPlayCount", ref _lPlayCount, value); }
		}
	}
}