using HopSkills.BO.CoreBusiness;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopSkills.BackOffice.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    [ForeignKey("CustomerId")]
    public Guid CustomerId { get; set; }
    public virtual ApplicationCustomer Customer { get; set; }
    public List<ApplicationGroup> Groups { get; } = [];
}

