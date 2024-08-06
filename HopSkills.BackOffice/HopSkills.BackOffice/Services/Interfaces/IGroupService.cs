using HopSkills.BackOffice.Model;

namespace HopSkills.BackOffice.Services.Interfaces
{
    public interface IGroupService
    {
        Task AddUserGroupAsync(AddUserGroupModel userGroup);
        Task CreateGroupAsync(CreateGroupModel group);
        Task<List<GroupModel>> GetGroupsAsync();
        Task<List<GroupModel>> GetGroupsbyCompanyAsync(string companyId);
    }
}