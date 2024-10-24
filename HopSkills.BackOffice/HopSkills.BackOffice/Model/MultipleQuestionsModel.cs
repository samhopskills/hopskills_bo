using HopSkills.BackOffice.Client.ViewModels;

namespace HopSkills.BackOffice.Model
{
    public class MultipleQuestionsModel
    {
        public string UniqueId { get; set; }
        public string Question { get; set; }
        public TimeSpan? Duration { get; set; }
        public int Xperience { get; set; }
        public List<AnswerModel> PossibleAnswers { get; set; }
        public string CorrectAnswerExplanation { get; set; }
        public List<GameFileModel>? ImageFiles { get; set; }
        public List<GameFileModel>? AudioFiles { get; set; }
    }
}
