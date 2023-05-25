using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TestDictionary();
            //var slider = new Slider
            //{
            //    Width = 150,
            //    Height = 30,
            //    Opacity = 0.75,

            //    VerticalAlignment = VerticalAlignment.Center,
            //    Margin = new Thickness(0)
            //};

            //var adornerLayer = AdornerLayer.GetAdornerLayer(mainGrid);
            //adornerLayer?.Add(new SliderAdorner(mainGrid, slider));
        }

        private void TestDictionary()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                // 添加项到字典中
                { "item1", 1 },
                { "sitem3", 7 },
                { "item5", 3 },
                { "item4", 4 }
            };

            foreach (var item in dict)
            {
               dict.Remove(item.Key);
            }

            List<int> list = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10
            };

            for (int i = 0; i < list.Count; i++)
            {
                list.Remove(i);
            }

            foreach (var item in list)
            {
                if (item % 2 == 0)
                {
                    list.Remove(item);
                }
            }
        }
    }
}
