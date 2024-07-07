using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.PluginInterfaces
{
    public interface ITrainingRepository
    {
        Task<List<Training>> GetTrainingAsync();
    }
}