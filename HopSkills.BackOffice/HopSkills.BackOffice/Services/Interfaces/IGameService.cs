using Azure;
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
        Task<EditGameModel?> GetGameById(string id);
        Task<Response<bool>> DeleteImageFromGame(string id);
        Task<Response<bool>> UploadImageForGame(EditGameImage Image, string Id);
        Task UpdateGamePartial(GameChangesModel changes);
    }
}