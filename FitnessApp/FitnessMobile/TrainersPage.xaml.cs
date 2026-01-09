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

    // EVENIMENTUL NOU DE CLICK (TAP)
    private async void OnTrainerTapped(object sender, TappedEventArgs e)
    {
        var trainer = e.Parameter as TrainerDto;
        if (trainer == null) return;

        // Efect vizual rapid (blink)
        var frame = sender as VisualElement;
        await frame.FadeTo(0.5, 100);
        await frame.FadeTo(1.0, 100);

        // Navigare
        await Navigation.PushAsync(new TrainerDetailsPage(trainer));
    }
}