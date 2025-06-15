using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Services
{
    public class SpotifyService
    {
        //define credentials and client for auth token POST request
        string ClientID = "56817441eb124e2e93fca1f922cad2e6";
        string ClientSecret = "330cc589a43b47599aeccc1927ae33bb";
        private readonly HttpClient _httpClient = new();

        //https://developer.spotify.com/documentation/web-api/tutorials/client-credentials-flow
        //make POST request for access token
        public async Task<string> GetAccessTokenAsync()
        {
            //make a client instance, get url for request
            var client = new HttpClient();
            var tokenUrl = "https://accounts.spotify.com/api/token";

            //combine ClientID and ClientSecret for correct auth header format, post as basic authorisation, return JSON response 
            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientID}:{ClientSecret}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // make body of POST request to get access token from client credentials
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            //send Post and get response as a string
            var response = await client.PostAsync(tokenUrl, body);
            var responseString = await response.Content.ReadAsStringAsync();

            //If response was ok get access token and return as token
            if (response.IsSuccessStatusCode)
            {
                var token = JObject.Parse(responseString)["access_token"].ToString();
                return token;
            }

            //else error message and status code returned
            else
            {
                // Optional: handle errors
                throw new Exception($"Couldnt get access token: {response.StatusCode}\n{responseString}");
            }
        }

        //Find an album and return info
        public async Task<List<string>> SearchAlbumAsync(string token, string albumName)
        {
            //define result list
            List<string> result = new List<string>();

            //submit token as bearer for authenticatio of request to the search url
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(albumName)}&type=album&limit=1";

            //send get request and check if valid
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            //read response as string
            var jsonString = await response.Content.ReadAsStringAsync();

            //change response format to extract individual values from it 
            JObject jsonResponse = JObject.Parse(jsonString);

            //extract title, artitst, release date album link and image url
            var albums = jsonResponse["albums"]?["items"]?.ToList();
            //if a result is returned
            if (albums != null)
            {
                //get title, artitst, release date album link and image url and add to result List
                foreach (var album in albums)
                {
                    string albumTitle = album["name"]?.ToString();
                    string totalTracks = album["total_tracks"]?.ToString();
                    string artistName = album["artists"]?.First?["name"]?.ToString();
                    string releaseDate = album["release_date"]?.ToString();
                    string type = album["type"]?.ToString();
                    string albumImageUrl = album["images"]?.First?["url"]?.ToString();

                    result.Add(albumTitle);
                    result.Add(totalTracks);
                    result.Add(artistName);
                    result.Add(releaseDate);
                    result.Add(type);
                    result.Add(albumImageUrl);
                }
            }
            else
            {
                result.Add("Error getting album");
            }

            return result;
        }
    }


}
