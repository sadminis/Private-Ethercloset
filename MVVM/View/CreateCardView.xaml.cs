using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Drawing;
using Private_Ethercloset.MVVM.Model;
using System.Windows.Media.Imaging;
using System;
using static MaterialDesignThemes.Wpf.Theme;
using System.Text;
using System.IO;
using System.Drawing.Imaging;


namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateCardView.xaml
    /// </summary>
    public partial class CreateCardView : UserControl
    {
        private BitmapImage? _image;

        public CreateCardView()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            // Create a list of items
            List<string> items = new List<string>
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

            // Set the ComboBox's ItemsSource to the list of items
            WeaponDye1.ItemsSource = items;
            WeaponDye2.ItemsSource = items;
            HeadDye1.ItemsSource = items;
            HeadDye2.ItemsSource = items;
            ChestDye1.ItemsSource= items;
            ChestDye2.ItemsSource = items;
            HandDye1.ItemsSource = items;
            HandDye2.ItemsSource = items;
            LegDye1.ItemsSource = items;
            LegDye2.ItemsSource = items;
            FootDye1.ItemsSource = items;
            FootDye2.ItemsSource = items;
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
                _image = bitmap;
            }
            
        }

        private static string ConvertIndicesToBinary(List<int> indices)
        {
            StringBuilder binaryData = new StringBuilder();
            foreach (int index in indices)
            {
                // Convert each integer to its binary representation (16 bits per integer)
                binaryData.Append(Convert.ToString(index, 2).PadLeft(16, '0'));
            }
            return binaryData.ToString();
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_image != null) 
            {
                List<int> indices = new List<int>();
                indices.Add(0);
                indices.Add(1);
                indices.Add(2);
                indices.Add(3);

                Bitmap bitmap = convertBitmapImagetoBitmap(_image);


                string binaryData = ConvertIndicesToBinary(indices);

                if (binaryData.Length > bitmap.Width * bitmap.Height)
                {
                    throw new Exception("Data is too large to be embedded in the image.");
                }

                // Embed the binary data into the image
                int bitIndex = 0;
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Get pixel color
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Modify the LSB of the red channel to store data
                        if (bitIndex < binaryData.Length)
                        {
                            // Change the LSB of the red channel
                            int red = pixelColor.R & 0xFE; // Clear the last bit
                            if (binaryData[bitIndex] == '1')
                            {
                                red |= 0x01; // Set the last bit to 1
                            }
                            pixelColor = Color.FromArgb(pixelColor.A, red, pixelColor.G, pixelColor.B);
                            bitmap.SetPixel(x, y, pixelColor);
                            bitIndex++;
                        }
                    }
                }

                //string outputImagePath = DirectoryManager.getNewLocker();
                string outputImagePath = Path.Combine(DirectoryManager.getNewLocker(), "front.png");

                // Debugging: Check bitmap dimensions
                MessageBox.Show($"Bitmap Width: {bitmap.Width}, Height: {bitmap.Height}", "Bitmap Info", MessageBoxButton.OK);

                MessageBox.Show($"Output image path: {outputImagePath}", "Path Info", MessageBoxButton.OK);

                try
                {
                    // Save the modified image as a PNG file
                    bitmap.Save(outputImagePath, ImageFormat.Png);
                    MessageBox.Show($"Image saved successfully to {outputImagePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("请上传一张图片", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveTestImage()
        {
            string testOutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "testImage.png");
            using (Bitmap testBitmap = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(testBitmap))
                {
                    g.Clear(Color.Red); // Fill with a solid color for testing
                }
                try
                {
                    testBitmap.Save(testOutputPath, ImageFormat.Png);
                    MessageBox.Show($"Test image saved successfully to {testOutputPath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving test image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
