using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Users.Interfaces
{
    public interface IAddTeamUseCase
    {
        Task ExecuteAsync(Team team);
    }
}