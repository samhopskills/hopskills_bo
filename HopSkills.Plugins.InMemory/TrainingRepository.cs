using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;

namespace HopSkills.Plugins.InMemory
{
    public class TrainingRepository : ITrainingRepository
    {
        private List<Training> trainings;

        public TrainingRepository()
        {
            trainings = GenerateRandomTrainings(10);
        }

        public async Task<List<Training>> GetTrainingAsync()
        {
            return trainings;
        }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomPhoneNumber()
        {
            return $"0{random.Next(100000000, 999999999)}";
        }

        public static Training CreateRandomTraining(int trainingId)
        {
            return new Training
            {
                TrainingId = trainingId,
                Title = RandomString(12),
                TotalXp = random.Next(50),
                IsArchived = true,
                IsCertification = true,
                IsPublished = false,
                Description = RandomString(200),
                theme = new Theme { Title = RandomString(10) },
                Difficulty = new DifficultyLevel { Title = RandomString(10) }
            };
        }

        public static List<Training> GenerateRandomTrainings(int count)
        {
            var trainings = new List<Training>();
            for (int i = 1; i <= count; i++)
            {
                trainings.Add(CreateRandomTraining(i));
            }
            return trainings;
        }
    }
}
