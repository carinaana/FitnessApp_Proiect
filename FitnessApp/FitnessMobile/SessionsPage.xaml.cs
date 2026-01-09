using FitnessMobile.Models;
using Plugin.LocalNotification; // <--- PT NOTIFICARE

namespace FitnessMobile;

public partial class SessionsPage : ContentPage
{
    WorkoutType _workout;

    public SessionsPage(WorkoutType workout)
    {
        InitializeComponent();
        _workout = workout;
        Title = $"{workout.Name} Schedule";
        LoadSessions();
    }

    async void LoadSessions()
    {
        SessionsList.ItemsSource = await App.Service.GetSessions(_workout.ID);
    }

    private async void OnBookClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var session = button.CommandParameter as SessionDto;

        bool confirm = await DisplayAlert("Booking", $"Reserve spot for {session.StartTime}?", "Yes", "No");
        if (!confirm) return;

        var request = new BookingRequest { SessionID = session.ID, UserEmail = App.CurrentUserEmail };

        bool success = await App.Service.CreateBooking(request);

        if (success)
        {
            await DisplayAlert("Success", "Booking Confirmed!", "OK");
            ScheduleNotification(session); // <--- PROGRAMEAZA NOTIFICAREA
            LoadSessions(); // Refresh la lista (scade nr locuri)
        }
        else
        {
            await DisplayAlert("Error", "Class is full or you already booked it.", "OK");
        }
    }

    private void ScheduleNotification(SessionDto session)
    {
        // Data Sesiunii minus 2 ore
        var notifyTime = session.Date.AddHours(-2);

        // Atentie: session.Date din API are ora 00:00. Trebuie combinata cu session.StartTime.
        // Aici presupunem ca API-ul a trimis Data Corecta cu tot cu ora.
        // Daca nu, logica e putin mai complexa de parsat stringul "18:00".
        // Pentru simplificare, notificam acum peste 5 secunde demo:

        var request = new NotificationRequest
        {
            NotificationId = session.ID,
            Title = "Reminder Fitness!",
            Description = $"Ai clasa de {_workout.Name} cu {session.TrainerName}!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10) // Demo: 10 secunde. Pt real: notifyTime
            }
        };

        LocalNotificationCenter.Current.Show(request);
    }
}
