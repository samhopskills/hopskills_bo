using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.PluginInterfaces
{
    public interface ITeamRepository
    {
        Task AddTeamAsync(Team team);
        Task DeleteTeamAsync(List<Team> teams);
        Task<List<Team>> GetTeamsAsync();
        Task UpdateTeamAsync(Team team);
    }
}