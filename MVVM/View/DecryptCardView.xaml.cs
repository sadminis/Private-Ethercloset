using Private_Ethercloset.MVVM.Model;
using Private_Ethercloset.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for DecryptCardView.xaml
    /// </summary>
    public partial class DecryptCardView : UserControl
    {
        private BitmapImage? _image;

        private const int EncryptEnd = 0b1111111111111111;//65535
        private const int leagalIndicesLength = 23;
        private DecryptCardViewModel _VMRef;

        private const short WeaponIndex = 0;
        private const short WeaponDye1Index = 1;
        private const short WeaponDye2Index = 2;
        private const int initIndex = 0;


        public DecryptCardView()
        {
            InitializeComponent();

            ResetAllUI();
        }
        public DecryptCardView(BitmapImage importedCard)
        {
            InitializeComponent();
            SetupImage(importedCard);


        }

        public void SetupImage(BitmapImage image)
        {
            ImageDisplay.Source = image;
            _image = image;
            UpdateUI(image);
        }

        //significantly optimized performance by locking pixels. However image will have to be 32bpp or there will be an error
        //32bpp is ensured in createCardView functions
        // same as it in LockerView
        public List<int> ExtractIndicesFromImage_Optimized(Bitmap bitmap)
        {
            List<int> indices = new List<int>();
            StringBuilder binaryData = new StringBuilder();

            // Lock the bitmap's bits.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Example of checking the pixel format
            var pixelFormat = bitmap.PixelFormat;

            //Iterate through the pixels.
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int index = (y * bmpData.Stride) + (x * 4); // Assuming 32bpp
                    //check 32bpp
                    if(index >= rgbValues.Length - 2)
                    {
                        break;
                    }
                    byte red = rgbValues[index + 2]; // Red channel (BGRA format)

                    // Read the LSB of the red channel and append it
                    binaryData.Append((red & 0x01)); // Get the last bit
                }
            }


            bitmap.UnlockBits(bmpData); // Unlock the bits

            // Convert binary string to integers
            for (int i = 0; i < binaryData.Length; i += 16)
            {
                if (i + 16 <= binaryData.Length)
                {
                    string byteString = binaryData.ToString().Substring(i, 16);
                    int index = Convert.ToInt32(byteString, 2);

                    //reached the end of list. return
                    if (index >= EncryptEnd)
                    {
                        return indices;
                    }
                    indices.Add(index);

                }
            }

            return indices;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = DirectoryManager.ImportPicture();
            if (imagePath != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                bitmap.Freeze(); // Optionally freeze the bitmap for performance

                // Set the ImageSource of the Image control
                ImageDisplay.Source = bitmap;
                _image = bitmap;
                UpdateUI(bitmap);
            }

        }

        //reset all UI elements using an int list of all 0's
        private void ResetAllUI()
        {
            
            List<int> intList = Enumerable.Repeat(initIndex, leagalIndicesLength).ToList();
            SetUIwithIndicesList(intList);
        }

        private void SetUIwithIndicesList(List<int> indiciesList)
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            //set Weapon info
            WeaponName.Text = indiciesList[0] == 0 ? "无内容" : //better init by adding a default entry in the data base
                databaseHelper.GetItemNameByID(indiciesList[0]);
            WeaponIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[0]), UriKind.RelativeOrAbsolute));
            WeaponDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[1]);
            WeaponDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[1]), UriKind.RelativeOrAbsolute));
            WeaponDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[2]);
            WeaponDyeIcon2.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[2]), UriKind.RelativeOrAbsolute));

            // Set Head info
            HeadName.Text = indiciesList[3] == 0 ? "无内容" :
                databaseHelper.GetItemNameByID(indiciesList[3]);
            HeadIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[3]), UriKind.RelativeOrAbsolute));
            HeadDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[4]);
            HeadDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[4]), UriKind.RelativeOrAbsolute));
            HeadDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[5]);
            HeadDyeIcon2.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[5]), UriKind.RelativeOrAbsolute));

            // Set Chest info
            ChestName.Text = databaseHelper.GetItemNameByID(indiciesList[6]);
            ChestIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[6]), UriKind.RelativeOrAbsolute));
            ChestDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[7]);
            ChestDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[7]), UriKind.RelativeOrAbsolute));
            ChestDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[8]);
            ChestDyeIcon2.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[8]), UriKind.RelativeOrAbsolute));

            // Set Hand info
            HandName.Text = databaseHelper.GetItemNameByID(indiciesList[9]);
            HandIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[9]), UriKind.RelativeOrAbsolute));
            HandDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[10]);
            HandDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[10]), UriKind.RelativeOrAbsolute));
            HandDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[11]);
            HandDyeIcon2.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[11]), UriKind.RelativeOrAbsolute));

            // Set Leg info
            LegName.Text = databaseHelper.GetItemNameByID(indiciesList[12]);
            LegIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[12]), UriKind.RelativeOrAbsolute));
            LegDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[13]);
            LegDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[13]), UriKind.RelativeOrAbsolute));
            LegDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[14]);
            LegDyeIcon2.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[14]), UriKind.RelativeOrAbsolute));

            // Set Foot info
            FootName.Text = databaseHelper.GetItemNameByID(indiciesList[15]);
            FootIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[15]), UriKind.RelativeOrAbsolute));
            FootDyeText1.Text = databaseHelper.GetDyeNameByID(indiciesList[16]);
            FootDyeIcon1.Source = new BitmapImage(new Uri(
                databaseHelper.GetDyeIconByID(indiciesList[16]), UriKind.RelativeOrAbsolute));
            FootDyeText2.Text = databaseHelper.GetDyeNameByID(indiciesList[17]);
            FootDyeIcon2.Source = new BitmapImage(new Uri(
               databaseHelper.GetDyeIconByID(indiciesList[17]), UriKind.RelativeOrAbsolute));

            //set Ear info
            EarName.Text = databaseHelper.GetItemNameByID(indiciesList[18]);
            EarIcon.Source = new BitmapImage(new Uri(
               databaseHelper.GetIconPathByID(indiciesList[18]), UriKind.RelativeOrAbsolute));


            // Set Neck info
            NeckName.Text = databaseHelper.GetItemNameByID(indiciesList[19]);
            NeckIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[19]), UriKind.RelativeOrAbsolute));

            // Set Bracelet info
            BraceletName.Text = databaseHelper.GetItemNameByID(indiciesList[20]);
            BraceletIcon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[20]), UriKind.RelativeOrAbsolute));

            // Set Ring1 info
            Ring1Name.Text = databaseHelper.GetItemNameByID(indiciesList[21]);
            Ring1Icon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[21]), UriKind.RelativeOrAbsolute));

            // Set Ring2 info
            Ring2Name.Text = databaseHelper.GetItemNameByID(indiciesList[22]);
            Ring2Icon.Source = new BitmapImage(new Uri(
                databaseHelper.GetIconPathByID(indiciesList[22]), UriKind.RelativeOrAbsolute));
        }

        //with given image, extract info and fill view textblocks n icons
        //includes converting to bitmap functionality
        private void UpdateUI(BitmapImage image)
        {
            ResetAllUI();
            if (image == null) { return; }
            //convert from bitmapimage to bit map
            Bitmap tempBitMap;
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a PNG bitmap encoder and save the BitmapImage to the stream
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);

                // Convert the stream to a Bitmap
                tempBitMap = new Bitmap(stream);
            }
            if(tempBitMap == null) { return; };

            var indiciesList = ExtractIndicesFromImage_Optimized(tempBitMap);

            
            if(indiciesList.Count< leagalIndicesLength)
            {
                //did not get enough data, meaning either image is not 32bpp or is invalid. Exit
                Debug.WriteLine("Indicies list too short, length should be " + leagalIndicesLength);
                return;
            }

            SetUIwithIndicesList(indiciesList);
        }
    }


}
