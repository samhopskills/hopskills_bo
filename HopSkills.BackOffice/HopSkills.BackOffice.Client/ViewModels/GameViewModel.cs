namespace HopSkills.BackOffice.Client.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public int NumberOfQuestion { get; set; }
        public TimeOnly Duration { get; set; }
    }
}
