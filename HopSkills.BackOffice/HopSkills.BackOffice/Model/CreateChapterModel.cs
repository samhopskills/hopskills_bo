namespace HopSkills.BackOffice.Model
{
    public class CreateChapterModel
    {
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
        public int Xperience { get; set; }
        public string Content { get; set; }
        public string ImageChapter { get; set; }
        public IEnumerable<string> AttachedGames { get; set; }
        public IEnumerable<ChapterAttachedFile> AttachedFiles { get; set; }
    }
}