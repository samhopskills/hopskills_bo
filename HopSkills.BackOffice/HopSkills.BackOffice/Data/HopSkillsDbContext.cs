using HopSkills.BackOffice.Model;
using HopSkills.BO.CoreBusiness;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Data;

public class HopSkillsDbContext(DbContextOptions<HopSkillsDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationCustomer> Customers { get; set; }
    public DbSet<ApplicationGroup> Groups { get; set; }
    //public DbSet<ApplicationGroupsApplicationUsers> GroupsUsers { get; set; }
}
