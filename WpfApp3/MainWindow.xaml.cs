using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var sUrl = @"http://te�Ef";
            var result = CheckIsUrlFormat(sUrl);

            bool result2 = Uri.IsWellFormedUriString(sUrl, UriKind.RelativeOrAbsolute);


            int tt = 39;
            int kk = tt / 60;
            var hh = tt % 60;
        }

        /// <summary>
        /// 检测串值是否为合法的网址格式
        /// </summary>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsUrlFormat(string strValue)
        {
            return CheckIsFormat(@"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", strValue);
        }

        /// <summary>
        /// 检测串值是否为合法的格式
        /// </summary>
        /// <param name="strRegex">正则表达式</param>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsFormat(string strRegex, string strValue)
        {
            try
            {
                if (strValue != null && strValue.Trim() != "")
                {
                    Regex re = new Regex(strRegex);
                    if (re.IsMatch(strValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        /// <summary>
        /// 检测链接是否为合法的网址格式
        /// </summary>
        /// <param name="uri">待检测的链接</param>
        /// <returns></returns>
        public bool CheckUrlIsValid(string uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                    return false;

                var regex = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                Regex re = new Regex(regex);
                return re.IsMatch(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
