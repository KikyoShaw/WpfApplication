using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Text
{
	/// <summary>
	/// TextControl.xaml 的交互逻辑
	/// </summary>
	public partial class TextControl : UserControl
	{
		public TextControl()
		{
			InitializeComponent();
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			var str = "44";

			var formattedText = new FormattedText(str, CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				new Typeface
				(
					new FontFamily("微软雅黑"),
					FontStyles.Normal,
					FontWeights.Bold,
					FontStretches.Normal
				),
				30,
				Brushes.Black, 1);

			var geometry = formattedText.BuildGeometry(new Point(10, 10));

			drawingContext.DrawGeometry
			(
				new SolidColorBrush(Colors.White),
				new Pen(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D30C00")), 2),
				geometry
			);

			base.OnRender(drawingContext);
		}


	}
}
