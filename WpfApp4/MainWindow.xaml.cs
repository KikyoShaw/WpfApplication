using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool result = false;

        private void Clear()
        {
            Storyboard?.Stop();
            Storyboard?.Children.Clear();
        }

        private bool aaa = true;
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            // XGrid.Visibility = !result ? Visibility.Collapsed : Visibility.Visible;
            Clear();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Clear();

            if (result)
            {
                var doubleAnimation = new DoubleAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    From = 0,
                    To = 180,
                };

                Storyboard.SetTargetName(doubleAnimation, XGrid.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Width"));

                Storyboard.Children.Add(doubleAnimation);
                Storyboard.Begin(this);
            }
            else
            {
                var doubleAnimation = new DoubleAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    From = 180,
                    To = 0,
                };

                Storyboard.SetTargetName(doubleAnimation, XGrid.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Width"));

                Storyboard.Children.Add(doubleAnimation);
                Storyboard.Begin(this);
            }

            result = !result;
            //if (!result)
            //{
            //    //var story = (Storyboard)this.Resources["HideWindow"];
            //    //if (story != null)
            //    //{
            //    //    story.Completed += delegate { XGrid.Visibility = Visibility.Collapsed; };
            //    //    story.Begin(this.XGrid);
            //    //}
            //    XGrid.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    //var story = (Storyboard)this.Resources["ShowWindow"];
            //    //if (story != null)
            //    //{
            //    //    story.Completed += delegate { XGrid.Visibility = Visibility.Visible; };
            //    //    story.Begin(this.XGrid);
            //    //}
            //    XGrid.Visibility = Visibility.Visible;
            //}


        }


        private async Task Main(string[] args)
        {
            const string url = "https://example.com/file.zip";
            const string destinationPath = "file.zip";
            long totalSize;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
                HttpResponseMessage response = await httpClient.SendAsync(request);
                totalSize = response.Content.Headers.ContentRange.Length.GetValueOrDefault();
            }

            long localFileSize = 0;
            if (File.Exists(destinationPath))
                localFileSize = new FileInfo(destinationPath).Length;
            
            using (FileStream fileStream = new FileStream(destinationPath, FileMode.Append, FileAccess.Write, FileShare.None))
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(localFileSize, null);
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    DateTime progressReportTime = DateTime.Now;

                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        localFileSize += bytesRead;

                        if ((DateTime.Now - progressReportTime).TotalSeconds >= 1)
                        {
                            Console.WriteLine($"下载进度: {(double)localFileSize / totalSize:F3}");
                            progressReportTime = DateTime.Now;
                        }
                    }
                }
            }

            Console.WriteLine("下载完成");
        }
    }
}
