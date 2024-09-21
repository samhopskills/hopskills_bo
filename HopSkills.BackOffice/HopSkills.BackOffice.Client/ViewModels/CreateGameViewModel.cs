using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
        [Required]
        public string ElligibleSub { get; set; }
        [Required]
        public string Status { get; set; } = "Draft";
        [Required]
        public string DifficultyLevel { get; set; }
        //[Required]
        public string? PriorGame { get; set; }
        public int NumberOfQuestion { get; set; }
        //[Required]
        public TimeSpan TotalDuration { get; set; }
        [AllowNull]
        public CreateGameImageViewModel? Image { get; set; }
        //[Required]
        public int TotalXperience { get; set; }
        public List<CreateMultipleQuestionsViewModel> multipleQuestions { get; set; }
    }

    public class CreateGameImageViewModel
    {
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [AllowNull]
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
