using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Users.Interfaces
{
    public interface IAddTeamUseCase
    {
        Task ExecuteAsync(Team team);
    }
}