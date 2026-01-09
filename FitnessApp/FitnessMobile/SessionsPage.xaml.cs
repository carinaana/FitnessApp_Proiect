using FitnessMobile.Models;
using Plugin.LocalNotification;

namespace FitnessMobile;

public partial class SessionsPage : ContentPage
{
    WorkoutType _workout;
    bool _isGlobal = false;

    public SessionsPage()
    {
        InitializeComponent();
        _isGlobal = true;
        Title = "Full Schedule";
    }

    public SessionsPage(WorkoutType workout)
    {
        InitializeComponent();
        _workout = workout;
        Title = $"{workout.Name} Schedule";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSessions();
    }

    async Task LoadSessions()
    {
        if (_isGlobal)
        {

            var allWorkouts = await App.Service.GetWorkoutTypes();
            var allSessions = new List<SessionDto>();

            foreach (var w in allWorkouts)
            {
                var s = await App.Service.GetSessions(w.ID);
                allSessions.AddRange(s);
            }

            SessionsList.ItemsSource = allSessions.OrderBy(x => x.Date).ThenBy(x => x.StartTime).ToList();
        }
        else
        {
            SessionsList.ItemsSource = await App.Service.GetSessions(_workout.ID);
        }
    }

    private async void OnBookClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var session = button.CommandParameter as SessionDto;

        bool confirm = await DisplayAlert("Booking", $"Reserve spot for {session.WorkoutName} at {session.StartTime}?", "Yes", "No");
        if (!confirm) return;

        var request = new BookingRequest { SessionID = session.ID, UserEmail = App.CurrentUserEmail };

        bool success = await App.Service.CreateBooking(request);

        if (success)
        {
            await DisplayAlert("Success", "Booking Confirmed!", "OK");
            ScheduleNotification(session);
            await LoadSessions(); 
        }
        else
        {
            await DisplayAlert("Error", "Class is full or already booked.", "OK");
        }
    }

    private async void ScheduleNotification(SessionDto session)
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            bool granted = await LocalNotificationCenter.Current.RequestNotificationPermission();

            if (!granted)
            {
                await DisplayAlert("Permisiune refuzata", "Nu iti putem trimite notificari.", "OK");
                return;
            }
        }


        //var notifyTime = session.Date.AddHours(-2);
        var notifyTime = DateTime.Now.AddSeconds(10);

        // Daca sesiunea e peste 2 luni, notificarea e utila. Daca e azi, notificam imediat demo.
        //if (notifyTime < DateTime.Now) notifyTime = DateTime.Now.AddSeconds(5);

        var request = new NotificationRequest
        {
            NotificationId = session.ID,
            Title = "Reminder Fitness!",
            Description = $"Ai clasa de {session.WorkoutName} la ora {session.StartTime}!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = notifyTime,
                Android = new Plugin.LocalNotification.AndroidOption.AndroidScheduleOptions
                {
                    AlarmType = Plugin.LocalNotification.AndroidOption.AndroidAlarmType.Rtc
                }
            }
        };

        await LocalNotificationCenter.Current.Show(request);
    }
}