using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.CoreBusiness
{
    public class Game
    {
        public int GameId { get; set; }
        public string ImageLink { get; set; }
        public string Title { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public Theme theme { get; set; }
        public Game PreGame { get; set; }
        public string Description { get; set; }
        public int TotalLast { get; set; }
        public int TotalXp { get; set; }
        public bool IsPublished { get; set; }
        public bool IsArchived { get; set; }
        public List<QuizzContent> QuizzContents { get; set; }
        public List<TrueOrFalseContent> TrueOrFalseContents { get; set; }
    }

    public class GameContent
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public int Last { get; set; }
        public int Xp { get; set; }
        public List<string> ImagesLinks { get; set; }
        public List<string> AudiosLinks { get; set; }
        public string ExplainAnswer { get; set; }
    }

    public class TrueOrFalseContent : GameContent
    {
        public List<ContentAnswer> Answers { get; set; }
    }

    public class ContentAnswer
    {
        public int AnswerId { get; set; }
        public string ImageLink { get; set; }
    }

    public class QuizzContent : GameContent
    {
        public List<ContentAnswer> Answers { get; set; }

    }
}
