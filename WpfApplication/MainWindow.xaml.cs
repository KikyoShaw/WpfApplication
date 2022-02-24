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

namespace WpfApplication
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			initText();

		}

		private void initText()
		{
			this.textTips.Inlines.Clear();
			this.textTips.Inlines.Add(FormatText("恭喜你获得 iPhone13 pro max 一台"));
			
		}

		private void Btn_Click(object sender, RoutedEventArgs e)
		{
			testPopup.IsOpen = true;
		}

		static Inline FormatText(string text)
		{
			Color backColor1 = (Color)ColorConverter.ConvertFromString("#FF00FF");
			Color backColor2 = (Color)ColorConverter.ConvertFromString("#DC143C");
			Dictionary<string, Func<string, Run>> keywords = new Dictionary<string, Func<string, Run>>()
		{
			{"恭喜你获得", s => new Run(s){Foreground = new SolidColorBrush(backColor1)}},
			{" iPhone13 pro max ", s => new Run(s){Foreground = new SolidColorBrush(backColor2)}},
			{"一台", s => new Run(s){Foreground = new SolidColorBrush(backColor1)}},
		};
			Span span = new Span();
			int startIndex = 0;
			while (true)
			{
				var hit = keywords.Keys.Select(k => new { word = k, index = text.IndexOf(k, startIndex) }).OrderBy(x => (uint)x.index).FirstOrDefault();
				if (hit.index < 0)
				{
					span.Inlines.Add(new Run(text.Substring(startIndex)));
					break;
				}
				span.Inlines.Add(new Run(text.Substring(startIndex, hit.index - startIndex)));
				span.Inlines.Add(keywords[hit.word](hit.word));
				startIndex = hit.index + hit.word.Length;
			}
			return span;
		}

		private void toastBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
