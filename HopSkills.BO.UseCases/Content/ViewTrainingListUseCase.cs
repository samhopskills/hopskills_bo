using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.Content.Interface;
using HopSkills.BO.UseCases.PluginInterfaces;

namespace HopSkills.BO.UseCases.Content
{
    public class ViewTrainingListUseCase : IViewTrainingListUseCase
    {
        private readonly ITrainingRepository _trainingRepository;

        public ViewTrainingListUseCase(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<List<Training>> ExecuteAsync()
        {
            return await _trainingRepository.GetTrainingAsync();
        }
    }
}
