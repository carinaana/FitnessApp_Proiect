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

    private async void OnWorkoutSelected(object sender, SelectionChangedEventArgs e)
    {
        var workout = e.CurrentSelection.FirstOrDefault() as WorkoutType;
        if (workout == null) return;

        // Mergem la Sesiuni (Orar)
        await Navigation.PushAsync(new SessionsPage(workout));
        WorkoutsList.SelectedItem = null;
    }
}
