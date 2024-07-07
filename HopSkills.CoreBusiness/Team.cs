using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.CoreBusiness
{
    public class Team
    {
        public int  TeamId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public int NumberOfUser { get; set; }
    }
}
