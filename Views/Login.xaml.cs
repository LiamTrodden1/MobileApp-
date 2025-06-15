using AlbumApp.ViewModels;
using Firebase.Auth;

namespace AlbumApp.Views;

public partial class Login : ContentPage
{
    public Login(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }

    //call AutoLogin on app startup to automatically log in a user
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    AutoLogin();
    //}

    //Check for token to log user in straight away
    //private void AutoLogin() 
    //{
    //    var token = Task.Run(async () => await SecureStorage.GetAsync("AuthToken")).Result;

    //    if (!string.IsNullOrEmpty(token))
    //    {
            // Token exists, navigate to Dashboard
    //        Shell.Current.GoToAsync("//Dashboard");
    //    }
    //}
}