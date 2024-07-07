using System.ComponentModel.DataAnnotations;

namespace HopSkills.CoreBusiness
{
    public class Company
    {
        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public Subscription AttachedLicence { get; set; }
        [Url]
        public string Image { get; set; }
    }
}