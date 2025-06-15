using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Firebase.Auth;
using System.Text;
using System.Text.RegularExpressions;

namespace AlbumApp.ViewModels;

public partial class CreateAccountViewModel : ObservableObject
{
    private readonly FirebaseAuthClient _authClient;

    public CreateAccountViewModel(FirebaseAuthClient authClient)
    {
        _authClient = authClient;
    }

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string invalidCreate;

    [ObservableProperty]
    private string confirmPassword;


    // check if email and password are in the correct format
    //https://christian-schou.com/blog/email-regex/?utm_source=chatgpt.com
    private bool RegEX()
    {
        //[any letter from a-z/A-Z any number and symbols._-/] @ [any letter from a-z/A-Z] . [[any letter from a-zA-Z and can repeat twice]
        //\ for . and - so they are used as characters instead
        //e.g. troddenl@coventry.ac.uk
        string ValidEmail = @"^[a-zA-Z0-9._\-/]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        //(at least 1 lowercase) + (at least 1 uppercase) + (atleast 1 digit) + (atleast 1 of the symbols) + minimum length of 8
        string ValidPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[./-_!@#$%^&*()+=<>?{}[\]|~`]).{8,}$";


        //check if strings aren't empty
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            return false;
        }
        else
        {
            //check if email and password comply with regEx
            bool isValidEmail = Regex.IsMatch(Email, ValidEmail);
            bool isValidPassword = Regex.IsMatch(Password, ValidPassword);

            // if email and password are valid return True else False
            if (isValidEmail == true && isValidPassword == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //compare if the new password is the same
    private bool SamePassword()
    {
        if (Password == ConfirmPassword)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [RelayCommand]
    private async Task SubmitDetails()
    {
        //call RegEx to check formats correct
        bool Valid = RegEX();

        //check passwords are the same
        bool Same = SamePassword();

        if (Valid == true && Same == true)
        {
            try
            {
                //if formats correct try creating account with Username and Password then go to Login
                await _authClient.CreateUserWithEmailAndPasswordAsync(Email, Password);
                await Shell.Current.GoToAsync("//Login");
            }
            catch
            {
                //If that didnt work message error creating account 
                InvalidCreate = "Error Creating Account";

                //clear email and password
                Email = "";
                Password = "";
            }
        }
        else
        {
            if (Same == false)
            {
                //else message format is incorrect
                InvalidCreate = "Passwords Do Not Match";
            }
            else
            {
                //else message format is incorrect
                InvalidCreate = "Invalid Format";

                //clear email and password
                Email = "";
                Password = "";
            }

        }
    }

    //when button pressed go back to login
    [RelayCommand]
    private async Task NavigateLogin()
    {
        await Shell.Current.GoToAsync("//Login");
    }
}

