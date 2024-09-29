using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static string ImportPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}
