using FitnessMobile.Models;

namespace FitnessMobile;

public partial class TrainerDetailsPage : ContentPage
{
    TrainerDto _trainer;

    public TrainerDetailsPage(TrainerDto trainer)
    {
        InitializeComponent();
        _trainer = trainer;

        // Populam datele
        NameLabel.Text = trainer.FullName;
        SpecLabel.Text = trainer.Specialization;
        DescLabel.Text = trainer.Description;
        ReviewsList.ItemsSource = trainer.Reviews;
    }

    private async void OnAddReviewClicked(object sender, EventArgs e)
    {
        var review = new ReviewDto
        {
            MemberName = App.CurrentUserEmail, // Numele celui logat
            Comment = CommentEntry.Text,
            Rating = (int)Math.Round(RatingSlider.Value),
            Date = DateTime.Now
        };

        bool success = await App.Service.PostReview(_trainer.ID, review);
        if (success)
        {
            await DisplayAlert("Success", "Review added!", "OK");
            await Navigation.PopAsync(); // Ne intoarcem
        }
        else
        {
            await DisplayAlert("Error", "Could not add review.", "OK");
        }
    }
}
