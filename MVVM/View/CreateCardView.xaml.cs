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
            var image = DirectoryManager.ImportPicture();
            if (image != null) {
                //var cropWindow = new CropWindow(image);
                //cropWindow.Show();
            }
            
        }
    }
}
