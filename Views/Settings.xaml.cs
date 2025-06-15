using AlbumApp.ViewModels;
namespace AlbumApp.Views;

public partial class Settings : ContentPage
{
	public Settings(SettingsViewModel settingsViewModel)
	{
        InitializeComponent();
        BindingContext = settingsViewModel;
    }
}
