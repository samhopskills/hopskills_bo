using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Data;

public class HopSkillsDbContext(DbContextOptions<HopSkillsDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
