﻿using Private_Ethercloset.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.ViewModel
{
    public class LockerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BitmapImage> Images { get; private set; }
        private string galleryDirectory;
        private MainViewModel _MainViewModel;

        public LockerViewModel()
        {
            galleryDirectory = DirectoryManager.getGalleryDirectory();

            Images = new ObservableCollection<BitmapImage>();

            LoadImagesFromDirectory();


        }

        public LockerViewModel(MainViewModel mainViewModel)
        {
            galleryDirectory = DirectoryManager.getGalleryDirectory();

            Images = new ObservableCollection<BitmapImage>();
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
                    Images.Add(new BitmapImage(new Uri(imagePath)));
                }
            }

        }

        public void NavigateToDecrypt(BitmapImage image)
        {
            _MainViewModel.NavigateToDecrypt(image);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
