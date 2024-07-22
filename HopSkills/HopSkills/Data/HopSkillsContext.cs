using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HopSkills.Data;

namespace HopSkills.Data
{
    public class HopSkillsContext(DbContextOptions<HopSkillsContext> options) : IdentityDbContext<HopSkillsUser>(options)
    {
    }
}
