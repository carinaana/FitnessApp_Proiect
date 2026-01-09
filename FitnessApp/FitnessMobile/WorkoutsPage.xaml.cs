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
        var picker = sender as Picker;
        string selected = picker.SelectedItem as string;
        if (selected == "All")
        {
            selected = null;
        }

        WorkoutsList.ItemsSource = await App.Service.GetWorkoutTypes(selected);
    }

    private async void OnWorkoutTapped(object sender, TappedEventArgs e)
    {
        var workout = e.Parameter as WorkoutType;
        if (workout == null) return;

        var frame = sender as VisualElement;
        await frame.FadeTo(0.5, 100);
        await frame.FadeTo(1.0, 100);

        await Navigation.PushAsync(new SessionsPage(workout));
    }
}