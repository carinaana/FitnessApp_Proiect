using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessWeb.Data;
using FitnessWeb.Models;
using FitnessWeb.Models.ViewModels;

namespace FitnessWeb.Pages.Trainers
{
    public class TrainerSpecializationsPageModel : PageModel
    {
        public List<AssignedSpecializationData> AssignedSpecializationDataList;

        public void PopulateAssignedSpecializationData(FitnessContext context, Trainer trainer)
        {
            var allWorkoutTypes = context.WorkoutType;
            var trainerSpecializations = new HashSet<int>(
                trainer.TrainerSpecializations.Select(c => c.WorkoutTypeID));

            AssignedSpecializationDataList = new List<AssignedSpecializationData>();
            foreach (var type in allWorkoutTypes)
            {
                AssignedSpecializationDataList.Add(new AssignedSpecializationData
                {
                    WorkoutTypeID = type.ID,
                    Name = type.Name,
                    Assigned = trainerSpecializations.Contains(type.ID)
                });
            }
        }

        public void UpdateTrainerSpecializations(FitnessContext context, string[] selectedOptions, Trainer trainerToUpdate)
        {
            if (selectedOptions == null)
            {
                trainerToUpdate.TrainerSpecializations = new List<TrainerSpecialization>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var trainerSpecializations = new HashSet<int>(
                trainerToUpdate.TrainerSpecializations.Select(c => c.WorkoutType.ID));

            foreach (var type in context.WorkoutType)
            {
                if (selectedOptionsHS.Contains(type.ID.ToString()))
                {
                    if (!trainerSpecializations.Contains(type.ID))
                    {
                        trainerToUpdate.TrainerSpecializations.Add(new TrainerSpecialization
                        {
                            TrainerID = trainerToUpdate.ID,
                            WorkoutTypeID = type.ID
                        });
                    }
                }
                else
                {
                    if (trainerSpecializations.Contains(type.ID))
                    {
                        var specToRemove = trainerToUpdate.TrainerSpecializations
                            .SingleOrDefault(i => i.WorkoutTypeID == type.ID);
                        context.Remove(specToRemove);
                    }
                }
            }
        }
    }
}