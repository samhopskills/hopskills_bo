using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Model
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}