using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace HopSkills.BackOffice.Data
{
    public class ApplicationCustomer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<ApplicationGroup> Groups { get; set; }
    }
}
