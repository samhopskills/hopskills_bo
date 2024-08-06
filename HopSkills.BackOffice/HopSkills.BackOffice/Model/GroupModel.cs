using HopSkills.BackOffice.Data;

namespace HopSkills.BackOffice.Model
{
    public class GroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CompanyId { get; set; }
    }

    public class CreateGroupModel
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
    }
}
