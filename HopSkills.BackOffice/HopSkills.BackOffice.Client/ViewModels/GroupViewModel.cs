using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class CreateGroupViewModel
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
    }
}
