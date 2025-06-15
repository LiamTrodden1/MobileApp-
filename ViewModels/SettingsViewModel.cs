using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using AlbumApp.Services;
using CommunityToolkit.Mvvm.Input;
using static SQLite.SQLite3;

namespace AlbumApp.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string oldPassword;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string confirmPassword;

    [ObservableProperty]
    private string invalidChange;

    private readonly FirebaseAuthClient _authClient;
    private readonly LoginViewModel _loginViewModel;
    private readonly AuthenticationService _authenticationService;

    public SettingsViewModel(FirebaseAuthClient authClient, LoginViewModel loginViewModel, AuthenticationService authenticationService)
    {
        _authClient = authClient;
        _loginViewModel = loginViewModel;
        _authenticationService = authenticationService;
    }

    [RelayCommand]
    private async Task ChangePassword()
    {
        //get the API key to change password
        var AuthToken = await SecureStorage.GetAsync("AuthToken");
        //make password change request
        var result = await _authenticationService.UpdatePassword(AuthToken, Password);
        try
        {
            //check the new password wasnt mistyped
            if (Password == ConfirmPassword)
            {
                //call UpdatePassword Service
                await _authenticationService.UpdatePassword(AuthToken, Password);
                InvalidChange = "Password Changed";
            }
            else
            {
                //notify passwords don't match
                InvalidChange = "New Passwords Do Not Match";
            }
        }
        catch 
        {

            //notify an error occured
            InvalidChange = "Error changing password";
        }
    }
}