using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateMultipleQuestionsViewModel : CreateFormViewModel
    {
        [Required]
        public List<CreateAnswerViewModel> PossibleAnswers { get; set; }
        public string Zone { get; set; }
        public int Id { get; set; }
    }
}
