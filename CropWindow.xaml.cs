using Microsoft.Win32;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = SixLabors.ImageSharp.Image;
using Point = System.Windows.Point;

namespace Private_Ethercloset
{
    /// <summary>
    /// Interaction logic for CropWindow.xaml
    /// </summary>
    public partial class CropWindow : Window
    {
        private Image<Rgba32> _image;
        private Point _startPoint;
        private Point _endPoint;
        private bool _isSelecting;


        public CropWindow(string filePath)
        {
            InitializeComponent();
            _image = Image.Load<Rgba32>(filePath);
            DisplayImage.Source = ImageToBitmapSource(_image);
        }

        private void DisplayImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _isSelecting = true;
                _startPoint = e.GetPosition(DisplayImage);
            }
        }

        private void DisplayImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelecting)
            {
                _endPoint = e.GetPosition(DisplayImage);
                // Optionally: Redraw selection rectangle here
            }
        }

        private void DisplayImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isSelecting = false;
        }

        private void CropButton_Click(object sender, RoutedEventArgs e)
        {
            if (_image != null)
            {
                // Calculate crop rectangle
                int x = (int)Math.Min(_startPoint.X, _endPoint.X);
                int y = (int)Math.Min(_startPoint.Y, _endPoint.Y);
                int width = (int)Math.Abs(_startPoint.X - _endPoint.X);
                int height = (int)Math.Abs(_startPoint.Y - _endPoint.Y);

                // Crop the image
                var croppedImage = _image.Clone(ctx => ctx.Crop(new SixLabors.ImageSharp.Rectangle(x, y, width, height)));
                //SaveCroppedImage(croppedImage);
            }
        }


        private BitmapSource ImageToBitmapSource(Image<Rgba32> image)
        {
            // Create a Bitmap from the ImageSharp image
            var bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Lock the bitmap to write pixels
            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                              System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                              bitmap.PixelFormat);

            // Create a byte array for pixel data
            var byteArray = new byte[image.Width * image.Height * 4]; // 4 bytes per pixel (R, G, B, A)

            // Populate the byte array with pixel data
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y]; // Access the pixel directly
                    int index = (y * image.Width + x) * 4; // Calculate byte index
                    byteArray[index] = pixel.R;     // Red
                    byteArray[index + 1] = pixel.G; // Green
                    byteArray[index + 2] = pixel.B; // Blue
                    byteArray[index + 3] = pixel.A; // Alpha
                }
            }

            // Copy the pixel data to the bitmap
            Marshal.Copy(byteArray, 0, bitmapData.Scan0, byteArray.Length);
            bitmap.UnlockBits(bitmapData);

            // Return the Bitmap as a BitmapSource for WPF
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
