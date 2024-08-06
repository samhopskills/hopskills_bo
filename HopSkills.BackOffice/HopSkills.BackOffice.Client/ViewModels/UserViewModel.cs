using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }
    }

    public class CreateUserViewModel
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
        [Required]
        //[PasswordPropertyText]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Company { get; set; }
    }

    public class CreateUserCustomerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
