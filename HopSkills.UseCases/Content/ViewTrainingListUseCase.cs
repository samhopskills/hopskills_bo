using HopSkills.CoreBusiness;
using HopSkills.UseCases.Content.Interface;
using HopSkills.UseCases.PluginInterfaces;

namespace HopSkills.UseCases.Content
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
