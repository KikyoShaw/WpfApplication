using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DragABall
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 成员变量
        /// <summary>
        /// 上次绘制时的速度
        /// </summary>
        private Vector v1 = new Vector(0, 0);
        /// <summary>
        /// 与上次绘制时间的时间间隔
        /// </summary>
        private TimeSpan prevTime = TimeSpan.Zero;
        /// <summary>
        /// 鼠标点击 相对于小球的位置
        /// </summary>
        private Point point = new Point(0, 0);
        /// <summary>
        /// 计时器
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();
        /// <summary>
        /// 绘制拉力线
        /// </summary>
        private Line line = new Line();
        /// <summary>
        /// 鼠标点相对于布局元素
        /// </summary>
        private Point pMouse = new Point(0, 0);
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //给对象加一个平移转换
            this.moveObj.RenderTransform = new TranslateTransform();
            //开始计时
            this.stopwatch.Start();
            //每一帧绘制
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            //线
            line.StrokeThickness = 2;
            line.Stroke = System.Windows.Media.Brushes.Red;
            layout.Children.Add(line);
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //鼠标相对于小方块的位置
            point = e.GetPosition(this.moveObj);
            //由于改相对位置是相对于小方块左上角的,将其纠正到相当于小球中心
            point.Offset(-this.moveObj.ActualWidth / 2, -this.moveObj.ActualHeight / 2);
            //鼠标点相对于布局元素
            pMouse = e.GetPosition(this.layout);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //粘滞阻力
            //在理论力学中所说的"与物体速度一次方成正比的阻力"指的就是粘滞阻力 在空气中运动速度不十分快的物体 受到的阻力主要是粘滞阻力
            //斯托克斯公式 球形物体 F=6πηvr=3πηvd η为流体的粘性系数 r为球形物体的半径 d为球形物体的直径
            //加速度a=(v2-v1)/t 速度差v2-v1=at
            //F=F拉-F阻=ma
            //速度差v2-v1=at=((F拉-F阻)/m)t=((F拉-3πηv1d)/m)t
            //球体质量=4/3 * π * r^3 * ρ=1/6* π * d^3 * ρ
            //s=vt+1/2at^2

            //时间 t
            TimeSpan currentTime = this.stopwatch.Elapsed;
            double t = (currentTime - this.prevTime).TotalSeconds;
            this.prevTime = currentTime;
            //使用鼠标相对于小方块的位置作为拉力 F拉
            Vector F拉 = new Vector(point.X, point.Y);
            //流体的粘性系数 η
            double eta = 100;
            if (!string.IsNullOrEmpty(this.tbx_Eta.Text))
                double.TryParse(this.tbx_Eta.Text, out eta);
            //直径 d
            double d = this.moveObj.Width;
            //小球密度 ρ
            double rho = 1;
            if (!string.IsNullOrEmpty(this.tbx_Rho.Text))
                double.TryParse(this.tbx_Rho.Text, out rho);
            //小球质量 m
            double m = (Math.PI * d * d * d * rho) / 6;
            //为了能拉动小球 自定义个拉力系数 拉小球需要的力和小球质量m成正比和小球直径d的三次方成正比
            double coefficient = d * d * d;
            F拉 = coefficient * F拉;
            //速度差v2-v1=((F拉-3πηv1d)/m)t
            Vector vDiff = ((F拉 - 3 * Math.PI * eta * v1 * d) / m) * t;
            //s=v1t+1/2at^2=v1t+(1/2)((v2-v1)/t)*t*t=v1t+(1/2)(v2-v1)*t
            Vector s = v1 * t + (vDiff * t) / 2;

            //小球移动
            TranslateTransform translate = (TranslateTransform)this.moveObj.RenderTransform;
            translate.X += s.X;
            translate.Y += s.Y;
            //新的相对位置
            point.X -= s.X;
            point.Y -= s.Y;
            //记录本次速度
            //v2=vDiff+v1
            Vector v2 = vDiff + v1;
            v1 = v2;
            //绘制线
            line.X1 = pMouse.X;
            line.Y1 = pMouse.Y;
            line.X2 = pMouse.X - point.X;
            line.Y2 = pMouse.Y - point.Y;
        }
    }
}
