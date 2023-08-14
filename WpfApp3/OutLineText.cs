using System.ComponentModel;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp3
{
    public class OutLineText : Shape
    {
        private Geometry _textGeometry;

        #region Dependency Properties
        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(OutLineText),
                                                     new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily,
                                                             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [Localizability(LocalizationCategory.Font)]
        [TypeConverter(typeof(FontFamilyConverter))]
        public FontFamily FontFamily 
        { 
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(typeof(OutLineText),
                                                        new FrameworkPropertyMetadata(SystemFonts.MessageFontSize,
                                                             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        public double FontSize 
        { 
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(typeof(OutLineText),
                                                     new FrameworkPropertyMetadata(TextElement.FontStretchProperty.DefaultMetadata.DefaultValue,
                                                             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontStretchConverter))]
        public FontStretch FontStretch 
        { 
            get => (FontStretch)GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }

        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(typeof(OutLineText),
                                                     new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle,
                                                             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontStyleConverter))]
        public FontStyle FontStyle 
        {
            get => (FontStyle)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(typeof(OutLineText),
                                                     new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight,
                                                             FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontWeightConverter))]
        public FontWeight FontWeight 
        { 
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        public static readonly DependencyProperty OriginPointProperty =
                                        DependencyProperty.Register("Origin", typeof(Point), typeof(OutLineText),
                                                new FrameworkPropertyMetadata(new Point(0, 0),
                                                        FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(PointConverter))]
        public Point Origin 
        { 
            get => (Point)GetValue(OriginPointProperty);
            set => SetValue(OriginPointProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
                                        DependencyProperty.Register("Text", typeof(string), typeof(OutLineText),
                                                new FrameworkPropertyMetadata(string.Empty,
                                                        FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                             OnPropertyChanged));
        [Bindable(true), Category("Appearance")]
        public string Text 
        { 
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        protected override Geometry DefiningGeometry => _textGeometry;

        //_textGeometry = Geometry.Empty;//_textGeometry ;
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OutLineText)d).CreateTextGeometry();
        }


        private void CreateTextGeometry()
        {
            //var formattedText = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight,
            //                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);
            //var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1B2C59")!);
            var formattedText = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.White, 96);
            _textGeometry = formattedText.BuildGeometry(Origin);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_textGeometry == null) CreateTextGeometry();
            if (_textGeometry.Bounds == Rect.Empty)
                return new Size(0, 0);
            // return the desired size
            return new Size(Math.Min(availableSize.Width, _textGeometry.Bounds.Width), Math.Min(availableSize.Height, _textGeometry.Bounds.Height));
        }

    }
}
