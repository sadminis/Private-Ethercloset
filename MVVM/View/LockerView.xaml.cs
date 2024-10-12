using Private_Ethercloset.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LockerView.xaml
    /// </summary>
    public partial class LockerView : UserControl
    {
        private const int EncryptEnd = 0b1111111111111111;//65535


        private List<string> items = new List<string>
            {
                "无染色",
                "----------------",
                "素雪白染剂",
                "苍白灰染剂",
                "古菩灰染剂",
                "石板灰染剂",
                "木炭灰染剂",
                "煤烟黑染剂",
                "----------------",
                "玫瑰粉染剂",
                "丁香紫染剂",
                "罗兰莓染剂",
                "卫月红染剂",
                "铁锈红染剂",
                "果酒红染剂",
                "珊瑚粉染剂",
                "鲜血红染剂",
                "鲑鱼粉染剂",
                "宝石红染剂",
                "樱桃粉染剂",
                "----------------",
                "日落橙染剂",
                "台地红染剂",
                "树皮棕染剂",
                "巧克力染剂",
                "铁锈棕染剂",
                "钴铁棕染剂",
                "软木棕染剂",
                "卢恩棕染剂",
                "奥猴棕染剂",
                "山羊棕染剂",
                "南瓜橙染剂",
                "橡果棕染剂",
                "果园棕染剂",
                "山栗棕染剂",
                "哥布林染剂",
                "页岩棕染剂",
                "鼹鼠棕染剂",
                "沃土棕染剂",
                "----------------",
                "骸骨白染剂",
                "黄沙棕染剂",
                "沙漠黄染剂",
                "蜂蜜黄染剂",
                "玉米黄染剂",
                "猛豹黄染剂",
                "奶油黄染剂",
                "日影黄染剂",
                "萄干棕染剂",
                "丝雀黄染剂",
                "香草黄染剂",
                "----------------",
                "泥沼绿染剂",
                "妖精绿染剂",
                "青柠绿染剂",
                "苔藓绿染剂",
                "牧草绿染剂",
                "橄榄绿染剂",
                "沼泽绿染剂",
                "苹果绿染剂",
                "仙人掌染剂",
                "猎人绿染剂",
                "口花绿染剂",
                "金龟绿染剂",
                "地神绿染剂",
                "深林绿染剂",
                "天上蓝染剂",
                "绿松蓝染剂",
                "魔花绿染剂",
                "----------------",
                "寒冰蓝染剂",
                "天空蓝染剂",
                "海雾蓝染剂",
                "孔雀蓝染剂",
                "罗海蓝染剂",
                "腐尸蓝染剂",
                "青磷蓝染剂",
                "靛青蓝染剂",
                "油墨蓝染剂",
                "盗龙蓝染剂",
                "东洲蓝染剂",
                "风暴蓝染剂",
                "虚空蓝染剂",
                "皇室蓝染剂",
                "午夜蓝染剂",
                "阴影蓝染剂",
                "深渊蓝染剂",
                "龙骑蓝染剂",
                "松石蓝染剂",
                "----------------",
                "薰衣草染剂",
                "忧郁紫染剂",
                "醋栗紫染剂",
                "鸢尾紫染剂",
                "葡萄紫染剂",
                "莲花粉染剂",
                "蜂鸟粉染剂",
                "仙子梅染剂",
                "帝王紫染剂",
                "丝雀黄染剂",
                "香草黄染剂",
                "----------------",
                "无瑕白染剂",
                "煤玉黑染剂",
                "柔彩粉染剂",
                "黑暗红染剂",
                "黑暗棕染剂",
                "柔彩绿染剂",
                "黑暗绿染剂",
                "柔彩蓝染剂",
                "黑暗蓝染剂",
                "柔彩紫染剂",
                "黑暗紫染剂",
                "----------------",
                "闪耀银染剂",
                "闪耀金染剂",
                "金属红染剂",
                "金属橙染剂",
                "金属黄染剂",
                "金属绿染剂",
                "金属蓝染剂",
                "金属靛染剂",
                "金属紫染剂",
                "炮铜黑染剂",
                "珍珠白染剂",
                "金属铜染剂"
            };


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
            // Assuming each integer is stored as 16 bits (2 bytes)
            for (int i = 0; i < binaryData.Length; i += 16)
            {
                if (i + 16 <= binaryData.Length)
                {
                    string byteString = binaryData.Substring(i, 16);
                    int index = Convert.ToInt32(byteString, 2); // Convert binary to integer
                    indices.Add(index);
                }
            }

            return indices;
        }

        //significantly optimized performance by locking pixels. However image will have to be 32bpp or there will be an error
        //32bpp is ensured in createCardView functions
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


        private Bitmap convertBitmapImagetoBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a PNG bitmap encoder and save the BitmapImage to the stream
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);

                // Convert the stream to a Bitmap
                return new Bitmap(stream);
            }
        }

        

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {


            var image = sender as System.Windows.Controls.Image;

            if (image != null)
            {
                Debug.WriteLine("Image clicked");

                // Check if the source is BitmapImage
                if (image.Source is BitmapImage bitmapImage)
                {
                    // Convert BitmapImage to Bitmap
                    Bitmap imageItem = convertBitmapImagetoBitmap(bitmapImage);

                    // Check if imageItem is null
                    if (imageItem == null)
                    {
                        Debug.WriteLine("imageItem bitmap is null");
                        return;
                    }

                    Debug.WriteLine("imageItem bitmap is not null");


                    LockerViewModel lockerViewModel = (LockerViewModel)DataContext;
                    if (lockerViewModel == null)
                    {
                        Debug.WriteLine("Cannot get LockerViewModel reference");
                        return;
                    }

                    lockerViewModel.NavigateToDecrypt(bitmapImage);


                    // Call your method to extract indices
                    //var listOfIndices = ExtractIndicesFromImage_Optimized(imageItem);
                    //Debug.WriteLine("List of indices: " + string.Join(", ", listOfIndices));
                }
                else
                {
                    Debug.WriteLine("Image source is not a BitmapImage.");
                }
                Debug.WriteLine("End execution");
            }

            



        }


        

    }
}
/* 10/11/2024
discarded version
lock to SIGNIFICANTLY increase process speed
however this method assumes 32bpp and doesn't work in other pixel format

 */