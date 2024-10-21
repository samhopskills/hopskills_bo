using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateAnswerViewModel
    {
        public string? UniqueId { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public string? ChangeType { get; set; }
    }
}