using System.ComponentModel.DataAnnotations;

namespace HopSkills.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
