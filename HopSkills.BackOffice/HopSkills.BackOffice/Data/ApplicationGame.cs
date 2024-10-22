using HopSkills.BO.CoreBusiness;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationGame
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string DifficultyLevel { get; set; }
        public string ElligibleSub { get; set; }
        public string? PriorGame { get; set; }
        public string Status { get; set; }
        public int NumberOfQuestion { get; set; }
        public int Duration { get; set; }
        public TimeOnly TotalDuration { get; set; }
        [AllowNull]
        public string? ImageUri { get; set; }
        public int TotalXp { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public ApplicationUser Creator { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public ApplicationCustomer Customer { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
