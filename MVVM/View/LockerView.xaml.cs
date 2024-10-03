using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for LockerView.xaml
    /// </summary>
    public partial class LockerView : UserControl
    {
        public LockerView()
        {
            InitializeComponent();
        }


        public List<int> ExtractIndicesFromImage(Bitmap bitmap)
        {
            List<int> indices = new List<int>();
            string binaryData = "";

            // Read the LSB of the red channel from each pixel
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Get the pixel color
                    System.Drawing.Color pixelColor = bitmap.GetPixel(x, y);

                    // Read the LSB of the red channel
                    binaryData += (pixelColor.R & 0x01); // Get the last bit
                }
            }

            // Convert binary string to integers
            // Assuming each integer is stored as 32 bits (4 bytes)
            for (int i = 0; i < binaryData.Length; i += 32)
            {
                if (i + 32 <= binaryData.Length)
                {
                    string byteString = binaryData.Substring(i, 32);
                    int index = Convert.ToInt32(byteString, 2); // Convert binary to integer
                    indices.Add(index);
                }
            }

            return indices;
        }
    }
}
