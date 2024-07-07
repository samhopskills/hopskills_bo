using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Content.Interface
{
    public interface IViewGameListUseCase
    {
        Task<Game> Execute();
    }
}