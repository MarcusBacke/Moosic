using Moosic.MVVM.ViewModel;

using Newtonsoft.Json;
using RestSharp;
using Moosic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Moosic.Models.SpotifySearch;
using System.Numerics;
using System.Windows;

namespace Moosic.Helpers
{


    //Search Helper, 
    public static class SearchHelper
    {
        public static Token token { get; set; }
        public static async Task GetTokenAsync()
        {
            #region SecretVault
            string clientID = "d6c62bd2908e49a68a29c204b9c716e5";

            string clientSecret = "7af3b3575a264224abfc9f22c380bbfb";
            #endregion

            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientID + ":" + clientSecret));

            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            HttpContent content = new FormUrlEncodedContent(args);

            HttpResponseMessage resp = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            string msg = await resp.Content.ReadAsStringAsync();

            token = JsonConvert.DeserializeObject<Token>(msg);
        }
  


        public static SpotifyResult SearchArtistOrSong(string searchWord)
        {
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {token.access_token}");
            var request = new RestRequest($"?q={searchWord}&type=artist", Method.Get);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<SpotifyResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }

        }
    
        public static async Task PlaySong(string songID)
        {
            var client = new RestClient("https://api.spotify.com/v1/player");
            client.AddDefaultHeader("Authorization", $"Bearer {token.access_token}");
            var request = new RestRequest("PUT");
            request.AddJsonBody(new { uris = new string[] { $"spotify:track:{songID}" } });
            var response = await client.ExecuteAsync(request);
        }
    }
    
}

