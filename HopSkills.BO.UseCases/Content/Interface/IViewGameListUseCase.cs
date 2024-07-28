using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Content.Interface
{
    public interface IViewGameListUseCase
    {
        Task<Game> Execute();
    }
}