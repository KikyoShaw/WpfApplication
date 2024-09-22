using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace _2024_9_4_PictureScaleTransform.Converter
{
    public class DoubleToVisibleConverter:IValueConverter
    {
        private static readonly Lazy<DoubleToVisibleConverter> _sLazy = new Lazy<DoubleToVisibleConverter>(() => new DoubleToVisibleConverter());
        public static DoubleToVisibleConverter Instance => _sLazy.Value;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if (double.TryParse(value.ToString(), out double res))
                    {
                        return res > 0 ? Visibility.Collapsed : Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
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
