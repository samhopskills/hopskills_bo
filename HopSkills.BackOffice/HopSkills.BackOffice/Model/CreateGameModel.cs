namespace HopSkills.BackOffice.Model
{
    public class CreateGameModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Theme { get; set; }
        public string Status { get; set; }
        public int NumberOfQuestion { get; set; }
        public int Lasting { get; set; }
        public string Image { get; set; }
        public int Xp { get; set; }
    }
}