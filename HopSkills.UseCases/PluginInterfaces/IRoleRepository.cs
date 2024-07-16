using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.PluginInterfaces
{
    public interface IRoleRepository
    {
        Task CreateAsync(Role role);
        Task<List<Role>> GetAllAsync();
    }
}