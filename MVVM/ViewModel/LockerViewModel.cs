using Private_Ethercloset.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.ViewModel
{
    class LockerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BitmapImage> Images { get; private set; }
        private string galleryDirectory;

        public LockerViewModel()
        {
            galleryDirectory = DirectoryManager.getGalleryDirectory();

            Images = new ObservableCollection<BitmapImage>();

            LoadImagesFromDirectory(galleryDirectory);

            MonitorDirectory(galleryDirectory);
        }

        private void LoadImagesFromDirectory(string directoryPath)
        {
            var directory = Directory.GetDirectories(directoryPath);

            foreach (var dir in directory)
            {
                var imagePath = Directory.GetFiles(dir).FirstOrDefault();

                if (imagePath != null)
                {
                    Images.Add(new BitmapImage(new Uri(imagePath)));
                }
            }
        }

        private void MonitorDirectory(string directoryPath)
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite,
                Filter = "*.*",
                EnableRaisingEvents = true
            };

            watcher.Created += (sender, e) => LoadImagesFromDirectory(directoryPath);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
