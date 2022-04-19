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

namespace TestApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 

	public class ItemInfo
	{
		public string Name { get; set; } = "";
	}
	public partial class MainWindow : Window
	{
		private static Lazy<MainWindow> lazyInstance = new Lazy<MainWindow>(() => new MainWindow());
		public static MainWindow Instance => lazyInstance.Value;

		public Dictionary<long, ItemInfo> infos { get; set;} = new Dictionary<long, ItemInfo>();
		public MainWindow()
		{
			InitializeComponent();

			//List<string> list = new List<string>();
			//list.Add("111");
			//if(!list.Contains("111"))
			//	list.Add("111");
			//list.Add("111");

			ItemInfo _info = new ItemInfo();
			_info.Name = "111";
			infos.Add(0, _info);
			ItemInfo _info1 = new ItemInfo();
			_info1.Name = "444";
			if (infos.ContainsKey(0))
			{
				var item = infos[0];
				item = _info1;
				//item.Name = "222";
			}


		}

		private void btn_clicked(object sender, RoutedEventArgs e)
		{
			try
			{
				Point pStartPos = this.PointFromScreen(BtnGird.PointToScreen(new Point(0, 0)));
				Point pEndPos = this.PointFromScreen(Grids.PointToScreen(new Point(0, 0)));
				ttt.StartT1(pStartPos, pEndPos);
			}
			catch { }
		}
	}
}
