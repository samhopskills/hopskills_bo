using HopSkills.BackOffice.Model;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface ICustomerService
    {
        Task CreateCustomer(CustomerModel customer);
        Task<IEnumerable<CustomerModel>> GetAllCustomers();
        Task<CustomerModel> GetCustomerById(string Customerid);
    }
}