using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.Plugins.InMemory
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> customers { get; set; }
        public CustomerRepository()
        {
            customers = GenerateRandom(10);
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return customers;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomPhoneNumber()
        {
            return $"0{random.Next(100000000, 999999999)}";
        }

        public static Customer CreateRandom(int customerId)
        {
            return new Customer
            {
                Name = RandomString(10),
                Address = RandomString(15),
                AttachedLicence = null,
                CompanyId = customerId,
                Country = RandomString(3),
                Email = RandomString(10),
                Image = RandomString(10),
                Phone = RandomString(10),
                CreationDate = DateTime.Now
            };
        }

        public static List<Customer> GenerateRandom(int count)
        {
            var _customers = new List<Customer>();
            for (int i = 1; i <= count; i++)
            {
                _customers.Add(CreateRandom(i));
            }
            return _customers;
        }

        public Task CreateAsync(Customer customer)
        {
            var maxId = customers.Max(u => u.CompanyId);
            customer.CompanyId = maxId + 1;

            customers.Add(customer);

            return Task.CompletedTask;
        }
    }
}
