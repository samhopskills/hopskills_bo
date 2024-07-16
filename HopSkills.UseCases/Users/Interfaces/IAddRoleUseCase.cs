using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IAddRoleUseCase
    {
        Task ExecuteAsync(Role role);
    }
}