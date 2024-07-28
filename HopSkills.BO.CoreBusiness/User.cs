using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.BO.CoreBusiness
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public int companyId { get; set; }

        public Role role { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
