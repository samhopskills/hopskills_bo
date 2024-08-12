namespace HopSkills.BackOffice.Model
{
    public class MultipleQuestionsModel
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public TimeOnly Duration { get; set; }
        public int Xperience { get; set; }
        public List<AnswerModel> PossibleAnswers { get; set; }
        public string CorrectAnswerExplanation { get; set; }
    }
}
