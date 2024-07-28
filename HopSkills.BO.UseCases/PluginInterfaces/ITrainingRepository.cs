using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.PluginInterfaces
{
    public interface ITrainingRepository
    {
        Task<List<Training>> GetTrainingAsync();
    }
}