using System.ComponentModel.DataAnnotations;

namespace HopSkills.CoreBusiness
{
    public class Customer
    {
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AttachedLicence { get; set; }
        [Url]
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
    }
}