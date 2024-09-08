using HopSkills.BackOffice.Model;

namespace HopSkills.BackOffice.Services
{
    public interface ITrainingService
    {
        Task AddTraining(CreateTrainingModel createTrainingModel);
        Task<bool> DeleteTraining(List<TrainingModel> games);
        Task<IEnumerable<TrainingModel>> GetAll();
        Task<IEnumerable<TrainingModel>> GetTrainingsByCustomer(string companyId);
        Task<IEnumerable<TrainingModel>> GetTrainingsByUser(string usermail);
        Task<bool> UpdateTrainingStatus(string id);
    }
}