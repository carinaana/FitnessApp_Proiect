using FitnessMobile.Models;

namespace FitnessMobile;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // 1. Validari simple
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        // 2. Crearea obiectului DTO
        var registerDto = new RegisterDto
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Phone = PhoneEntry.Text,
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text
        };

        // 3. Apelarea API-ului
        bool isSuccess = await App.Service.Register(registerDto);

        if (isSuccess)
        {
            await DisplayAlert("Success", "Account created! You can now login.", "OK");
            // Ne intoarcem la pagina de Login
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Email might be taken.", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Ne intoarcem la Login fara sa facem nimic
        await Navigation.PopAsync();
    }
}
