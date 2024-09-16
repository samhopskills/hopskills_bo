namespace HopSkills.BackOffice.Model
{
    public class EditGameModel
    {
        public Guid Id { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DifficultyLevel { get; set; }
        public string ElligibleSub { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public string PriorGame { get; set; }
        public TimeOnly TotalDuration { get; set; }
        public EditGameImage? Image { get; set; }
        public int TotalXperience { get; set; }
        public List<MultipleQuestionsModel>? multipleQuestions { get; set; }

    }

    public class EditGameImage
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
