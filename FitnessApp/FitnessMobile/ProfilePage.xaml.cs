namespace FitnessMobile;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        UserEmailLabel.Text = App.CurrentUserEmail;
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        App.CurrentUserEmail = null;

        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}