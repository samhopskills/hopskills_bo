using HopSkills.BO.CoreBusiness;

namespace HopSkills.BO.UseCases.PluginInterfaces
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task EditAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();
    }
}