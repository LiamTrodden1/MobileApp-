using AlbumApp.ViewModels;
namespace AlbumApp.Views;

public partial class CreateAccount : ContentPage
{
	public CreateAccount(CreateAccountViewModel createAccountViewModel)
    {
		InitializeComponent();
        BindingContext = createAccountViewModel;
    }
}