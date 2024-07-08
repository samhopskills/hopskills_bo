using HopSkills.CoreBusiness;

namespace HopSkills.UseCases.PluginInterfaces
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();
    }
}