using System.ComponentModel.DataAnnotations;

namespace HopSkills.ViewModels
{
    public class TeamViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
