using Private_Ethercloset.Core;
using Private_Ethercloset.MVVM.Model;
using Private_Ethercloset.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Private_Ethercloset.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        public LockerViewModel LockerVM { get; set; }
        public CreateCardViewModel CreateCardVM { get; set; }
        public DecryptCardViewModel DecryptCardVM { get; set; }



        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LockerViewCommand { get; set; }
        public RelayCommand CreateCardViewCommand { get; set; }
        public RelayCommand DecryptCardViewCommand { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel() { 
            HomeVM = new HomeViewModel();
            LockerVM = new LockerViewModel(this);
            CreateCardVM = new CreateCardViewModel();
            DecryptCardVM = new DecryptCardViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            LockerViewCommand = new RelayCommand(o =>
            {
                LockerVM.LoadImagesFromDirectory();
                CurrentView = LockerVM;
            });

            CreateCardViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateCardVM;
            });

            DecryptCardViewCommand = new RelayCommand(o =>
            {
                CurrentView = DecryptCardVM;
            });



        }

        public void NavigateToDecrypt(SteganoCard steganoCard)
        {
            DecryptCardView decryptCardView = new DecryptCardView(steganoCard);
            CurrentView = decryptCardView;
        }



    }
}
