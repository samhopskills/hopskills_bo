using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IViewUserListUseCase
    {
        Task<List<User>> ExecuteAsync();
    }
}