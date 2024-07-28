using HopSkills.BO.CoreBusiness;
using HopSkills.BackOffice;
using HopSkills.BackOffice.Client.ViewModels;
using MudBlazor;

namespace HopSkills.BackOffice.Client.Services
{
    public interface IAccountManagement
    {
        public Task<List<Role>> GetRoles();

        public Task<bool> AddRole(string roleName);
    }
}
