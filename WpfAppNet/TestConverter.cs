using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfAppNet
{
    public class TestConverter : IValueConverter
    {
        private static readonly Lazy<TestConverter> LazyInstance = new Lazy<TestConverter>(() => new TestConverter());
        public static TestConverter Instance => LazyInstance.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string parameters)
            {
                string[] paramArray = parameters.Split(',');
                if (paramArray.Length >= 3)
                {
                    string param1 = paramArray[0].Trim();
                    string param2 = paramArray[1].Trim();
                    string param3 = paramArray[2].Trim();

                    // 根据参数值返回适当的 Visibility 值
                    if (param1 == "1" && param2 == "2" && param3 == "3")
                    {
                        return Visibility.Visible;
                    }
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
