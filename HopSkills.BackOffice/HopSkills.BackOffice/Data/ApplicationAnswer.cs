using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationAnswer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        [ForeignKey("MultiQuestionId")]
        public Guid MultiQuestionId { get; set; }
        public ApplicationMultiQuestion MultiQuestions { get; set; }
    }
}
