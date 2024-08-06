using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Data
{
    [Keyless]
    public class ApplicationGroupsApplicationUsers
    {
        public Guid GroupsId { get; set; }
        public Guid UsersId { get; set; }
    }
}
