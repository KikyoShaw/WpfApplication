using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTestApp
{
    /// <summary>
    /// UserControl7.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl7 : UserControl
    {
        public UserControl7()
        {
            InitializeComponent();

            // 在代码后端中获取 GridB 并为其 RenderTransform 属性创建 TranslateTransform 实例
            //TranslateTransform translateTransform = new TranslateTransform();
            //GridB.RenderTransform = translateTransform;

            //// 将 GridB 平移至 GridA 的显示范围之外
            //double offsetX = -GridB.ActualWidth; // 设置水平偏移量为 GridB 的宽度的负值
            //double offsetY = -GridB.ActualHeight; // 设置垂直偏移量为 GridB 的高度的负值
            //translateTransform.X = offsetX;
            //translateTransform.Y = offsetY;
        }
    }
}
