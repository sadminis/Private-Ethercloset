using Private_Ethercloset.MVVM.Model;
using Private_Ethercloset.MVVM.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xunit;


namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for LockerView.xaml
    /// </summary>
    public partial class LockerView : UserControl
    {
        private const int EncryptEnd = 0b1111111111111111;//65535

        public LockerView()
        {
            InitializeComponent();
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string imagePath = image.Tag as string;

            Assert.NotNull(imagePath);

            SteganoCard steganoCard = new SteganoCard(imagePath);
            string decryptedMessage = steganoCard.decrypt();

            steganoCard.loadWithMessage(decryptedMessage);


            LockerViewModel lockerViewModel = (LockerViewModel)DataContext;

            Assert.NotNull(lockerViewModel);
          
            lockerViewModel.NavigateToDecrypt(steganoCard);


                    // Call your method to extract indices
                    //var listOfIndices = ExtractIndicesFromImage_Optimized(imageItem);
                    //Debug.WriteLine("List of indices: " + string.Join(", ", listOfIndices));

            Debug.WriteLine("End execution");
        }


        

    }
}
/* 10/11/2024
discarded version
lock to SIGNIFICANTLY increase process speed
however this method assumes 32bpp and doesn't work in other pixel format

 */