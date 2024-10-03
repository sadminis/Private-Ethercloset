using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Drawing;
using Private_Ethercloset.MVVM.Model;
using System.Windows.Media.Imaging;
using System;


namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateCardView.xaml
    /// </summary>
    public partial class CreateCardView : UserControl
    {
        public CreateCardView()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        { 
            var imagePath = DirectoryManager.ImportPicture();
            if (imagePath != null) {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                bitmap.Freeze(); // Optionally freeze the bitmap for performance

                // Set the ImageSource of the Image control
                ImageDisplay.Source = bitmap;
            }
            
        }
    }
}
