
using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Users.Interfaces
{
    public interface IAddRoleUseCase
    {
        Task ExecuteAsync(Role role);
    }
}