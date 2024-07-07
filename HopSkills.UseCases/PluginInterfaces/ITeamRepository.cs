using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.PluginInterfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsAsync();
    }
}