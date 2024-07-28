using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.PluginInterfaces
{
    public interface IRoleRepository
    {
        Task CreateAsync(Role role);
        Task<List<Role>> GetAllAsync();
    }
}