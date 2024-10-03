using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.Model
{
    public static class DirectoryManager
    {
        public static string getGalleryDirectory()
        {
            string galleryDirectory = Path.Combine(Environment.CurrentDirectory, "Gallery");

            if (!Directory.Exists(galleryDirectory))
            {
                Directory.CreateDirectory(galleryDirectory);
            }

            return galleryDirectory;
        }

        public static string getResourceImagesDirectory()
        {
            string imagesDirectory = Path.Combine(Environment.CurrentDirectory, "Images");
            return imagesDirectory;
        }

        public static BitmapImage ImportPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files |*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath); // Load the file into the BitmapImage
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Optional: to avoid locking the file
                bitmap.EndInit();

                return bitmap;
            }

            return null;
        }
    }
}
