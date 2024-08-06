using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IGameService
    {
        Task AddGame(CreateGameModel createGameModel);
        Task<GameViewModel> GetAll();
        Task<GameViewModel> GetGamesByCustomer();
    }
}