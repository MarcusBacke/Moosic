using Moosic.Core;
using System;

namespace Moosic.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand PlayerViewCommand { get; set; }
        public RelayCommand SpotifyViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }

        public SpotifyViewModel SpotifyVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public PlayerViewModel PlayerVM { get; set; }
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
            SettingsVM = new SettingsViewModel();
            PlayerVM = new PlayerViewModel();
            CurrentView = PlayerVM;
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            PlayerViewCommand = new RelayCommand(o => { CurrentView = PlayerVM; });
            SpotifyViewCommand = new RelayCommand(o => { CurrentView = SpotifyVM; });
            SettingsViewCommand= new RelayCommand(o => { CurrentView= SettingsVM; });
        }
    }
}
