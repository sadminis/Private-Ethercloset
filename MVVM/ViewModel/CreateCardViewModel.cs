using Private_Ethercloset.MVVM.Model;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Private_Ethercloset.MVVM.ViewModel
{
    public class CreateCardViewModel : INotifyPropertyChanged
    {
        public string imageSource;
        private string DefaultImagePath;

        public CreateCardViewModel()
        {
            DefaultImagePath = Path.Combine(DirectoryManager.getResourceImagesDirectory(), "Placeholder.png");
            Debug.WriteLine(DefaultImagePath);
            imageSource = DefaultImagePath;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
