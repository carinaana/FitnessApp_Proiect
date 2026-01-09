using FitnessMobile.Models;

namespace FitnessMobile;

public partial class WorkoutsPage : ContentPage
{
    public WorkoutsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        WorkoutsList.ItemsSource = await App.Service.GetWorkoutTypes();
    }

    private async void OnFilterChanged(object sender, EventArgs e)
    {
        string selected = DiffPicker.SelectedItem as string;
        WorkoutsList.ItemsSource = await App.Service.GetWorkoutTypes(selected == "All" ? null : selected);
    }

    // EVENIMENTUL NOU DE CLICK (TAP)
    private async void OnWorkoutTapped(object sender, TappedEventArgs e)
    {
        var workout = e.Parameter as WorkoutType;
        if (workout == null) return;

        // Efect vizual
        var frame = sender as VisualElement;
        await frame.FadeTo(0.5, 100);
        await frame.FadeTo(1.0, 100);

        // Navigare catre sesiunile specifice acestui sport
        await Navigation.PushAsync(new SessionsPage(workout));
    }
}