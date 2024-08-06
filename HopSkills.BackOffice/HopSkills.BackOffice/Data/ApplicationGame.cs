using HopSkills.BO.CoreBusiness;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationGame
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public int NumberOfQuestion { get; set; }
        public int Lasting { get; set; }
        public string ImageUri { get; set; }
        public int TotalXp { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public ApplicationUser Creator { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public ApplicationCustomer Customer { get; set; }
    }
}
