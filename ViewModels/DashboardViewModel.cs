using System.Collections.ObjectModel;
using AlbumApp.Models;
using AlbumApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace AlbumApp.ViewModels;

public partial class DashboardViewModel : ObservableObject
{

    // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/carouselview/interaction?view=net-maui-9.0
    [ObservableProperty]
    private ObservableCollection<DashboardModel> albums;

    [ObservableProperty]
    public int totalCount;

    [ObservableProperty]
    public string mostListenedTitle;

    [ObservableProperty]
    public string mostListenedImage;

    private readonly FirebaseAuthClient _authClient;
    private readonly AlbumDatabaseService _albumDatabaseService;

    public DashboardViewModel(FirebaseAuthClient authClient)
    {
        _authClient = authClient;
        _albumDatabaseService = new AlbumDatabaseService();

        LoadMostRecentAlbums();
        TotalAlbums();
        MostListenedTo();
    }

    //Load the top 3 most recent albums 
    public async Task LoadMostRecentAlbums()
    {
        //make the get most recent 3 albums by ID requst
        var recentAlbums = await _albumDatabaseService.GetMostRecentAlbumsAsync();

        //initialise albums
        Albums = new ObservableCollection<DashboardModel>();

        //check if albums is null
        if (recentAlbums != null)
        {
            //if not null add each returned album carousel by the title and image
            foreach (var album in recentAlbums)
            {
                Albums.Add(new DashboardModel
                {
                    Name = album.albumTitle,
                    ImageURL = album.albumImageURL
                });
            }
        }
        // if empty
        else
        {
            Console.WriteLine("No Album to display");
        }
    }

    public async Task TotalAlbums()
    {
        try
        {
            //intialise a counter for total listend to albums and call all albums
            var items = await _albumDatabaseService.GetItemsAsync();
            TotalCount = 0;

            //for each retrieved album add the albums count to the total count
            foreach (var item in items)
            {
                TotalCount += item.count;
            }
        }
        catch
        {
            //no albums to count 
            Console.WriteLine("Error collecting Total listened to Albums");
        }

    }

    public async Task MostListenedTo()
    {
        try
        {
            //call all albums and inittialise the most listened to and the ID of it 
            var items = await _albumDatabaseService.GetItemsAsync();
            int mostListened = 0;
            int mostListenedID = 0;

            //loop through all albums and add save thealbum with the highest count aswell as its ID
            foreach (var item in items)
            {
                if (item.count > mostListened)
                {
                    mostListened = item.count;
                    mostListenedID = item.ID;
                }
            }

            //call the most listened to album by ID and display the title and image
            var mostListenedAlbum = await _albumDatabaseService.GetItemAsync(mostListenedID);
            MostListenedTitle = mostListenedAlbum.albumTitle;
            MostListenedImage = mostListenedAlbum.albumImageURL;
        }
        catch
        {
            Console.WriteLine("Error Collecting Most Listened to Album");
        }

    }

    [RelayCommand]
    //log out the user
    public async Task Logout()
    {
        try
        {
            //use a firebase built in sign out
            _authClient.SignOut();

            //remove authorisation token
            SecureStorage.Remove("AuthToken");

            //go back to login
            await Shell.Current.GoToAsync("//Login");
        }

        catch
        {
            Console.WriteLine("Error logging out");
        }
    }
}

