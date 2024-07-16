using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IViewTeamListUseCase
    {
        Task DeleteAsync(List<Team> teams);
        Task<List<Team>> ExecuteAsync();
        Task UpdateAsync(Team team);
    }
}