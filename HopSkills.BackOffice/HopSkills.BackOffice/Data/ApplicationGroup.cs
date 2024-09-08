using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HopSkills.BO.CoreBusiness;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public virtual ApplicationCustomer Customer { get; set; }
    }
}
