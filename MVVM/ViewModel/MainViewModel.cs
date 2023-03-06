using Moosic.Core;
using System;

namespace Moosic.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;
        public ObservableCollection<Item> Songs { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SpotifyViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }

        public SpotifyViewModel SpotifyVM { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; 
                OnPropertyChanged(); 
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            SpotifyVM = new SpotifyViewModel();
            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            SpotifyViewCommand = new RelayCommand(o => { CurrentView = SpotifyVM; });
        }
    }
}
