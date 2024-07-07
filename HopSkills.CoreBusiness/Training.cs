using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.CoreBusiness
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string ImageLink { get; set; }
        public string Title { get; set; }
        public DifficultyLevel Difficulty{ get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public Theme theme { get; set; }
        public Training PreTraining { get; set; }
        public string Description { get; set; }
        public List<Chapter> Chapters { get; set; }
        public int TotalLast { get; set; }
        public int TotalXp { get; set; }
        public bool IsCertification { get; set; }
        public bool IsPublished { get; set; }
        public bool IsArchived { get; set; }
        public bool Status { get; set; }
    }

    public class Chapter
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public int Xp { get; set; }
        public string? Decription { get; set; }
        public List<string> ImagesLinks { get; set; }
        public List<ContentChapter> Contents { get; set; }
        public List<Game> Games { get; set; }
    }

    public class ContentChapter
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public TimeOnly Last { get; set; }
        public string Content { get; set; }
        public string  Type { get; set; }
    }

    public class Theme
    {
        public int ThemeId { get; set; }
        public string Title { get; set; }
    }

    public class DifficultyLevel
    {
        public int DifficultyLevelId { get; set; }
        public string Title { get; set; }
    }
}
