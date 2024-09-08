using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Data
{
    [Keyless]
    public class TrainingGame
    {
        public Guid TrainingId { get; set; }
        public Guid GameId { get; set; }
    }
}
