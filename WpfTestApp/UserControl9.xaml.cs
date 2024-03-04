using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestApp
{
    /// <summary>
    /// UserControl9.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl9 : UserControl
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private System.Windows.Threading.DispatcherTimer m_Timer1 = new System.Windows.Threading.DispatcherTimer();

        double m_Percent = 1;
        bool m_IsStart = false;

        public UserControl9()
        {
            InitializeComponent();
            m_Timer1.Interval = TimeSpan.FromMilliseconds(300);
            m_Timer1.Tick += M_Timer1_Tick;
        }

        private void M_Timer1_Tick(object sender, EventArgs e)
        {
            m_Percent -= 0.01;
            if (m_Percent <= 0)
            {
                m_Percent = 1;
                m_Timer1.Stop();
                m_IsStart = false;
                StartChange(m_IsStart);
            }
            circleProgressBar.CurrentValue1 = m_Percent;
        }

        /// <summary>
        /// UI变化
        /// </summary>
        /// <param name="bState"></param>
        private void StartChange(bool bState)
        {
            if (bState)
                btnStart.Content = "停止";
            else
                btnStart.Content = "开始";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (m_IsStart)
            {
                m_Timer1.Stop();
                m_IsStart = false;

            }
            else
            {
                m_Percent = 1;
                m_Timer1.Start();
                m_IsStart = true;

            }
            StartChange(m_IsStart);
        }
    }
}
