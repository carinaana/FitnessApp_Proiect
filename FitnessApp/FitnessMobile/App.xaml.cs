using FitnessMobile.Services;

namespace FitnessMobile
{
    public partial class App : Application
    {
        // Aici salvam emailul utilizatorului logat
        public static string CurrentUserEmail { get; set; }

        // Serviciul accesibil din toata aplicatia
        public static RestService Service { get; private set; }

        public App()
        {
            InitializeComponent();
            Service = new RestService();

            // Aplicatia porneste cu pagina de Login
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
