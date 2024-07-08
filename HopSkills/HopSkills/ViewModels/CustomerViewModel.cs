using System.ComponentModel.DataAnnotations;

namespace HopSkills.ViewModels
{
    public class CustomerViewModel
    {
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
    }
}
