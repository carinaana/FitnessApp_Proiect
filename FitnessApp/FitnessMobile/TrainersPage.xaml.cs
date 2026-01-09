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
    }

    async Task LoadData(string filter = null)
    {
        var trainers = await App.Service.GetTrainers(filter);
        TrainersList.ItemsSource = trainers;
    }

    private async void OnFilterChanged(object sender, EventArgs e)
    {
        string selected = SpecializationPicker.SelectedItem as string;
        if (selected == "All") selected = null;
        await LoadData(selected);
    }

    private async void OnTrainerSelected(object sender, SelectionChangedEventArgs e)
    {
        var trainer = e.CurrentSelection.FirstOrDefault() as TrainerDto;
        if (trainer == null) return;

        // Navigam la detalii
        await Navigation.PushAsync(new TrainerDetailsPage(trainer));

        // Resetam selectia
        TrainersList.SelectedItem = null;
    }
}
