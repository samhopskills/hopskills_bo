using System.ComponentModel.DataAnnotations;

namespace HopSkills.BackOffice.Client.ViewModels
{
    public class CreateChapterViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public TimeSpan? Duration { get; set; }
        [Required]
        public int Xperience { get; set; }
        [Required]
        public string Content { get; set; }
        public string ImageChapter { get; set; }
        public IEnumerable<string> AttachedGames { get; set; }
        public IEnumerable<ChapterAttachedFileViewModel> AttachedFiles { get; set; }
    }
}