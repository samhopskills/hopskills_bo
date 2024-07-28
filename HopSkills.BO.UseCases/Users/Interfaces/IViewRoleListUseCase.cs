using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Users.Interfaces
{
    public interface IViewRoleListUseCase
    {
        Task<List<Role>> ExecuteAsync();
    }
}