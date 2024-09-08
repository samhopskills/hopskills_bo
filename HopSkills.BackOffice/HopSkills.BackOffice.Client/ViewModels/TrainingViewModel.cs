namespace HopSkills.BackOffice.Client.ViewModels
{
    public class TrainingViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public string Difficulty { get; set; }
        public int ChaptersNumber { get; set; }
        public TimeOnly Duration { get; set; }
        public bool DoesCertify { get; set; }
    }
}
