
using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users
{
    public interface IViewRoleListUseCase
    {
        Task<List<Role>> ExecuteAsync();
    }
}