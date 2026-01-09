using FitnessMobile.Models;

namespace FitnessMobile;

public partial class TrainersPage : ContentPage
{
    public TrainersPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
        await LoadFilters();
    }

    async Task LoadFilters()
    {
        var workoutTypes = await App.Service.GetWorkoutTypes();

        var filterOptions = workoutTypes.Select(w => w.Name).ToList();
        filterOptions.Insert(0, "All");

        SpecializationPicker.ItemsSource = filterOptions;

    }
    async Task LoadData(string filter = null)
    {
        var trainers = await App.Service.GetTrainers(filter);
        TrainersList.ItemsSource = trainers;
    }

    private async void OnFilterChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        string selected = picker.SelectedItem as string;
        if (selected == "All")
        {
            selected = null;
        }
        await LoadData(selected);
    }

    private async void OnTrainerTapped(object sender, TappedEventArgs e)
    {
        var trainer = e.Parameter as TrainerDto;
        if (trainer == null) return;

        var frame = sender as VisualElement;
        await frame.FadeTo(0.5, 100);
        await frame.FadeTo(1.0, 100);
        await Navigation.PushAsync(new TrainerDetailsPage(trainer));
    }
}