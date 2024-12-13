using Private_Ethercloset.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.ViewModel
{
    public class LockerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Image> Images { get; private set; }
        private string galleryDirectory;
        private MainViewModel _MainViewModel;

        public LockerViewModel()
        {
            galleryDirectory = DirectoryManager.getGalleryDirectory();

            Images = new ObservableCollection<Image>();

            LoadImagesFromDirectory();


        }

        public LockerViewModel(MainViewModel mainViewModel)
        {
            galleryDirectory = DirectoryManager.getGalleryDirectory();

            Images = new ObservableCollection<Image>();
            _MainViewModel = mainViewModel;
            LoadImagesFromDirectory();

            
        }

        public void LoadImagesFromDirectory()
        {
            Images.Clear();

            var directory = Directory.GetDirectories(galleryDirectory);

            foreach (var dir in directory)
            {
                var imagePath = Directory.GetFiles(dir).FirstOrDefault();

                if (imagePath != null)
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    // Create an Image control and set its properties
                    var imageControl = new System.Windows.Controls.Image
                    {
                        Source = bitmap,
                        Width = 100,
                        Height = 100,
                        Stretch = Stretch.UniformToFill,
                        Tag = imagePath // Store the file path in the Tag property
                    };

                    // Add the Image control to the ItemsControl
                    Images.Add(imageControl);
                }
            }
        }

        public void NavigateToDecrypt(SteganoCard steganoCard)
        {
            _MainViewModel.NavigateToDecrypt(steganoCard);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
