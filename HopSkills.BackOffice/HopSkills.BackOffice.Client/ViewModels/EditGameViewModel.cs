using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class EditGameViewModel
    {
        public Guid Id { get; set; }
        public string Creator { get; set; }
        [Required]
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; }
        [Required]
        public string Theme { get; set; }
        //[Required]
        public string ElligibleSub { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string DifficultyLevel { get; set; }
        //[Required]
        public string PriorGame { get; set; }
        public int NumberOfQuestion { get; set; }
        //[Required]
        public TimeSpan TotalDuration { get; set; }
        public EditGameImageViewModel Image { get; set; }
        //[Required]
        public int TotalXperience { get; set; }
        public List<CreateMultipleQuestionsViewModel> multipleQuestions { get; set; }
    }

    public class EditGameImageViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
