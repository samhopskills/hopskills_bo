using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class UserViewModel
    {
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //[Required]
        //[StringLength(50)]
        //public string Address { get; set; }
        //[Required]
        //[Phone]
        //public string Phone { get; set; }
        [Required]
        //[PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        //[PasswordPropertyText]
        public string ConfirmPassword { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
