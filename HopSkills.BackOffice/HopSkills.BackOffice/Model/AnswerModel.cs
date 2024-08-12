using HopSkills.BackOffice.Data;

namespace HopSkills.BackOffice.Model
{
    public class AnswerModel
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}