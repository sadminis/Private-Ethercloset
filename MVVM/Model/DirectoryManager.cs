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

        public static string getIconsRootPath() 
        {
            //
            string iconsDirectory = Path.Combine(Environment.CurrentDirectory, "Resources");
            return Path.Combine(iconsDirectory, "Icons"); ;
        }

        public static string? ImportPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files |*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                return filePath;
            }

            return null;
        }

        public static string getNewLocker()
        {
            string galleryPath = getGalleryDirectory();

            // Get all existing folders in the gallery
            var existingFolders = Directory.GetDirectories(galleryPath)
                                            .Select(folder => new DirectoryInfo(folder).Name)
                                            .ToList();

            // Find the next available folder number
            int nextFolderNumber = 1;

            // Increment the folder number based on existing folders
            while (existingFolders.Contains(nextFolderNumber.ToString()))
            {
                nextFolderNumber++;
            }

            // Create the new folder
            string newFolderPath = Path.Combine(galleryPath, nextFolderNumber.ToString());
            Directory.CreateDirectory(newFolderPath);

            return newFolderPath;
        }

        public static string getDatabasePath()
        {
            string appDataPath = Path.Combine(Environment.CurrentDirectory, "Resources");
            return Path.Combine(appDataPath, "items.db");
        }
    }
}
