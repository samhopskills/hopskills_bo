using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Services
{
    public class GroupService : IGroupService
    {
        private readonly ILogger<GroupService> _logger;
        private readonly HopSkillsDbContext _hopSkillsDbContext;

        public GroupService(ILogger<GroupService> logger, HopSkillsDbContext hopSkillsDbContext)
        {
            _logger = logger;
            _hopSkillsDbContext = hopSkillsDbContext;
        }

        public async Task AddUserGroupAsync(AddUserGroupModel userGroup)
        {
            //try
            //{
            //    await _hopSkillsDbContext.GroupsUsers.AddAsync(new ApplicationGroupsApplicationUsers
            //    {
            //        UsersId = new Guid(userGroup.UserId),
            //        GroupsId = new Guid(userGroup.GroupId)
            //    });
            //    await _hopSkillsDbContext.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message, ex);
            //}
        }

        public async Task CreateGroupAsync(CreateGroupModel group)
        {
            try
            {
                await _hopSkillsDbContext.Groups.AddAsync(new ApplicationGroup
                {
                    CreatedOn = DateTime.UtcNow,
                    Name = group.Name,
                    CustomerId = new Guid(group.CompanyId)
                });
                await _hopSkillsDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }


        public async Task<List<GroupModel>> GetGroupsAsync()
        {
            var groups = new List<GroupModel>();
            try
            {
                groups = await _hopSkillsDbContext.Groups.Select(x => new
                GroupModel
                { Id = x.Id.ToString(), Name = x.Name, CreatedOn = x.CreatedOn
                , CompanyId = x.CustomerId.ToString() }).ToListAsync();
                return groups;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return groups;
        }

        public async Task<List<GroupModel>> GetGroupsbyCompanyAsync(string companyId)
        {
            var groups = new List<GroupModel>();
            try
            {
                groups = await _hopSkillsDbContext.Groups.Where(x => x.CustomerId.ToString() == companyId).Select(x => new
                GroupModel
                { Id = x.Id.ToString(), Name = x.Name, CreatedOn = x.CreatedOn }).ToListAsync();
                return groups;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return groups;
        }

    }
}
