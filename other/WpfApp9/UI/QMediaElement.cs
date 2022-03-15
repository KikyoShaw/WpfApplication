using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp9
{
    public class QMediaElement : MediaElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            SolidColorBrush nameColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#101010"));
            drawingContext.PushOpacityMask(nameColor);

            base.OnRender(drawingContext);
            drawingContext.Pop();
            return;

            if (ActualWidth == 0 || ActualHeight == 0)
            {
                base.OnRender(drawingContext);
                return;
            }
             
            try
            {

                WriteableBitmap bitmpCache = null;
                Rect bitmapRect = new Rect(0, 0, ActualWidth, ActualHeight);
                bitmpCache = new WriteableBitmap((int)bitmapRect.Width, (int)bitmapRect.Height, 96, 96, PixelFormats.Pbgra32, null);
                byte[] pixels = new byte[(int)bitmapRect.Width * (int)bitmapRect.Height * 4];

                DrawingVisual drawingVisual = new DrawingVisual();
                {
                    DrawingContext dc = drawingVisual.RenderOpen();
                    base.OnRender(dc);
                    dc.Close();
                    dc = null;
                }
                RenderTargetBitmap r = new RenderTargetBitmap(
                        (int)bitmapRect.Width, (int)bitmapRect.Height, 96, 96, PixelFormats.Pbgra32);
                r.Render(drawingVisual);
                int stride = ((r.PixelWidth * r.Format.BitsPerPixel + 7) / 8);
                r.CopyPixels(pixels, stride, 0);
                /*for(int h = 0; h <(int)bitmapRect.Height; h++)
                {
                    int x = h * (int)bitmapRect.Width * 4;
                    for (int w = 0; w < (int)bitmapRect.Width; w++)
                    {
                        int pixPos = x  + w * 4;
                        var blue = pixels[pixPos] ;
                        var green = pixels[pixPos + 1];
                        var red = pixels[pixPos + 2];
                        var alpha = pixels[pixPos + 3];
                        if(blue == 0 || green == 0 || red == 0)
                        {
                            int i = 0;
                        }
                    }

                }*/
                bitmpCache.Lock();
                Int32Rect rect = new Int32Rect(0, 0, (int)bitmapRect.Width, (int)bitmapRect.Height);
                bitmpCache.WritePixels(rect, pixels, stride, 0);
                //bitmpCache.AddDirtyRect(rect);
                bitmpCache.Unlock();

                drawingContext.DrawImage(bitmpCache, bitmapRect);
                
                // base.OnRender(drawingContext);
            }
            catch (Exception exception)
            {
                int i = 0;
                //HuyaApplet.LogHelper.LogError($"CreateNewView Notify Failed! uuid=>{extMainType.ExtMain.extUuid}, popupContainer=>{popupContainer}, popupLevel=>{popupLevel}, exception=>{exception}");
            }
        }
    }
}
