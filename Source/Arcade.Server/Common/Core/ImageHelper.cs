using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Common.Core
{
    public static class ImageHelper
    {

        public static string GetImageExtension(byte[] bytes)
        {
            Bitmap bitmap = ToImage(bytes);
            string ext = null;
            if (bitmap.RawFormat.Equals(ImageFormat.Bmp)) ext = "bmp";
            else if (bitmap.RawFormat.Equals(ImageFormat.Jpeg)) ext = "jpg";
            else if (bitmap.RawFormat.Equals(ImageFormat.Png)) ext = "png";
            return ext;
        }
        
        private static Bitmap ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new Bitmap(ms);
                return image;
            }
        }

        public static byte[] ResizeImage(byte[] bytes , int height)
        {
            Bitmap image = ToImage(bytes);
            int width = (int)((height / (float)image.Height) * image.Width);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.Clear(Color.White);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            int quality = 90; //90 is the magic setting - really. It has excellent quality and file size.
            using(MemoryStream stream = new MemoryStream())
            {

                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
                destImage.Save(stream, GetImageCodeInfo("image/jpeg"), encoderParameters);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Returns the first ImageCodeInfo instance with the specified mime type. Some people try to get the ImageCodeInfo instance by index - sounds rather fragile to me.
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetImageCodeInfo(string mimeType)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in info)
                if (ici.MimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
                    return ici;
            return null;
        }
    }
}
