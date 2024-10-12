

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
using System.Data.SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;


namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateCardView.xaml
    /// </summary>
    public partial class CreateCardView : UserControl
    {
        private BitmapImage? _image;
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
        private const int CategoryWeapon_Low = 32; //weapons<=32 33=鱼饵
        private const int CategoryWeapon_High = 84;//>=84. 62=职业水晶。61=骑士武具（剑+盾）
        private const int CategoryHead = 34;
        private const int CategoryChest = 35;
        private const int CategoryHand = 37;
        private const int CategoryLeg = 36;
        private const int CategoryFoot = 38;
        private const int CategoryEar = 41;
        private const int CategoryNeck = 40;
        private const int CategoryBracelet = 42;
        private const int CategoryRing = 43;
        private const string DefaultIcon = "026107.png";
        private const int IconNameLength = 10; //6 digits + .png (4)
        private const int EncryptEnd = 0b1111111111111111;//65535


        public CreateCardView()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            // Create a list of items

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

        //private Bitmap convertBitmapImagetoBitmap(BitmapImage bitmapImage)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        // Create a PNG bitmap encoder and save the BitmapImage to the stream
        //        PngBitmapEncoder encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
        //        encoder.Save(stream);

        //        // Convert the stream to a Bitmap
        //        return new Bitmap(stream);
        //    }
        //}

        //convert bitmapimage to bitmap, ensuring pixel format = 32bpp
        //Is necessary to use the optimized decrypt function.
        private Bitmap convertBitmapImageToBitmap_32bpp(BitmapImage bitmapImage)
        {
            // Create a new Bitmap with the desired pixel format (32bpp ARGB)
            Bitmap bitmap32bpp = new Bitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bitmap32bpp))
            {
                // Draw the BitmapImage onto the Bitmap
                g.DrawImage(BitmapImageToBitmap(bitmapImage), new Rectangle(0, 0, bitmap32bpp.Width, bitmap32bpp.Height));
            }

            return bitmap32bpp; // Return the new 32bpp bitmap
        }

        // Helper method to convert BitmapImage to Bitmap
        private Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a PNG bitmap encoder and save the BitmapImage to the stream
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin); // Reset the stream position for reading

                // Convert the stream to a Bitmap (but discard the result)
                return new Bitmap(stream);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_image != null) 
            {
                DatabaseHelper databaseHelper = new DatabaseHelper();
                List<int> indices = new List<int>();
                indices.Add(databaseHelper.GetItemIdByName(WeaponEntry.Text));
                indices.Add(items.IndexOf(WeaponDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(WeaponDye2.SelectedItem.ToString()));
                
                indices.Add(databaseHelper.GetItemIdByName(HeadEntry.Text));
                indices.Add(items.IndexOf(HeadDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(HeadDye2.SelectedItem.ToString()));

                indices.Add(databaseHelper.GetItemIdByName(ChestEntry.Text));
                indices.Add(items.IndexOf(ChestDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(ChestDye2.SelectedItem.ToString()));

                indices.Add(databaseHelper.GetItemIdByName(HandEntry.Text));
                indices.Add(items.IndexOf(HandDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(HandDye2.SelectedItem.ToString()));

                indices.Add(databaseHelper.GetItemIdByName(LegEntry.Text));
                indices.Add(items.IndexOf(LegDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(LegDye2.SelectedItem.ToString()));

                indices.Add(databaseHelper.GetItemIdByName(FootEntry.Text));
                indices.Add(items.IndexOf(FootDye1.SelectedItem.ToString()));
                indices.Add(items.IndexOf(FootDye2.SelectedItem.ToString()));

                indices.Add(databaseHelper.GetItemIdByName(EarEntry.Text));
                indices.Add(databaseHelper.GetItemIdByName(NeckEntry.Text));
                indices.Add(databaseHelper.GetItemIdByName(BraceletEntry.Text));
                //indices.Add(databaseHelper.GetItemIdByName(FootEntry.Text));
                indices.Add(databaseHelper.GetItemIdByName(Ring1Entry.Text));
                indices.Add(databaseHelper.GetItemIdByName(Ring2Entry.Text));

                indices.Add(EncryptEnd);

                //view debug text
                Debug.WriteLine("Save Button clicked: List of indices: " + string.Join(", ", indices));
                
                //
                Bitmap bitmap = convertBitmapImageToBitmap_32bpp(_image);


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


                try
                {
                    // Save the modified image as a PNG file
                    bitmap.Save(outputImagePath, ImageFormat.Png);
                    MessageBox.Show($"成功保存到衣柜", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        //fuzzy search in db w/ category
        private List<string> PerformFuzzySearch(string searchTerm, int itemCategory)
        {
            var results = new List<string>();
            //string query = "SELECT name FROM items WHERE name LIKE @searchTerm"; //no category version
            string query = "SELECT name FROM items WHERE name LIKE @searchTerm AND ItemUICategoryID = @itemCategory";
            using (var connection = new DatabaseHelper().GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    command.Parameters.AddWithValue("@itemCategory", itemCategory);//limit category

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            return results;
        }

        //fuzzy search weapons
        //weapon category range(10/10/2024): <=32 or >= 84. 
        private List<string> PerformFuzzySearchWeapon(string searchTerm)
        {
            var results = new List<string>();

            string query = "SELECT name FROM items WHERE name LIKE @searchTerm AND" +
                " (ItemUICategoryID <= @WeaponCategoryLow OR ItemUICategoryID >= @WeaponCategoryHigh)";

            using (var connection = new DatabaseHelper().GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    command.Parameters.AddWithValue("@WeaponCategoryLow", CategoryWeapon_Low);//

                    command.Parameters.AddWithValue("@WeaponCategoryHigh", CategoryWeapon_High);//

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            return results;
        }

        //get icon path from database
        private string GetIconPathFromDatabase(string itemName, int itemCategory)
        {
            string iconPath = null;

            // change icon during fuzzy search. 
            string query = "SELECT Icon FROM items WHERE name LIKE @itemName AND ItemUICategoryID = @itemCategory LIMIT 1";  

            using (var connection = new DatabaseHelper().GetConnection())
            {
                connection.Open();

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@itemName", "" + itemName + "");
                    command.Parameters.AddWithValue("@itemCategory", itemCategory);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            iconPath = reader["Icon"].ToString();  
                        }
                    }
                }
            }

            //if found, adjust to actual path. The last 10 chars should be the actual icon name
            if (!string.IsNullOrWhiteSpace(iconPath) && iconPath.Length >= IconNameLength)
            {
                return Path.Combine(DirectoryManager.getIconsRootPath(), iconPath.Substring(iconPath.Length - 10));
            }

            //else return default icon path (礼物盒
            return Path.Combine(DirectoryManager.getIconsRootPath(), DefaultIcon);
        }

        //get icon path from database
        private string GetWeaponIconPathFromDatabase(string itemName)
        {
            string iconPath = null;

            // change icon during fuzzy search. weapon version.
            string query = "SELECT name, Icon FROM items WHERE name LIKE @itemName AND " +
                "(ItemUICategoryID <= @WeaponCategoryLow OR ItemUICategoryID >= @WeaponCategoryHigh) LIMIT 1";


            using (var connection = new DatabaseHelper().GetConnection())
            {
                connection.Open();

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@itemName", "" + itemName + "");
                    command.Parameters.AddWithValue("@WeaponCategoryLow", CategoryWeapon_Low);//
                    command.Parameters.AddWithValue("@WeaponCategoryHigh", CategoryWeapon_High);//
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            iconPath = reader["Icon"].ToString();
                        }
                    }
                }
            }

            //if found, adjust to actual path. The last 10 chars should be the actual icon name
            if (!string.IsNullOrWhiteSpace(iconPath) && iconPath.Length >= IconNameLength)
            {
                return Path.Combine(DirectoryManager.getIconsRootPath(), iconPath.Substring(iconPath.Length - 10));
            }

            //else return default icon path (礼物盒
            return Path.Combine(DirectoryManager.getIconsRootPath(), DefaultIcon);
        }

        private void WeaponSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (WeaponSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = WeaponSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                WeaponEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                WeaponSearchResults.IsDropDownOpen = false;
            }
        }

        private void OnWeaponEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = WeaponEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //var results = PerformFuzzySearch(searchTerm);
                var results = PerformFuzzySearchWeapon(searchTerm);
                WeaponSearchResults.ItemsSource = results;
                WeaponSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetWeaponIconPathFromDatabase(searchTerm);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    WeaponIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                WeaponSearchResults.ItemsSource = null;
                WeaponSearchResults.IsDropDownOpen = false;
            }
        }

        
        private void HeadSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (HeadSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = HeadSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                HeadEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                HeadSearchResults.IsDropDownOpen = false;
            }
        }

        private void HeadEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = HeadEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryHead);
                HeadSearchResults.ItemsSource = results;
                HeadSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryHead);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    HeadIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                HeadSearchResults.ItemsSource = null;
                HeadSearchResults.IsDropDownOpen = false;
            }
        }

        private void ChestSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (ChestSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = ChestSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                ChestEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                ChestSearchResults.IsDropDownOpen = false;
            }
        }

        private void ChestEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = ChestEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryChest);
                ChestSearchResults.ItemsSource = results;
                ChestSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryChest);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    ChestIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                ChestSearchResults.ItemsSource = null;
                ChestSearchResults.IsDropDownOpen = false;
            }
        }

        private void HandSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (HandSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = HandSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                HandEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                HandSearchResults.IsDropDownOpen = false;
            }
        }

        private void HandEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = HandEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryHand);
                HandSearchResults.ItemsSource = results;
                HandSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryHand);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    HandIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                HandSearchResults.ItemsSource = null;
                HandSearchResults.IsDropDownOpen = false;
            }
        }

        private void LegSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (LegSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = LegSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                LegEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                LegSearchResults.IsDropDownOpen = false;
            }
        }

        private void LegEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = LegEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryLeg);
                LegSearchResults.ItemsSource = results;
                LegSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryLeg);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    LegIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                LegSearchResults.ItemsSource = null;
                LegSearchResults.IsDropDownOpen = false;
            }
        }

        private void FootSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (FootSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = FootSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                FootEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                FootSearchResults.IsDropDownOpen = false;
            }
        }

        private void FootEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = FootEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryFoot);
                FootSearchResults.ItemsSource = results;
                FootSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryFoot);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    FootIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                FootSearchResults.ItemsSource = null;
                FootSearchResults.IsDropDownOpen = false;
            }
        }

        private void EarSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (EarSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = EarSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                EarEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                EarSearchResults.IsDropDownOpen = false;
            }
        }

        private void EarEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = EarEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryEar);
                EarSearchResults.ItemsSource = results;
                EarSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryEar);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    EarIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                EarSearchResults.ItemsSource = null;
                EarSearchResults.IsDropDownOpen = false;
            }
        }

        private void NeckSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (NeckSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = NeckSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                NeckEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                NeckSearchResults.IsDropDownOpen = false;
            }
        }

        private void NeckEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = NeckEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryNeck);
                NeckSearchResults.ItemsSource = results;
                NeckSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryNeck);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    NeckIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                NeckSearchResults.ItemsSource = null;
                NeckSearchResults.IsDropDownOpen = false;
            }
        }

        private void BraceletSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (BraceletSearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = BraceletSearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                BraceletEntry.Text = selectedItem;

                // Optionally close the dropdown after selection
                BraceletSearchResults.IsDropDownOpen = false;
            }
        }

        private void BraceletEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = BraceletEntry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryBracelet);
                BraceletSearchResults.ItemsSource = results;
                BraceletSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryBracelet);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    BraceletIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                BraceletSearchResults.ItemsSource = null;
                BraceletSearchResults.IsDropDownOpen = false;
            }
        }

        private void Ring1SearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (Ring1SearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = Ring1SearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                Ring1Entry.Text = selectedItem;

                // Optionally close the dropdown after selection
                Ring1SearchResults.IsDropDownOpen = false;
            }
        }

        private void Ring1Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = Ring1Entry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryRing);
                Ring1SearchResults.ItemsSource = results;
                Ring1SearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryRing);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    Ring1Icon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                Ring1SearchResults.ItemsSource = null;
                Ring1SearchResults.IsDropDownOpen = false;
            }
        }

        private void Ring2SearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the ComboBox has a selected item
            if (Ring2SearchResults.SelectedItem != null)
            {
                // Get the selected item (ensure it's of type string)
                string selectedItem = Ring2SearchResults.SelectedItem.ToString();

                // Set the text of the WeaponEntry TextBox to the selected item
                Ring2Entry.Text = selectedItem;

                // Optionally close the dropdown after selection
                Ring2SearchResults.IsDropDownOpen = false;
            }
        }

        private void Ring2Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = Ring2Entry.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var results = PerformFuzzySearch(searchTerm, CategoryRing);
                Ring2SearchResults.ItemsSource = results;
                Ring2SearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, CategoryRing);

                if (!string.IsNullOrEmpty(iconPath))
                {
                    Ring2Icon.Source = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
                }
                //else do nothing -- GetIconPathFromDatabase will generate a default when name is invalid
            }
            else
            {
                Ring2SearchResults.ItemsSource = null;
                Ring2SearchResults.IsDropDownOpen = false;
            }
        }
    }
}
