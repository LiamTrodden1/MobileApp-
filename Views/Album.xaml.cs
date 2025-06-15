using AlbumApp.ViewModels;

namespace AlbumApp.Views;

public partial class Album : ContentPage
{
	public Album(AlbumViewModel albumViewmodel)
	{
		InitializeComponent();
		BindingContext = albumViewmodel;
	}
}