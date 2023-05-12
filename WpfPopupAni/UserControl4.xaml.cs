using System;
using System.Collections.Generic;
using System.Text;
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
using System.Windows.Threading;

namespace WpfPopupAni
{
    /// <summary>
    /// UserControl4.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl4 : UserControl
    {
        private int currentImageIndex;
        private DispatcherTimer timer;
        private List<string> imageFiles = null;

        public UserControl4()
        {
            InitializeComponent();

            imageFiles ??= new List<string>();

            for (int i = 1; i < 11; i++)
            {
                imageFiles.Add($@"pack://application:,,,/WpfPopupAni;Component/Resources/tu{i}.jpg");
            }
            
            StartCarousel();
        }

        private void StartCarousel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 4);
            timer.Tick += Timer_Tick;
            timer.Start();

            SetImageSource(currentImageIndex);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentImageIndex = (currentImageIndex + 1) % imageFiles.Count;
            SetImageSource(currentImageIndex);
        }

        private void SetImageSource(int index)
        {
            var path = imageFiles[index];
            carouselImage.Source = GetImageCacheFromApplication(path);
            ((Storyboard)carouselImage.Resources["SwitchImageStoryboard"]).Begin();
        }

        private static BitmapImage GetImageCacheFromApplication(string sUrl)
        {
            if (string.IsNullOrEmpty(sUrl))
                return null;

            if (!sUrl.Contains(@"pack://application"))
                return null;

            BitmapImage bmp = null;
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.UriSource = new Uri(sUrl);
            img.EndInit();
            img.Freeze();
            bmp = img;
            return bmp;

        }
    }
}
