using AlbumApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AlbumApp.ViewModels
{
    public partial class AppPreferencesViewModel : ObservableObject
    {
        private readonly AlbumDatabaseService _albumDatabaseService;
        public AppPreferencesViewModel(AlbumDatabaseService albumDatabaseService)
        {
            _albumDatabaseService = albumDatabaseService;
        }

        [ObservableProperty]
        public string favAlbumName;

        [ObservableProperty]
        public string favAlbum;

        [ObservableProperty]
        public string favImageURL;

        [RelayCommand]
        public async Task SubmitFavAlbumName()
        {
            // get album by itsa title
            var album = await _albumDatabaseService.GetItemByTitleAsync(FavAlbumName);

            //check if album can be found
            if (album != null)
            {
                //show album and its title
                FavAlbum = album.albumTitle;
                FavImageURL = album.albumImageURL;

                //store in preferences so it saves for next time
                Preferences.Set("FavouriteAlbumTitle", album.albumTitle);
                Preferences.Set("FavouriteAlbumImageURL", album.albumImageURL);

            }
            //if album cant be found in the database
            else
            {
                FavAlbum = "Album not found."; 
                FavImageURL = "";
            }
        }

        public void LoadPreferences()
        {
            FavAlbum = Preferences.Get("FavouriteAlbumTitle", string.Empty);
            FavImageURL = Preferences.Get("FavouriteAlbumImageURL", string.Empty);
        }
    }
}
