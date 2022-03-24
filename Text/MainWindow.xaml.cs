using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Text
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			//string ss = "true1";

			//bool state = false;

			//bool.TryParse(ss, out state);

		}


		private Mutex mutexClearEasterEggRes = null;
		private const string sEasterEggMutexName = "huyapc-clean-easteregg-res-mutex-name-{09C278EE-7933-4919-986E-7F1CDE84C489}";
		private void Btn_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				if (mutexClearEasterEggRes != null)
					return;

				bool bFirstCreate = false;
				mutexClearEasterEggRes = new Mutex(false, sEasterEggMutexName, out bFirstCreate);
				if (bFirstCreate == false)
					return;

				System.Threading.ThreadPool.QueueUserWorkItem((state) =>
				{
					try
					{
						string TempDir = System.IO.Path.GetTempPath();
						string sExpiredEasterEggDir = System.IO.Path.Combine(TempDir, @"huya_pc\Cache\ExpiredEasterEgg");
						if (Directory.Exists(sExpiredEasterEggDir) == false)
							Directory.CreateDirectory(sExpiredEasterEggDir);

						string sCachePath = System.IO.Path.Combine(TempDir, @"huya_pc\Cache\EasterEggCache");
						if (Directory.Exists(sCachePath))
						{
							DirectoryInfo dir = new DirectoryInfo(sCachePath);
							var vDirList = dir.GetDirectories();
							if ((vDirList != null) && (vDirList.Count() > 0))
							{
								foreach (var item in vDirList)
								{
									var names = item.Name;
									Directory.Move(item.FullName, System.IO.Path.Combine(sExpiredEasterEggDir, item.Name));
								}
							}
						}

						//删除资源
						if (Directory.Exists(sExpiredEasterEggDir))
						{
							DirectoryInfo dir = new DirectoryInfo(sExpiredEasterEggDir);
							var vDirList = dir.GetDirectories();
							var vFileList = dir.GetFiles();
							if (((vFileList != null) && (vFileList.Count() > 0)) ||
								((vDirList != null) && (vDirList.Count() > 0)))
							{
								Directory.Delete(sExpiredEasterEggDir, true);
							}
						}
					}
					catch { }

				}, null);
			}
			catch { }
		}
	}
}
