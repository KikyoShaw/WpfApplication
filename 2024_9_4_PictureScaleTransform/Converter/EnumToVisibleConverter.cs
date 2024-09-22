using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace _2024_9_4_PictureScaleTransform.Converter
{
    public class EnumToVisibleConverter : IValueConverter
    {
        private static readonly Lazy<EnumToVisibleConverter> _sLazy = new Lazy<EnumToVisibleConverter>(() => new EnumToVisibleConverter());
        public static EnumToVisibleConverter Instance => _sLazy.Value;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                if (value == null
                    || parameter == null)
                {
                    return Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                //HuyaFX.Log.LogUtils.Error("HyConverter", $"BoolToVisibility Convert Exception: {ex.Message}");
            }

            return value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
