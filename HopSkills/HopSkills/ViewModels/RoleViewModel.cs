using System.ComponentModel.DataAnnotations;

namespace HopSkills.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
