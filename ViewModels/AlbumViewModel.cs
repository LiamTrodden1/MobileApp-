using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AlbumApp.Models;

namespace AlbumApp.ViewModels
{
    public partial class AlbumViewModel : ObservableObject
    {
        private SpotifyService _spotifyService;
        private readonly AlbumDatabaseService _albumDatabaseService;

        public string albumTitle;
        public int totalTracks;
        public string artistName;
        public string releaseDate;
        public string type;

        public AlbumViewModel()
        {
            //spotify service and database service 
            _spotifyService = new SpotifyService();
            _ = GetAccessTokenAsync();
            _albumDatabaseService = new AlbumDatabaseService();
        }

        [ObservableProperty]
        public string albumName;

        [ObservableProperty]
        public string accessToken;

        [ObservableProperty]
        public string albumResponse;

        [ObservableProperty]
        public string albumImageURL;

        [ObservableProperty]
        public string albumStoredAlert;

        //get spotify access token and store
        private async Task GetAccessTokenAsync()
        {
            try
            {
                //request access token 
                AccessToken = await _spotifyService.GetAccessTokenAsync();
            }

            catch (Exception ex)
            {
                //failed to get access token
                Console.WriteLine($"Failed to get token: {ex.Message}");
            }
        }
        [RelayCommand]
        private async Task SubmitAlbumName()
        {
            //check album has been entered
            if (string.IsNullOrWhiteSpace(AlbumName))
            {
                AlbumResponse = "Enter a valid album name";
                return;
            }
            //check token is valid
            if (string.IsNullOrEmpty(AccessToken))
            {
                AlbumResponse = "Token not retrieved";
                return;
            }

                try
                {
                //request album data
                    var result = await _spotifyService.SearchAlbumAsync(AccessToken, AlbumName);

                    //Get the relevant data
                    albumTitle = result[0];
                    totalTracks = int.Parse(result[1]);
                    artistName = result[2];
                    releaseDate = result[3];
                    type = result[4];
                    var albumImageUrl = result[5];

                    //Convert for output 
                    AlbumResponse = $"Album Name:  {albumTitle}{Environment.NewLine}" +
                                    $"Total Tracks:  {totalTracks}{Environment.NewLine}" +
                                    $"Artist Name:  {artistName}{Environment.NewLine}" +
                                    $"Release Date:  {releaseDate}{Environment.NewLine}" +
                                    $"Type:  {type}";
                    //get image
                    AlbumImageURL = albumImageUrl;
                }
                catch (Exception ex)
                {
                    AlbumResponse = $"Error: {ex.Message}";
                }
        }

        //save album to database
        [RelayCommand]
        public async Task SubmitDatabase()
        {
            //check input isnt empty
            if (string.IsNullOrEmpty(AlbumResponse))
            {
                AlbumStoredAlert = "Please load an album first.";
                return;
            }

            try
            {

                //store the instance of the album
                var album = new AlbumManagementModel
                {
                    albumTitle = albumTitle,
                    totalTracks = totalTracks,
                    artistName = artistName,
                    releaseDate = releaseDate,
                    type = type,
                    albumImageURL = AlbumImageURL,
                    count = 1
                };

                // Check if the album already exists in the database
                var existingAlbum = await _albumDatabaseService.GetItemByTitleAsync(album.albumTitle);

                //if it exists update the count instead and notify
                if (existingAlbum != null)
                {
                    existingAlbum.count++;
                    await _albumDatabaseService.UpdateItemAsync(existingAlbum);
                    AlbumStoredAlert = "Album saved to database";
                }

                else
                {
                    //save to database and notify saved
                    await _albumDatabaseService.SaveItemAsync(album);
                    AlbumStoredAlert = "Album saved to database";
                }
            }

            //else notify failed to save
            catch (Exception ex)
            {
                AlbumStoredAlert = "Failed to save album";
            }
        }
    }
}
