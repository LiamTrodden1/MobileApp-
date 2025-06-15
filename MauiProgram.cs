using Microsoft.Extensions.Logging;
using AlbumApp.Views;
using AlbumApp.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using AlbumApp.Services;
using Microsoft.Maui.Storage;

namespace AlbumApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        //connect viewmodel and allow navigation to Dashboard
        builder.Services.AddSingleton<DashboardViewModel>();
        builder.Services.AddTransient<Dashboard>();

        //connect viewmodel and allow navigation to Login
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<Login>();

        //connect viewmodel and allow navigation to CreateAccount
        builder.Services.AddSingleton<CreateAccountViewModel>();
        builder.Services.AddTransient<CreateAccount>();

        //connect viewmodel and allow navigation to Settings
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<Settings>();

        //connect viewmodel and allow navigation to Album
        builder.Services.AddSingleton<AlbumViewModel>();
        builder.Services.AddTransient<Album>();

        //connect viewmodel and allow vanigation to AlbumManagement
        builder.Services.AddSingleton<AlbumManagementViewModel>();
        builder.Services.AddTransient<AlbumManagement>();

        //connect viewmodel and allow vanigation to AppPreferences
        builder.Services.AddSingleton<AppPreferencesViewModel>();
        builder.Services.AddTransient<AppPreferences>();

        //conect the authentication service
        builder.Services.AddSingleton<AuthenticationService>();

        //databse service
        builder.Services.AddSingleton<AlbumDatabaseService>();

        //https://www.youtube.com/watch?v=3DQMQ9Vuk0c
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyCmokn22b8UgdDShbNEBkKz61cDq-o_cuI",
            AuthDomain = "albumapp6002cem.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        }));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}