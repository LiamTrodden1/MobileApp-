using AlbumApp.ViewModels;

namespace AlbumApp.Views;

public partial class AlbumManagement : ContentPage
{
	public AlbumManagement(AlbumManagementViewModel albumManagementViewModel)
	{
		InitializeComponent();
        BindingContext = albumManagementViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AlbumManagementViewModel albumManagementViewModel = (AlbumManagementViewModel)BindingContext;
        await albumManagementViewModel.LoadAlbums();

    }
}


