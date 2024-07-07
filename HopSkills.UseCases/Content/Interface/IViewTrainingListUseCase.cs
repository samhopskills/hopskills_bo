using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.Content.Interface
{
    public interface IViewTrainingListUseCase
    {
        Task<List<Training>> ExecuteAsync();
    }
}