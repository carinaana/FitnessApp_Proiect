namespace FitnessMobile;
using FitnessMobile.Models;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginDto = new LoginDto { Email = EmailEntry.Text, Password = PasswordEntry.Text };
        string result = await App.Service.Login(loginDto);

        if (result == "OK")
        {
            App.CurrentUserEmail = EmailEntry.Text; // Tinem minte cine e
            // Schimbam pagina principala cu TabBar-ul aplicatiei
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Email sau parola gresite!", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
