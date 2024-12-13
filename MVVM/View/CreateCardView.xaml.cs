

using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using Private_Ethercloset.MVVM.Model;
using System.Windows.Media.Imaging;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;
using SteganographyLibrary;
using ImageSharpImage = SixLabors.ImageSharp.Image;
using Xunit;
using SixLabors.ImageSharp;
using Microsoft.SqlServer.Server;


namespace Private_Ethercloset.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateCardView.xaml
    /// </summary>
    public partial class CreateCardView : UserControl
    {
        private Card card;
        private DatabaseHelper databaseHelper = new DatabaseHelper();


        public CreateCardView()
        {
            InitializeComponent();
            BindDyesToComboBox();
        }

        private void BindDyesToComboBox()
        {
            List<string> dyes = databaseHelper.getDyes();

            WeaponDye1.ItemsSource = dyes;
            WeaponDye2.ItemsSource = dyes;
            HeadDye1.ItemsSource = dyes;
            HeadDye2.ItemsSource = dyes;
            ChestDye1.ItemsSource = dyes;
            ChestDye2.ItemsSource = dyes;
            HandDye1.ItemsSource = dyes;
            HandDye2.ItemsSource = dyes;
            LegDye1.ItemsSource = dyes;
            LegDye2.ItemsSource = dyes;
            FootDye1.ItemsSource = dyes;
            FootDye2.ItemsSource = dyes;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = DirectoryManager.ImportPicture();
            Assert.NotNull(imagePath);

            card = new Card(imagePath);

            ImageDisplay.Source = card.getImage();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (card.hasImage())
            {
                updateAllEquipments();
                encryptAndSaveImage();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("请上传一张图片", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void encryptAndSaveImage()
        {
            SteganoCard steganoCard = new SteganoCard(card);

            steganoCard.encrypt();
            string outputImagePath = getLockerPath();
            steganoCard.save(outputImagePath);
        }

        private string getLockerPath()
        {
            return Path.Combine(DirectoryManager.getNewLocker(), "front.png");
        }

        private void updateAllEquipments()
        {
            updateWeapon();
            updateHead();
            updtaeChest();
            updateHand();
            updateLeg();
            updateFoot();
            updateEar();
            updateNeck();
            updateBracelet();
            updateRing1();
            updateRing2();
        }

        private void updateRing2()
        {
            card.changeAccessoryTo(GearType.RING2, Ring2Entry.Text);
        }

        private void updateRing1()
        {
            card.changeAccessoryTo(GearType.RING1, Ring1Entry.Text);
        }

        private void updateBracelet()
        {
            card.changeAccessoryTo(GearType.BRACELET, BraceletEntry.Text);
        }

        private void updateNeck()
        {
            card.changeAccessoryTo(GearType.NECK, NeckEntry.Text);
        }

        private void updateEar()
        {
            card.changeAccessoryTo(GearType.EAR, EarEntry.Text);
        }

        private void updateFoot()
        {
            card.changeBodyEquipmentTo(GearType.FOOT, WeaponEntry.Text, FootDye1.SelectedItem.ToString(), FootDye2.SelectedItem.ToString());
        }

        private void updateLeg()
        {
            card.changeBodyEquipmentTo(GearType.LEG, LegEntry.Text, LegDye1.SelectedItem.ToString(), LegDye2.SelectedItem.ToString());
        }

        private void updateHand()
        {
            card.changeBodyEquipmentTo(GearType.HAND, HandEntry.Text, HandDye1.SelectedItem.ToString(), HandDye2.SelectedItem.ToString());
        }

        private void updtaeChest()
        {
            card.changeBodyEquipmentTo(GearType.CHEST, ChestEntry.Text, ChestDye1.SelectedItem.ToString(), ChestDye2.SelectedItem.ToString());
        }

        private void updateHead()
        {
            card.changeBodyEquipmentTo(GearType.HEAD, HeadEntry.Text, HeadDye1.SelectedItem.ToString(), HeadDye2.SelectedItem.ToString());
        }

        private void updateWeapon()
        {
            card.changeBodyEquipmentTo(GearType.WEAPON, WeaponEntry.Text, WeaponDye1.SelectedItem.ToString(), WeaponDye2.SelectedItem.ToString());
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

                    command.Parameters.AddWithValue("@WeaponCategoryLow", (int)EquipmentCategory.WeaponLow);//

                    command.Parameters.AddWithValue("@WeaponCategoryHigh", (int)EquipmentCategory.WeaponHigh);//

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
            if (!string.IsNullOrWhiteSpace(iconPath) && iconPath.Length >= databaseHelper.getIconNameLength())
            {
                return Path.Combine(DirectoryManager.getIconsRootPath(), iconPath.Substring(iconPath.Length - 10));
            }

            //else return default icon path (礼物盒
            return Path.Combine(DirectoryManager.getIconsRootPath(), databaseHelper.getDefaultIcon());
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
                    command.Parameters.AddWithValue("@WeaponCategoryLow", (int)EquipmentCategory.WeaponLow);//
                    command.Parameters.AddWithValue("@WeaponCategoryHigh", (int)EquipmentCategory.WeaponHigh);//
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
            if (!string.IsNullOrWhiteSpace(iconPath) && iconPath.Length >= databaseHelper.getIconNameLength())
            {
                return Path.Combine(DirectoryManager.getIconsRootPath(), iconPath.Substring(iconPath.Length - 10));
            }

            //else return default icon path (礼物盒
            return Path.Combine(DirectoryManager.getIconsRootPath(), databaseHelper.getDefaultIcon());
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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Head);
                HeadSearchResults.ItemsSource = results;
                HeadSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Head);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Chest);
                ChestSearchResults.ItemsSource = results;
                ChestSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Chest);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Hand);
                HandSearchResults.ItemsSource = results;
                HandSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Hand);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Leg);
                LegSearchResults.ItemsSource = results;
                LegSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Leg);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Foot);
                FootSearchResults.ItemsSource = results;
                FootSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Foot);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Ear);
                EarSearchResults.ItemsSource = results;
                EarSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Ear);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Neck);
                NeckSearchResults.ItemsSource = results;
                NeckSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Neck);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Bracelet);
                BraceletSearchResults.ItemsSource = results;
                BraceletSearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Bracelet);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Ring);
                Ring1SearchResults.ItemsSource = results;
                Ring1SearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Ring);

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
                var results = PerformFuzzySearch(searchTerm, (int)EquipmentCategory.Ring);
                Ring2SearchResults.ItemsSource = results;
                Ring2SearchResults.IsDropDownOpen = true;

                //get icon
                var iconPath = GetIconPathFromDatabase(searchTerm, (int)EquipmentCategory.Ring);

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
