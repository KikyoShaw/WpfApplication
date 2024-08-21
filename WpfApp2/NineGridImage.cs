using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace WpfApp2
{
    public class NineGridImage : Image
    {
        //public static ImageSource SplitImage(BitmapSource source, Int32Rect clipRect)
        //{
        //    ImageSource results = null;
        //    var stride = clipRect.Width * ((source.Format.BitsPerPixel + 7) / 8);
        //    var pixelsCount = clipRect.Width * clipRect.Height;//tileWidth * tileHeight;
        //    var tileRect = new Int32Rect(0, 0, clipRect.Width, clipRect.Height);

        //    var pixels = new int[pixelsCount];
        //    //var copyRect = new Int32Rect(col * tileWidth, row * tileHeight, tileWidth, tileHeight);
        //    source.CopyPixels(clipRect, pixels, stride, 0);
        //    var wb = new WriteableBitmap(
        //        clipRect.Width,
        //        clipRect.Height,
        //        source.DpiX,
        //        source.DpiY,
        //        source.Format,
        //        source.Palette);
        //    wb.Lock();
        //    wb.WritePixels(tileRect, pixels, stride, 0);
        //    wb.Unlock();

        //    results = wb;
        //    return results;
        //}

        //public static ImageSource[] Get9CellImageSource(BitmapSource source, Int32Rect clipRect)
        //{
        //    ImageSource[] results = new ImageSource[9];
        //    Int32Rect rect = Int32Rect.Empty;
        //    int rightSideWidth = (int)(source.PixelWidth - clipRect.X - clipRect.Width);
        //    //top-left
        //    rect.Width = clipRect.X;
        //    rect.Height = clipRect.Y;
        //    results[0] = SplitImage(source, rect);
        //    //return results;
        //    //top-middle
        //    rect.X += rect.Width;
        //    rect.Width = clipRect.Width;
        //    results[1] = SplitImage(source, rect);
        //    //top-right
        //    rect.X += rect.Width;
        //    rect.Width = rightSideWidth;
        //    results[2] = SplitImage(source, rect);

        //    //left side
        //    rect = Int32Rect.Empty;
        //    rect.Y = clipRect.Y;
        //    rect.Width = clipRect.X;
        //    rect.Height = clipRect.Height;
        //    results[3] = SplitImage(source, rect);

        //    //middle
        //    rect.X += rect.Width;
        //    rect.Width = clipRect.Width;
        //    results[4] = SplitImage(source, rect);

        //    //right side
        //    rect.X += rect.Width;
        //    rect.Width = rightSideWidth;
        //    results[5] = SplitImage(source, rect);
        //    //bottom-left
        //    rect = Int32Rect.Empty;
        //    rect.Y = clipRect.Y + clipRect.Height;
        //    // rect.X = clipRect.X;
        //    rect.Height = source.PixelHeight - clipRect.Height - clipRect.Y;
        //    rect.Width = clipRect.X;
        //    results[6] = SplitImage(source, rect);

        //    //bottom-middle
        //    rect.X += rect.Width;
        //    rect.Width = clipRect.Width;
        //    results[7] = SplitImage(source, rect);
        //    //bottom-right
        //    rect.X += rect.Width;
        //    rect.Width = rightSideWidth;
        //    results[8] = SplitImage(source, rect);
        //    return results;
        //}

        //protected override void OnRender(System.Windows.Media.DrawingContext dc)
        //{

        //    if (ClipRect != Int32Rect.Empty)
        //    {
        //        //剪裁绘图
        //        DrawBitblt(dc);
        //        return;
        //    }
        //    if (DrawImageWith9Cells == false)
        //    {
        //        base.OnRender(dc);
        //        return;
        //    }
        //    RenderWith9Cells(dc);
        //}

        //private void DrawBitblt(DrawingContext dc)
        //{

        //    //增加剪裁后九宫
        //    if (DrawImageWith9Cells == false)
        //    {
        //        ImageSource source = GetImageSource();
        //        Rect rect = new Rect(new Point(0, 0), new Size(ActualWidth, ActualHeight));
        //        dc.DrawImage(source, rect);
        //    }
        //    else
        //    {
        //        RenderWith9Cells(dc);
        //    }
        //}

        ///// <summary>
        ///// 9格绘图
        ///// </summary>
        ///// <param name="dc"></param>
        //private void RenderWith9Cells(System.Windows.Media.DrawingContext dc)
        //{
        //    if (Source == null) return;
        //    if (ClipPadding.Right == 0 || ClipPadding.Bottom == 0) return;

        //    ImageSource source = Source;
        //    Uri u = new Uri(source.ToString());
        //    BitmapSource image = new BitmapImage(u);
        //    if (ClipRect != Int32Rect.Empty)
        //    {
        //        //预剪裁
        //        image = ImageClip.SplitImage(image, ClipRect) as BitmapSource;
        //    }
        //    double contentWidth = image.PixelWidth - ClipPadding.Left - ClipPadding.Right;
        //    double contentHeight = image.PixelHeight - ClipPadding.Top - ClipPadding.Bottom;
        //    Int32Rect contentRect = new Int32Rect((int)ClipPadding.Left, (int)ClipPadding.Top
        //                                                                    , (int)contentWidth, (int)contentHeight);

        //    //  Rect r = new Rect(new Point(),RenderSize);
        //    //  dc.DrawImage(image,r);
        //    //return;
        //    // image.BeginInit();
        //    // image.EndInit(); 
        //    ImageSource[] images = ImageClip.Get9CellImageSource(image, contentRect);
        //    if (images == null || images.Length != 9)
        //    {
        //        base.OnRender(dc);
        //        return;
        //    }
        //    DrawFrame(dc, images, contentRect);
        //    // DrawContent(contentRect, dc);
        //}



        ///// <summary>
        ///// 绘制边框
        ///// </summary>
        ///// <param name="dc"></param>
        //private void DrawFrame(System.Windows.Media.DrawingContext drawingContext, ImageSource[] images, Int32Rect contentRect)
        //{
        //    Rect drawRect = new Rect(new Point(), new Size(contentRect.X, contentRect.Y));
        //    double drawWidth = ActualWidth - ClipPadding.Left - ClipPadding.Right;
        //    double drawHeight = ActualHeight - ClipPadding.Top - ClipPadding.Bottom;
        //    drawingContext.DrawImage(images[0], drawRect);

        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = drawWidth;
        //    drawingContext.DrawImage(images[1], drawRect);
        //    //for (int i = (int)drawRect.X; i < ActualWidth - contentRect.Width - contentRect.X; i += contentRect.Width)
        //    //{
        //    //    drawRect.X += drawRect.Width;
        //    //    drawRect.Width = contentRect.Width;
        //    //    drawingContext.DrawImage(images[1], drawRect);
        //    //}
        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = ClipPadding.Right;
        //    drawingContext.DrawImage(images[2], drawRect);
        //    //中间

        //    drawRect.X = 0;
        //    drawRect.Y = contentRect.Y;
        //    drawRect.Width = contentRect.X;
        //    drawRect.Height = drawHeight;
        //    drawingContext.DrawImage(images[3], drawRect);

        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = drawWidth;
        //    drawingContext.DrawImage(images[4], drawRect);

        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = ClipPadding.Right;
        //    drawingContext.DrawImage(images[5], drawRect);

        //    //下边
        //    drawRect.X = 0;
        //    drawRect.Y = ActualHeight - ClipPadding.Bottom;
        //    drawRect.Width = ClipPadding.Left;
        //    drawRect.Height = ClipPadding.Bottom;
        //    drawingContext.DrawImage(images[6], drawRect);

        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = drawWidth;
        //    drawingContext.DrawImage(images[7], drawRect);

        //    drawRect.X += drawRect.Width;
        //    drawRect.Width = ClipPadding.Right;
        //    drawingContext.DrawImage(images[8], drawRect);


        //}

        //private void DrawClip(Rect clipRect, System.Windows.Media.DrawingContext drawingContext, Rect targetRect)
        //{
        //    Rect rect = targetRect;// new Rect(new Point(0, 0), targetRect);
        //    RectangleGeometry rectangleGeometry = new RectangleGeometry(clipRect);
        //    rectangleGeometry.Freeze();
        //    drawingContext.PushClip(rectangleGeometry);
        //    drawingContext.DrawImage(Source, rect);
        //    drawingContext.Pop();
        //}

    }
}
