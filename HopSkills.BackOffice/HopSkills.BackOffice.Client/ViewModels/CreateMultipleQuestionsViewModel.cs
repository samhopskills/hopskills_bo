using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateMultipleQuestionsViewModel
    {
        [Required]
        [MinLength(2)]
        public List<CreateAnswerViewModel> PossibleAnswers { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public int Xperience { get; set; }
        [Required]
        public string CorrectAnswerExplanation { get; set; }
        [MaxLength(255)]
        public List<string> ImageFiles { get; set; }
        [MaxLength(255)]
        public List<string> AudioFiles { get; set; }
        public string Zone { get; set; }

    }
}
