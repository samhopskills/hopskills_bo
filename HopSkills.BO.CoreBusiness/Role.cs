using System.ComponentModel.DataAnnotations;

namespace HopSkills.BO.CoreBusiness
{
    public class Role
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        //public List<UseCase> UseCases { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}