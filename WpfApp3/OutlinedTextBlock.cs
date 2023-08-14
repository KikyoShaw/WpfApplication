using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows;

namespace WpfApp3
{
    //public enum StrokePosition
    //{
    //    Center,
    //    Outside,
    //    Inside
    //}

    //[ContentProperty("Text")]
    //public class OutlinedTextBlock : FrameworkElement
    //{
    //    private void UpdatePen()
    //    {
    //        _Pen = new Pen(Stroke, StrokeThickness)
    //        {
    //            DashCap = PenLineCap.Round,
    //            EndLineCap = PenLineCap.Round,
    //            LineJoin = PenLineJoin.Round,
    //            StartLineCap = PenLineCap.Round
    //        };

    //        if (StrokePosition == StrokePosition.Outside || StrokePosition == StrokePosition.Inside)
    //        {
    //            _Pen.Thickness = StrokeThickness * 2;
    //        }

    //        InvalidateVisual();
    //    }

    //    public StrokePosition StrokePosition
    //    {
    //        get { return (StrokePosition)GetValue(StrokePositionProperty); }
    //        set { SetValue(StrokePositionProperty, value); }
    //    }

    //    public static readonly DependencyProperty StrokePositionProperty =
    //        DependencyProperty.Register("StrokePosition",
    //            typeof(StrokePosition),
    //            typeof(OutlinedTextBlock),
    //            new FrameworkPropertyMetadata(StrokePosition.Outside, FrameworkPropertyMetadataOptions.AffectsRender));

    //    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
    //      "Fill",
    //      typeof(Brush),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    //    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
    //      "Stroke",
    //      typeof(Brush),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    //    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
    //      "StrokeThickness",
    //      typeof(double),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

    //    public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
    //      "Text",
    //      typeof(string),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextInvalidated));

    //    public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
    //      "TextAlignment",
    //      typeof(TextAlignment),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register(
    //      "TextDecorations",
    //      typeof(TextDecorationCollection),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register(
    //      "TextTrimming",
    //      typeof(TextTrimming),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(OnFormattedTextUpdated));

    //    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
    //      "TextWrapping",
    //      typeof(TextWrapping),
    //      typeof(OutlinedTextBlock),
    //      new FrameworkPropertyMetadata(TextWrapping.NoWrap, OnFormattedTextUpdated));

    //    private FormattedText _FormattedText;
    //    private Geometry _TextGeometry;
    //    private Pen _Pen;
    //    private PathGeometry _clipGeometry;

    //    public Brush Fill
    //    {
    //        get { return (Brush)GetValue(FillProperty); }
    //        set { SetValue(FillProperty, value); }
    //    }

    //    public FontFamily FontFamily
    //    {
    //        get { return (FontFamily)GetValue(FontFamilyProperty); }
    //        set { SetValue(FontFamilyProperty, value); }
    //    }

    //    [TypeConverter(typeof(FontSizeConverter))]
    //    public double FontSize
    //    {
    //        get { return (double)GetValue(FontSizeProperty); }
    //        set { SetValue(FontSizeProperty, value); }
    //    }

    //    public FontStretch FontStretch
    //    {
    //        get { return (FontStretch)GetValue(FontStretchProperty); }
    //        set { SetValue(FontStretchProperty, value); }
    //    }

    //    public FontStyle FontStyle
    //    {
    //        get { return (FontStyle)GetValue(FontStyleProperty); }
    //        set { SetValue(FontStyleProperty, value); }
    //    }

    //    public FontWeight FontWeight
    //    {
    //        get { return (FontWeight)GetValue(FontWeightProperty); }
    //        set { SetValue(FontWeightProperty, value); }
    //    }

    //    public Brush Stroke
    //    {
    //        get { return (Brush)GetValue(StrokeProperty); }
    //        set { SetValue(StrokeProperty, value); }
    //    }

    //    public double StrokeThickness
    //    {
    //        get { return (double)GetValue(StrokeThicknessProperty); }
    //        set { SetValue(StrokeThicknessProperty, value); }
    //    }

    //    public string Text
    //    {
    //        get { return (string)GetValue(TextProperty); }
    //        set { SetValue(TextProperty, value); }
    //    }

    //    public TextAlignment TextAlignment
    //    {
    //        get { return (TextAlignment)GetValue(TextAlignmentProperty); }
    //        set { SetValue(TextAlignmentProperty, value); }
    //    }

    //    public TextDecorationCollection TextDecorations
    //    {
    //        get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
    //        set { SetValue(TextDecorationsProperty, value); }
    //    }

    //    public TextTrimming TextTrimming
    //    {
    //        get { return (TextTrimming)GetValue(TextTrimmingProperty); }
    //        set { SetValue(TextTrimmingProperty, value); }
    //    }

    //    public TextWrapping TextWrapping
    //    {
    //        get { return (TextWrapping)GetValue(TextWrappingProperty); }
    //        set { SetValue(TextWrappingProperty, value); }
    //    }

    //    public OutlinedTextBlock()
    //    {
    //        UpdatePen();
    //        TextDecorations = new TextDecorationCollection();
    //    }

    //    protected override void OnRender(DrawingContext drawingContext)
    //    {
    //        EnsureGeometry();

    //        drawingContext.DrawGeometry(Fill, null, _TextGeometry);

    //        if (StrokePosition == StrokePosition.Outside)
    //        {
    //            drawingContext.PushClip(_clipGeometry);
    //        }
    //        else if (StrokePosition == StrokePosition.Inside)
    //        {
    //            drawingContext.PushClip(_TextGeometry);
    //        }

    //        drawingContext.DrawGeometry(null, _Pen, _TextGeometry);

    //        if (StrokePosition == StrokePosition.Outside || StrokePosition == StrokePosition.Inside)
    //        {
    //            drawingContext.Pop();
    //        }
    //    }

    //    protected override Size MeasureOverride(Size availableSize)
    //    {
    //        EnsureFormattedText();

    //        // constrain the formatted text according to the available size

    //        double w = availableSize.Width;
    //        double h = availableSize.Height;

    //        // the Math.Min call is important - without this constraint (which seems arbitrary, but is the maximum allowable text width), things blow up when availableSize is infinite in both directions
    //        // the Math.Max call is to ensure we don't hit zero, which will cause MaxTextHeight to throw
    //        _FormattedText.MaxTextWidth = Math.Min(3579139, w);
    //        _FormattedText.MaxTextHeight = Math.Max(0.0001d, h);

    //        // return the desired size
    //        return new Size(Math.Ceiling(_FormattedText.Width), Math.Ceiling(_FormattedText.Height));
    //    }

    //    protected override Size ArrangeOverride(Size finalSize)
    //    {
    //        EnsureFormattedText();

    //        // update the formatted text with the final size
    //        _FormattedText.MaxTextWidth = finalSize.Width;
    //        _FormattedText.MaxTextHeight = Math.Max(0.0001d, finalSize.Height);

    //        // need to re-generate the geometry now that the dimensions have changed
    //        _TextGeometry = null;
    //        UpdatePen();

    //        return finalSize;
    //    }

    //    private static void OnFormattedTextInvalidated(DependencyObject dependencyObject,
    //      DependencyPropertyChangedEventArgs e)
    //    {
    //        var outlinedTextBlock = (OutlinedTextBlock)dependencyObject;
    //        outlinedTextBlock._FormattedText = null;
    //        outlinedTextBlock._TextGeometry = null;

    //        outlinedTextBlock.InvalidateMeasure();
    //        outlinedTextBlock.InvalidateVisual();
    //    }

    //    private static void OnFormattedTextUpdated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        var outlinedTextBlock = (OutlinedTextBlock)dependencyObject;
    //        outlinedTextBlock.UpdateFormattedText();
    //        outlinedTextBlock._TextGeometry = null;

    //        outlinedTextBlock.InvalidateMeasure();
    //        outlinedTextBlock.InvalidateVisual();
    //    }

    //    private void EnsureFormattedText()
    //    {
    //        if (_FormattedText != null)
    //        {
    //            return;
    //        }

    //        _FormattedText = new FormattedText(
    //          Text ?? "",
    //          CultureInfo.CurrentUICulture,
    //          FlowDirection,
    //          new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
    //          FontSize,
    //          Brushes.Black, 96);

    //        UpdateFormattedText();
    //    }

    //    private void UpdateFormattedText()
    //    {
    //        if (_FormattedText == null)
    //        {
    //            return;
    //        }

    //        _FormattedText.MaxLineCount = TextWrapping == TextWrapping.NoWrap ? 1 : int.MaxValue;
    //        _FormattedText.TextAlignment = TextAlignment;
    //        _FormattedText.Trimming = TextTrimming;

    //        _FormattedText.SetFontSize(FontSize);
    //        _FormattedText.SetFontStyle(FontStyle);
    //        _FormattedText.SetFontWeight(FontWeight);
    //        _FormattedText.SetFontFamily(FontFamily);
    //        _FormattedText.SetFontStretch(FontStretch);
    //        _FormattedText.SetTextDecorations(TextDecorations);
    //    }

    //    private void EnsureGeometry()
    //    {
    //        if (_TextGeometry != null)
    //        {
    //            return;
    //        }

    //        EnsureFormattedText();
    //        _TextGeometry = _FormattedText.BuildGeometry(new Point(0, 0));

    //        if (StrokePosition == StrokePosition.Outside)
    //        {
    //            var boundsGeo = new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight));
    //            _clipGeometry = Geometry.Combine(boundsGeo, _TextGeometry, GeometryCombineMode.Exclude, null);
    //        }
    //    }
    //}

    public class OutlinedText : FrameworkElement, IAddChild
    {
        #region Private Fields

        private Geometry _textGeometry;

        #endregion

        #region Private Methods

        /// <summary>
        /// Invoked when a dependency property has changed. Generate a new FormattedText object to display.
        /// </summary>
        /// <param name="d">OutlineText object whose property was updated.</param>
        /// <param name="e">Event arguments for the dependency property.</param>
        private static void OnOutlineTextInvalidated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OutlinedText)d).CreateText();
        }

        #endregion


        #region FrameworkElement Overrides

        /// <summary>
        /// OnRender override draws the geometry of the text and optional highlight.
        /// </summary>
        /// <param name="drawingContext">Drawing context of the OutlineText control.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            CreateText();
            // Draw the outline based on the properties that are set.
            drawingContext.DrawGeometry(Fill, new Pen(Stroke, StrokeThickness), _textGeometry);

        }

        /// <summary>
        /// Create the outline geometry based on the formatted text.
        /// </summary>
        public void CreateText()
        {
            FontStyle fontStyle = FontStyles.Normal;
            FontWeight fontWeight = FontWeights.Medium;

            if (Bold == true) fontWeight = FontWeights.Bold;
            if (Italic == true) fontStyle = FontStyles.Italic;

            // Create the formatted text based on the properties set.
            FormattedText formattedText = new FormattedText(
                Text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(Font, fontStyle, fontWeight, FontStretches.Normal),
                FontSize,
                Brushes.Black // This brush does not matter since we use the geometry of the text. 
                , 96);

            // Build the geometry object that represents the text.
            _textGeometry = formattedText.BuildGeometry(new Point(0, 0));




            //set the size of the custome control based on the size of the text
            this.MinWidth = formattedText.Width;
            this.MinHeight = formattedText.Height;

        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Specifies whether the font should display Bold font weight.
        /// </summary>
        public bool Bold
        {
            get
            {
                return (bool)GetValue(BoldProperty);
            }

            set
            {
                SetValue(BoldProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Bold dependency property.
        /// </summary>
        public static readonly DependencyProperty BoldProperty = DependencyProperty.Register(
            "Bold",
            typeof(bool),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnOutlineTextInvalidated),
                null
                )
            );

        /// <summary>
        /// Specifies the brush to use for the fill of the formatted text.
        /// </summary>
        public Brush Fill
        {
            get
            {
                return (Brush)GetValue(FillProperty);
            }

            set
            {
                SetValue(FillProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Fill dependency property.
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof(Brush),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                new SolidColorBrush(Colors.LightSteelBlue),
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnOutlineTextInvalidated),
                null
                )
            );

        /// <summary>
        /// The font to use for the displayed formatted text.
        /// </summary>
        public FontFamily Font
        {
            get
            {
                return (FontFamily)GetValue(FontProperty);
            }

            set
            {
                SetValue(FontProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Font dependency property.
        /// </summary>
        public static readonly DependencyProperty FontProperty = DependencyProperty.Register(
            "Font",
            typeof(FontFamily),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                new FontFamily("Arial"),
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnOutlineTextInvalidated),
                null
                )
            );

        /// <summary>
        /// The current font size.
        /// </summary>
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }

            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the FontSize dependency property.
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize",
            typeof(double),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                 (double)48.0,
                 FrameworkPropertyMetadataOptions.AffectsRender,
                 new PropertyChangedCallback(OnOutlineTextInvalidated),
                 null
                 )
            );


        /// <summary>
        /// Specifies whether the font should display Italic font style.
        /// </summary>
        public bool Italic
        {
            get
            {
                return (bool)GetValue(ItalicProperty);
            }

            set
            {
                SetValue(ItalicProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Italic dependency property.
        /// </summary>
        public static readonly DependencyProperty ItalicProperty = DependencyProperty.Register(
            "Italic",
            typeof(bool),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                 false,
                 FrameworkPropertyMetadataOptions.AffectsRender,
                 new PropertyChangedCallback(OnOutlineTextInvalidated),
                 null
                 )
            );

        /// <summary>
        /// Specifies the brush to use for the stroke and optional hightlight of the formatted text.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return (Brush)GetValue(StrokeProperty);
            }

            set
            {
                SetValue(StrokeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Stroke dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke",
            typeof(Brush),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                 new SolidColorBrush(Colors.Teal),
                 FrameworkPropertyMetadataOptions.AffectsRender,
                 new PropertyChangedCallback(OnOutlineTextInvalidated),
                 null
                 )
            );

        /// <summary>
        ///     The stroke thickness of the font.
        /// </summary>
        public ushort StrokeThickness
        {
            get
            {
                return (ushort)GetValue(StrokeThicknessProperty);
            }

            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        /// <summary>
        /// Identifies the StrokeThickness dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness",
            typeof(ushort),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                 (ushort)0,
                 FrameworkPropertyMetadataOptions.AffectsRender,
                 new PropertyChangedCallback(OnOutlineTextInvalidated),
                 null
                 )
            );

        /// <summary>
        /// Specifies the text string to display.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Text dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(OutlinedText),
            new FrameworkPropertyMetadata(
                 "",
                 FrameworkPropertyMetadataOptions.AffectsRender,
                 new PropertyChangedCallback(OnOutlineTextInvalidated),
                 null
                 )
            );

        public void AddChild(Object value)
        {

        }

        public void AddText(string value)
        {
            Text = value;
        }

        #endregion
    }
}
