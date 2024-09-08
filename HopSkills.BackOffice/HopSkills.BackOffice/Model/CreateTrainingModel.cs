
using HopSkills.BO.CoreBusiness;

namespace HopSkills.BackOffice.Model
{
    public class CreateTrainingModel
    {
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public TimeOnly TotalDuration { get; set; }
        public string? Image { get; set; }
        public bool DoesCertify { get; set; }
        public int TotalXperience { get; set; }
        public List<string>? Games { get; set; }
        public IEnumerable<CreateChapterModel> Chapters { get; internal set; }
    }
}