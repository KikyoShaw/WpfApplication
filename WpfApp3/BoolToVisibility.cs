using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp3
{
    public class BoolToVisibility : IValueConverter
    {
        private static readonly Lazy<BoolToVisibility>
               Lazy = new Lazy<BoolToVisibility>(() => new BoolToVisibility());

        public static BoolToVisibility Instance => Lazy.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    bool.TryParse(value.ToString(), out var result);
                    if (parameter == null)
                    {
                        return result ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else if (parameter.ToString() == "-") 
                    {
                        return result ? Visibility.Collapsed : Visibility.Visible;
                    }
                }
            }
            catch /*(Exception ex)*/
            {
                //HuyaFX.Log.LogUtils.Error("HyConverter", $"BoolToVisibility Convert Exception: {ex.Message}");
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
