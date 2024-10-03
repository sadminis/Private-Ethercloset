using Private_Ethercloset.Core;
using Private_Ethercloset.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Private_Ethercloset.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        public LockerViewModel LockerVM { get; set; }
        public CreateCardViewModel CreateCardVM { get; set; }



        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LockerViewCommand { get; set; }
        public RelayCommand CreateCardViewCommand { get; set; }

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
            LockerVM = new LockerViewModel();
            CreateCardVM = new CreateCardViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            LockerViewCommand = new RelayCommand(o =>
            {
                CurrentView = LockerVM;
            });

            CreateCardViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateCardVM;
            });
        }
    }
}
