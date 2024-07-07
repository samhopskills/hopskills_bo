using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IViewTeamListUseCase
    {
        Task<List<Team>> ExecuteAsync();
    }
}