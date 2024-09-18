using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateMultipleQuestionsViewModel
    {
        public List<CreateAnswerViewModel> PossibleAnswers { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public TimeSpan? Duration { get; set; }
        [Required]
        public int Xperience { get; set; }
        [Required]
        public string CorrectAnswerExplanation { get; set; }
        [MaxLength(255)]
        public List<string> ImageFiles { get; set; }
        [MaxLength(255)]
        public List<string> AudioFiles { get; set; }
    }
}
