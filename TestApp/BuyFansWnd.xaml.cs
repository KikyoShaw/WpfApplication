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
	/// BuyFansWnd.xaml 的交互逻辑
	/// </summary>
	public partial class BuyFansWnd : UserControl
	{
		FansVM vm = null;
		public BuyFansWnd()
		{
			InitializeComponent();
			vm = FansVM.Instance;
			this.DataContext = vm;

			if(vm != null)
			{
				if(vm.showAnimationTimer != null)
					vm.showAnimationTimer.Start();
			}

		}
	}
}
