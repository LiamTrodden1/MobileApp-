using AlbumApp.ViewModels;

namespace AlbumApp.Views;

public partial class AppPreferences : ContentPage
{
	public AppPreferences(AppPreferencesViewModel appPreferences)
	{
		InitializeComponent();
		BindingContext = appPreferences;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AppPreferencesViewModel viewModel)
        {
            viewModel.LoadPreferences();
        }

    }
}