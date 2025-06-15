using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using AlbumApp.ViewModels;

namespace AlbumApp.Views;

public partial class Dashboard : ContentPage
{
    //Bind DashboardViewModel
    public Dashboard(DashboardViewModel dashboardViewModel)
    {
        InitializeComponent();
        BindingContext = dashboardViewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DashboardViewModel viewModel)
        {
            await viewModel.LoadMostRecentAlbums();
            await viewModel.TotalAlbums();
            await viewModel.MostListenedTo();
        }
    }
}
