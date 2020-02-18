using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Infrastructure.Utils
{
    public static class Printing
    {

        //public static void PrintImage()
        //{
        //    PrintDocument pd = new PrintDocument();
        //    var pageSettings = new PageSettings();
        //    pageSettings.PaperSize = new PaperSize("Thermal Paper", size.Width, size.Height);
        //    pd.DefaultPageSettings = pageSettings;
        //    pd.PrintPage += (o, e) => PrintPage(queueNumber, size, e);
        //    pd.Print();
        //}

        //private static void PrintPage(int queueNumber, Size size, PrintPageEventArgs e)
        //{
        //    //var g = e.Graphics;
        //    //g.SmoothingMode = SmoothingMode.AntiAlias;
        //    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;


        //    //var wholeRect = new RectangleF(new PointF(0, 0), size);
        //    //g.FillRectangle(Brushes.White, wholeRect);

        //    //// draw queue number
        //    //StringFormat sf = new StringFormat();
        //    //sf.Alignment = StringAlignment.Center;
        //    //sf.LineAlignment = StringAlignment.Center;
        //    //g.DrawString($"{queueNumber}", new Font("Arial", 50, FontStyle.Bold), Brushes.Black, wholeRect, sf);

        //    //// draw queue number
        //    //StringFormat sf = new StringFormat();
        //    //sf.Alignment = StringAlignment.Center;
        //    //sf.LineAlignment = StringAlignment.Center;
        //    //g.DrawString($"{queueNumber}", new Font("Arial", 50, FontStyle.Bold), Brushes.Black, wholeRect, sf);

        //}

        public static void PrintBitmapSource(BitmapSource bmp)
        {
            var pd = new PrintDialog();
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawImage(bmp, new Rect(0, 0, bmp.Width, bmp.Height));
            }
            pd.PrintVisual(dv, "document image");
        }
    }
}
