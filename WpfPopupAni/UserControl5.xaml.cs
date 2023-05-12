using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfPopupAni
{
    /// <summary>
    /// UserControl5.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl5 : UserControl
    {
        private ObservableCollection<string> imageFiles;
        private DispatcherTimer timer;
        private int currentImageIndex;

        public UserControl5()
        {
            InitializeComponent();
            LoadImages();
            StartBannerScrolling();
        }

        private void LoadImages()
        {
            imageFiles ??= new ObservableCollection<string>();
            for (int i = 1; i < 11; i++)
            {
                imageFiles.Add($@"pack://application:,,,/WpfPopupAni;Component/Resources/tu{i}.jpg");
            }

            bannerListBox.ItemsSource = imageFiles;
            bannerListBox.SelectedIndex = 0;
        }

        private void StartBannerScrolling()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentImageIndex = (currentImageIndex + 1) % imageFiles.Count;
            bannerListBox.SelectedIndex = currentImageIndex;
            bannerListBox.ScrollIntoView(bannerListBox.SelectedItem);
        }
    }
}
