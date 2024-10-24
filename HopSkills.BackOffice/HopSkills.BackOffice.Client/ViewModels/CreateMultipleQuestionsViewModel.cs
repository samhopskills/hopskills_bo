using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateMultipleQuestionsViewModel 
    {
        [Required]
        public List<CreateAnswerViewModel> PossibleAnswers { get; set; }
        public int CountId { get; set; }
        public bool IsValid { get; set; }
        public string UniqueId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public TimeSpan? Duration { get; set; }
        [Range(1, 10)]
        public int Min { get; set; }
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
