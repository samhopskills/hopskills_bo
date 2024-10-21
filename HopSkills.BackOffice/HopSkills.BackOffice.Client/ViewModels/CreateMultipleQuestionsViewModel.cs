using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateMultipleQuestionsViewModel 
    {
        [Required]
        public List<CreateAnswerViewModel> PossibleAnswers { get; set; }
        public string Zone { get; set; }
        public int CountId { get; set; }
        public bool Expanded { get; set; }
        public bool IsValid { get; set; }
        public string UniqueId { get; set; }
        [Required]
        public string Question { get; set; }
        public TimeSpan? Duration { get; set; }
        [Required]
        [Range(1, 10)]
        public int Min { get; set; }
        [Required]
        [Range(0, 59)]
        public int Sec { get; set; }
        [Required]
        [Range(1, 20)]
        public int Xperience { get; set; }
        [Required]
        public string CorrectAnswerExplanation { get; set; }
        [MaxLength(255)]
        public List<GameFileModel> ImageFiles { get; set; }
        [MaxLength(255)]
        public List<GameFileModel> AudioFiles { get; set; }
    }

    public class GameFileModel
    {
        public string Content { get; set; }
        public bool Delete { get; set; }
    }
}
