using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateFormViewModel
    {

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
        [Range(1,20)]
        public int Xperience { get; set; }
        [Required]
        public string CorrectAnswerExplanation { get; set; }
        [MaxLength(255)]
        public List<string> ImageFiles { get; set; }
        [MaxLength(255)]
        public List<string> AudioFiles { get; set; }
    }
}
