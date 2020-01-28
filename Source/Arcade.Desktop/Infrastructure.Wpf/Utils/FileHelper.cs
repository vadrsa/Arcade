using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Infrastructure.Utils
{
    public static class FileHelper
    {
        public static string LoadImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
