using Newtonsoft.Json; // <--- NECESAR pt JSON
using System.Text;
using FitnessMobile.Models;
using System.Net.Http;   // <--- NECESAR pt HttpClient (_client)

namespace FitnessMobile.Services
{
    public class RestService
    {
        // --- AICI ERA PROBLEMA PROBABIL ---
        // Trebuie sa declari _client aici, ca sa fie vazut in toata clasa
        HttpClient _client;

        // SCHIMBA PORTUL AICI CU CEL REAL (ex: 7123)
        // Daca folosesti Android Emulator, lasa 10.0.2.2
        string BaseUrl = "https://10.0.2.2:7146/api/";

        public RestService()
        {
            // Aceasta setare permite aplicatiei sa ignore erorile SSL pe localhost
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            // Aici initializam variabila _client
            _client = new HttpClient(handler);
        }

        // --- AUTH ---
        public async Task<bool> Register(RegisterDto user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // _client este folosit aici
                var response = await _client.PostAsync(BaseUrl + "AuthApi/Register", content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<string> Login(LoginDto user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(BaseUrl + "AuthApi/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    return "OK";
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return errorMsg.Contains("Acces permis") ? "Acces permis doar clienților!" : "Email sau parolă greșită.";
                }
            }
            catch
            {
                return "Eroare de conexiune.";
            }
        }

        // --- TRAINERS ---
        public async Task<List<TrainerDto>> GetTrainers(string specialization = null)
        {
            string url = BaseUrl + "TrainersApi";
            if (!string.IsNullOrEmpty(specialization)) url += $"?specialization={specialization}";

            try
            {
                var response = await _client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<TrainerDto>>(response);
            }
            catch { return new List<TrainerDto>(); }
        }

        public async Task<bool> PostReview(int trainerId, ReviewDto review)
        {
            var json = JsonConvert.SerializeObject(review);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(BaseUrl + $"TrainersApi/{trainerId}/Review", content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // --- WORKOUTS & SESSIONS ---
        public async Task<List<WorkoutType>> GetWorkoutTypes(string difficulty = null)
        {
            string url = BaseUrl + "WorkoutTypesApi";
            if (!string.IsNullOrEmpty(difficulty)) url += $"?difficulty={difficulty}";

            try
            {
                var response = await _client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<WorkoutType>>(response);
            }
            catch { return new List<WorkoutType>(); }
        }

        public async Task<List<SessionDto>> GetSessions(int workoutId)
        {
            try
            {
                var response = await _client.GetStringAsync(BaseUrl + $"WorkoutTypesApi/{workoutId}/Sessions");
                return JsonConvert.DeserializeObject<List<SessionDto>>(response);
            }
            catch { return new List<SessionDto>(); }
        }

        // --- BOOKING ---
        public async Task<bool> CreateBooking(BookingRequest booking)
        {
            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(BaseUrl + "BookingsApi", content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}