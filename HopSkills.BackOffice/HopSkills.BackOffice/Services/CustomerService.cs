using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HopSkillsDbContext _hopSkillsDbContext;

        public CustomerService(HopSkillsDbContext hopSkillsDbContext)
        {
            _hopSkillsDbContext = hopSkillsDbContext;
        }

        public async Task CreateCustomer(CustomerModel customer)
        {
            await _hopSkillsDbContext.Customers.AddAsync(new ApplicationCustomer
            {
                Name = customer.Name,
                Country = customer.Country,
                CreatedOn = DateTime.UtcNow,
            });
            await _hopSkillsDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
        {
            var result = _hopSkillsDbContext.Customers.Select(x => new CustomerModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Country = x.Country,
                CreatedOn = x.CreatedOn
            }).AsEnumerable();

            return result;
        }

        public async Task<CustomerModel> GetCustomerById(string Customerid)
        {
            var result = _hopSkillsDbContext.Customers.Find(_hopSkillsDbContext, Customerid);
            return new CustomerModel{
                Name = result.Name,
                Country = result.Country,
                CreatedOn = result.CreatedOn
            };
        }

    }
}
