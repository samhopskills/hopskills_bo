using HopSkills.BackOffice.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationMultiQuestion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public TimeOnly Duration { get; set; }
        public int Xperience { get; set; }
        public List<ApplicationAnswer> PossibleAnswers { get; set; }
        public string CorrectAnswerExplanation { get; set; }
        [ForeignKey("GameId")]
        public Guid GameId { get; set; }
        public ApplicationGame Game { get; set; }
    }
}
