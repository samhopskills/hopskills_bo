using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationTraining
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public TimeOnly TotalDuration { get; set; }
        public bool DoesCertify { get; set; }
        public int TotalXperience { get; set; }
        public List<ApplicationGame>? Games { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public ApplicationUser Creator { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public ApplicationCustomer Customer { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
