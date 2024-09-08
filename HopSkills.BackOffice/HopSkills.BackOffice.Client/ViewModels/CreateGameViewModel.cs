using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateGameViewModel
    {
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
        public string Image { get; set; }
        //[Required]
        public int TotalXperience { get; set; }
        public List<CreateMultipleQuestionsViewModel> multipleQuestions { get; set; }
    }
}
