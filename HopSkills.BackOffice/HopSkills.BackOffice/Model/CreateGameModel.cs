namespace HopSkills.BackOffice.Model
{
    public class CreateGameModel
    {
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Enum Theme { get; set; }
        public Enum Status { get; set; }
        public int NumberOfQuestion { get; set; }
        public int TotalDuration { get; set; }
        public string Image { get; set; }
        public int TotalXperience { get; set; }
        public List<MultipleQuestionsModel>? multipleQuestions { get; set; }
    }
}