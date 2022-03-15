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

namespace WpfApp1
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp1"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp1;assembly=WpfApp1"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:flurCtrl/>
    ///
    /// </summary>
    public class flurCtrl : Control
    {
        private static List<BitmapImage> g_images = new List<BitmapImage>();
        private Storyboard storyboard = new Storyboard();

        static flurCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(flurCtrl), new FrameworkPropertyMetadata(typeof(flurCtrl)));

            for (int i = 0; i <= 11; i++)
            {
                string ss = string.Format("pack://application:,,,/WpfApp1;Component/Resources/{0:D2}.png", i);
                BitmapImage image = new BitmapImage(new Uri(ss));

                g_images.Add(image);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Image img = GetTemplateChild("iconImage") as Image;

            var keyFrames = CreateAnimation(img);

            storyboard.Children.Add(keyFrames);

            var moveFrame = CreateTranslateAnim();

            storyboard.Children.Add(moveFrame);


            //storyboard.Children.Add(  )

            storyboard.Begin();
        }

        private ObjectAnimationUsingKeyFrames CreateAnimation(Image img)
        {
            ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();
            frames.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetProperty(frames, new PropertyPath("Source"));
            Storyboard.SetTarget(frames, img);

            int idx = 0;
            foreach (var bmp in g_images)
            {
                DiscreteObjectKeyFrame frame = new DiscreteObjectKeyFrame();
                frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(idx * 100));
                frame.Value = bmp;
                frames.KeyFrames.Add(frame);
                idx += 1;
            }

            return frames;
        }

        private DoubleAnimation CreateTranslateAnim()
        {
            DoubleAnimation TransAnim = new DoubleAnimation();
            TransAnim.From = 0;
            TransAnim.To = 500;
            TransAnim.Completed += TransAnim_Completed;
            Random rd = new Random();

            TransAnim.Duration = new Duration(TimeSpan.FromSeconds(rd.Next() % 8 + 2));
            TransAnim.RepeatBehavior = new RepeatBehavior(1);

            Storyboard.SetTarget(TransAnim, this); 
            Storyboard.SetTargetProperty(TransAnim,new PropertyPath(Canvas.TopProperty));
            return TransAnim;
        }

        private void TransAnim_Completed(object sender, EventArgs e)
        {
            DoubleAnimation dd = sender as DoubleAnimation;
        }
    }
}
