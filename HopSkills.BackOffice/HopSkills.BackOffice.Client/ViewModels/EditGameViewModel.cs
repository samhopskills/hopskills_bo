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
        public double TotalDuration { get; set; }
        public TimeSpan TimeDuration { get; set; }
        public EditGameImageViewModel Image { get; set; }
        //[Required]
        public int TotalXperience { get; set; }
        public List<CreateMultipleQuestionsViewModel> multipleQuestions { get; set; }
    }

    public class EditGameImageViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool ToDelete { get; set; }
    }

    public class GameChangesModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public string? ElligibleSub { get; set; }
        public string? Status { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? PriorGame { get; set; }
        public double TotalDuration { get; set; }
        public EditGameImageViewModel? Image { get; set; }
        public int? TotalXperience { get; set; }
        public List<QuestionChangeModel>? Questions { get; set; }
    }

    public class QuestionChangeModel
    {
        public string? UniqueId { get; set; }
        public int? Id { get; set; }
        public string? Question { get; set; }
        public double? Duration { get; set; }
        public int? Min { get; set; }
        public int? Sec { get; set; }
        public int? Xperience { get; set; }
        public string? CorrectAnswerExplanation { get; set; }
        public List<GameFileModel?>? ImageFiles { get; set; }
        public List<GameFileModel?>? AudioFiles { get; set; }
        public List<AnswerChangeModel?>? Answers { get; set; }
        public string? ChangeType { get; set; }
    }

    public class AnswerChangeModel
    {
        public string? UniqueId { get; set; }
        public string? Label { get; set; }
        public string? Answer { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Order { get; set; }
        public string? ChangeType { get; set; }
    }
}
