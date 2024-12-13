using SixLabors.ImageSharp.PixelFormats;
using SteganographyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Xunit;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.Model
{
    public class SteganoCard
    {
        private Card _card;
        private SteganographicImage _image;
        private string _imagePath;

        public SteganoCard(string imagePath)
        {
            _imagePath = imagePath;
            _card = new Card(imagePath);
            _image = new SteganographicImage(Image.Load<Rgba32>(_imagePath));
        }

        public SteganoCard(Card card)
        {
            _card = card;
            _imagePath = _card.getImagePath();
            _image = new SteganographicImage(Image.Load<Rgba32>(_imagePath));
        }

        public BitmapImage getImageInCard()
        {
            return _card.getImage();
        }

        public void encrypt()
        {
            string encriptionMessage = getEncryptionMessageFromCard();
            byte[] encodedMessage = convertStringToBytes(encriptionMessage);

            var encodedImage = _image.EncodeDataInImage(encodedMessage);

            _image = new SteganographicImage(encodedImage);

            // make sure the message encripted = message decripted
            var decryptedMessage = _image.DecodedDataFromImage();
            Assert.Equal(encodedMessage, decryptedMessage);
        }

        private string convertBytesToString(byte[] message)
        {
            return Encoding.UTF8.GetString(message);
        }

        private byte[] convertStringToBytes(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }

        private string getEncryptionMessageFromCard()
        {
            List<int> equipToIDList = _card.getEncriptionMessage();

            // return as string
            return string.Join(",", equipToIDList);
        }

        public void save(string outputImagePath)
        {
            try
            {
                // Save the modified image as a PNG file
                _image.Image.Save(outputImagePath);
                MessageBox.Show($"成功保存到衣柜", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string decrypt()
        {
            byte[] decodedMessage = _image.DecodedDataFromImage();
            string decryptedMessage = convertBytesToString(decodedMessage);

            return decryptedMessage;
        }

        public void loadWithMessage(string decryptedMessage)
        {
            string[] splittedMessage = decryptedMessage.Split(',');
            int[] IDs = splittedMessage.Select(int.Parse).ToArray();

            _card.setWeapon(IDs[0], IDs[1], IDs[2]);
            _card.setHead(IDs[3], IDs[4], IDs[5]);
            _card.setChest(IDs[6], IDs[7], IDs[8]);
            _card.setLeg(IDs[9], IDs[10], IDs[11]);
            _card.setFoot(IDs[12], IDs[13], IDs[14]);
            _card.setEar(IDs[15]);
            _card.setNeck(IDs[16]);
            _card.setBracelet(IDs[17]);
            _card.setRing1(IDs[18]);
            _card.setRing2(IDs[19]);
        }
    }
}
