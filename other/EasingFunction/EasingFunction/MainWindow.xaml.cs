using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasingFunction
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//动画时长
		private int iDuration { get; set; } = 1;
		//缓动类型
		private LEaseMode easeMode { get; set; } = LEaseMode.EaseIn;
		private Storyboard sBoard = null;
		private string sType { get; set; } = "";

		public MainWindow()
		{
			InitializeComponent();

			this.Loaded += Window_Loaded;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ComboBox1.Items.Add("Back");
				ComboBox1.Items.Add("Bounce");
				ComboBox1.Items.Add("Circle");
				ComboBox1.Items.Add("Quadratic");
				ComboBox1.Items.Add("Cubic");
				ComboBox1.Items.Add("Quartic");
				ComboBox1.Items.Add("Quintic");
				ComboBox1.Items.Add("Elastic");
				ComboBox1.Items.Add("Exponential");
				ComboBox1.Items.Add("Sine");
				ComboBox1.SelectedIndex = 0;

				ComboBox2.Items.Add("EaseIn");
				ComboBox2.Items.Add("EaseOut");
				ComboBox2.Items.Add("EaseInOut");
				ComboBox2.SelectedIndex = 0;
			}
			catch { }
		}

		private void Btn_Clicked(object sender, RoutedEventArgs e)
		{
			try
			{
				#region 原版
				sBoard = new Storyboard();
				DoubleAnimationUsingKeyFrames ks = new DoubleAnimationUsingKeyFrames();

				EasingDoubleKeyFrame k1 = new EasingDoubleKeyFrame();
				k1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
				k1.Value = 0;

				EasingMode easeMode2 = EasingMode.EaseIn;
				if(ComboBox2.Text == "EaseOut")
					easeMode2 = EasingMode.EaseOut;
				else if (ComboBox2.Text == "EaseInOut")
					easeMode2 = EasingMode.EaseInOut;

				EasingDoubleKeyFrame k2 = new EasingDoubleKeyFrame();
				k2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(Convert.ToInt32(TimeTextBox.Text)));
				k2.Value = 400;
				switch (ComboBox1.Text)
				{
					case "Back":
						k2.EasingFunction = new BackEase() { EasingMode = easeMode2 };
						break;
					case "Bounce":
						k2.EasingFunction = new BounceEase() { EasingMode = easeMode2 };
						break;
					case "Circle":
						k2.EasingFunction = new CircleEase() { EasingMode = easeMode2 };
						break;
					case "Quadratic":
						k2.EasingFunction = new QuadraticEase() { EasingMode = easeMode2 };
						break;
					case "Cubic":
						k2.EasingFunction = new CubicEase() { EasingMode = easeMode2 };
						break;
					case "Quartic":
						k2.EasingFunction = new QuarticEase() { EasingMode = easeMode2 };
						break;
					case "Quintic":
						k2.EasingFunction = new QuinticEase() { EasingMode = easeMode2 };
						break;
					case "Elastic":
						k2.EasingFunction = new ElasticEase() { EasingMode = easeMode2 };
						break;
					case "Exponential":
						k2.EasingFunction = new ExponentialEase() { EasingMode = easeMode2 };
						break;
					case "Sine":
						k2.EasingFunction = new SineEase() { EasingMode = easeMode2 };
						break;

				}

				ks.KeyFrames.Add(k1);
				ks.KeyFrames.Add(k2);
				sBoard.Children.Add(ks);
				Storyboard.SetTarget(ks, Img1);
				Storyboard.SetTargetProperty(ks, new PropertyPath("(Canvas.Left)"));

				sBoard.Completed += sBoard_Completed;
				sBoard.Begin(Img1, true);
				#endregion

				#region 公式
				easeMode = LEaseMode.EaseIn;
				if (ComboBox2.Text == "EaseOut")
					easeMode = LEaseMode.EaseOut;
				else if (ComboBox2.Text == "EaseInOut")
					easeMode = LEaseMode.EaseInOut;
				
				iDuration = Convert.ToInt32(Convert.ToInt32(TimeTextBox.Text) * 1000);
				sType = ComboBox1.Text;
				var v = Img2.GetValue(Canvas.LeftProperty);
				new Thread(EaseAnimateHandle).Start();
				#endregion

			}
			catch { }
		}

		private void sBoard_Completed(object sender, EventArgs e)
		{
			try
			{
				Thread.Sleep(1000);
				sBoard.Remove(Img1);
				Dispatcher.BeginInvoke(new Action<Image, double>(LeftHandle), Img1, 0d);
			}
			catch { }
		}

		private void LeftHandle(Image img, double d)
		{
			try
			{
				img.SetValue(Canvas.LeftProperty, d);
			}
			catch { }
		}

		private void EaseAnimateHandle()
		{
			try
			{
				int iInterval = 10;

				for (int i = 0; i <= iDuration; i += iInterval)
				{
					double d0 = 0;
					switch (sType)
					{
						case "Back":
							d0 = AnimateBase.Back(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Bounce":
							d0 = AnimateBase.Bounce(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Circle":
							d0 = AnimateBase.Circle(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Quadratic":
							d0 = AnimateBase.Quadratic(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Cubic":
							d0 = AnimateBase.Cubic(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Quartic":
							d0 = AnimateBase.Quartic(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Quintic":
							d0 = AnimateBase.Quintic(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Elastic":
							d0 = AnimateBase.Elastic(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Exponential":
							d0 = AnimateBase.Exponential(Convert.ToDouble(i) / iDuration, easeMode);
							break;
						case "Sine":
							d0 = AnimateBase.Sine(Convert.ToDouble(i) / iDuration, easeMode);
							break;

					}

					double m = d0 * 400;

					Dispatcher.BeginInvoke(new Action<Image, double>(LeftHandle), Img2, m);

					Thread.Sleep(iInterval);
				}

				Thread.Sleep(1000);
				Dispatcher.BeginInvoke(new Action<Image, double>(LeftHandle), Img2, 0d);
			}
			catch { }
		}
	}
}
