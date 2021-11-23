using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Converter
{
    public class LongCountToShortText : IValueConverter
    {
        private static readonly Lazy<LongCountToShortText>
            Lazy = new Lazy<LongCountToShortText>(() => new LongCountToShortText());
        public static LongCountToShortText Instance => Lazy.Value;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null || value == DependencyProperty.UnsetValue)
                {
                    return "";
                }

                long Count = System.Convert.ToInt64(value.ToString());
                long OneY = 10000 * 10000;
                long OneW = 10000;

                if (Count >= OneY)
                {
                    long y = Count / OneY;
                    long w = (Count % OneY) / 10000000;
                    return string.Format("{0}.{1}亿", y, w);
                }
                else if (Count >= OneW)
                {
                    long w = Count / OneW;
                    long q = (Count % OneW) / 1000;
                    return string.Format("{0}.{1}万", w, q);
                }
                else
                {
                    return Count.ToString();
                }
            }
            catch { }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
