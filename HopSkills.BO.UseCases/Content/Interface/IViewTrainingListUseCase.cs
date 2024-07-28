using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.Content.Interface
{
    public interface IViewTrainingListUseCase
    {
        Task<List<Training>> ExecuteAsync();
    }
}