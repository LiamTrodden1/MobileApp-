using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Firebase.Auth;

namespace AlbumApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        public LoginViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string invalidLogin;

        [RelayCommand]
        private async Task SubmitDetails()
        {
            try
            {
                // try to log user in with email and password, store the token securely, go to dashboard
                //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/secure-storage?view=net-maui-9.0&tabs=windows
                var authResult = await _authClient.SignInWithEmailAndPasswordAsync(Email, Password);
                var token = await authResult.User.GetIdTokenAsync();
                await SecureStorage.SetAsync("AuthToken", token);
                await Shell.Current.GoToAsync("//Dashboard");
            }
            catch 
            {
                //display error message
                InvalidLogin = "Incorrect Details";
            }
            //clear email and password
            Email = "";
            Password = "";
        }

        //go to the create account page
        [RelayCommand]
        private async Task NavigateCreateAccount()
        {
            await Shell.Current.GoToAsync("//CreateAccount");
        }


    }
}