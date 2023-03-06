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
            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            PlayerViewCommand = new RelayCommand(o => { CurrentView = PlayerVM; });
            SpotifyViewCommand = new RelayCommand(o => { CurrentView = SpotifyVM; });
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        void PopulateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQAGLkN6rCU6w1vCsZDOwTNVGQSnZSDg0NFVTkJNz4tOQ4EHdYuwXTR8UeFuziJjAaTDAExgXxSbGAZsxx8Am5Q7KO0hdLe41GQUXVf4B2NzbXfbqVKTiMERgfmYdssKdfcYDmZmEPLWnMh3Q4MottW07TPB7OD7XdeyUhgTv7cexLY");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "2:32";
                Songs.Add(track);
            }

            SettingsViewCommand= new RelayCommand(o => { CurrentView= SettingsVM; });
        }
    }
}
