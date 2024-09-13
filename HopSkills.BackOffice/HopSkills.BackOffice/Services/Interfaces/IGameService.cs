using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Model;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IGameService
    {
        Task AddGame(CreateGameModel createGameModel);
        Task EditGame(EditGameModel editGameModel);
        Task<bool> DeleteGame(List<GameViewModel> games);
        Task<IEnumerable<GameViewModel>> GetAll();
        Task<IEnumerable<GameViewModel>> GetGamesByCustomer(string companyId);
        Task<IEnumerable<GameViewModel>> GetGamesByUser(string usermail);
        Task<bool> UpdateGameStatus(string id);
    }
}