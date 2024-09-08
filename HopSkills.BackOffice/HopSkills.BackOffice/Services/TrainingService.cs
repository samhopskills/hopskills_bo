using Azure.Storage.Blobs;
using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly HopSkillsDbContext _hopSkillsDb;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;
        private const string ContainerName = "trainings";

        public TrainingService(HopSkillsDbContext hopSkillsDb, 
            IUserService userService,
            ICustomerService customerService, BlobServiceClient blobServiceClient)
        {
            _hopSkillsDb = hopSkillsDb;
            _userService = userService;
            _customerService = customerService;
            _blobServiceClient = blobServiceClient;
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        }

        public async Task AddTraining(CreateTrainingModel createTrainingModel)
        {
            try
            {
                var creator = _hopSkillsDb.Users.FirstOrDefault(u => u.Email == createTrainingModel.Creator);
                if (creator != null)
                {
                    var newAppTraining = new ApplicationTraining
                    {
                        TotalDuration = createTrainingModel.TotalDuration,
                        Status = createTrainingModel.Status.ToString(),
                        Theme = createTrainingModel.Theme.ToString(),
                        TotalXperience = createTrainingModel.TotalXperience,
                        Title = createTrainingModel.Title,
                        UserId = new Guid(creator.Id),
                        CustomerId = creator.CustomerId
                    };
                    var resultTraining = await _hopSkillsDb.Trainings.AddAsync(newAppTraining);
                    Guid? trainingId = resultTraining.Entity.Id;
                    if (trainingId != null)
                    {
                        if (!string.IsNullOrEmpty(createTrainingModel.Image))
                        {
                            var blobClient = _blobContainerClient.GetBlobClient($"{trainingId.Value}.png");
                            var bytes = Convert.FromBase64String(createTrainingModel.Image);
                            await blobClient.UploadAsync(BinaryData.FromBytes(bytes));
                        }
                        var chaptersToCreate = createTrainingModel.Chapters;
                        if (!chaptersToCreate.Any())
                        {
                            //var multiQ = chaptersToCreate.Select(
                            //e => new ApplicationMultiQuestion
                            //{
                            //    CorrectAnswerExplanation = e.CorrectAnswerExplanation,
                            //    Duration = e.Duration,
                            //    PossibleAnswers = e.PossibleAnswers.Select(e => new ApplicationAnswer
                            //    {
                            //        Answer = e.Answer,
                            //        IsCorrect = e.IsCorrect
                            //    }).ToList(),
                            //    Question = e.Question,
                            //    Xperience = e.Xperience,
                            //    GameId = trainingId.Value
                            //}).ToList();
                            //foreach (var multi in multiQ)
                            //{
                            //    var result = await _hopSkillsDb.MultiQuestions.AddAsync(multi);
                            //    if (result != null)
                            //    {
                            //        Guid? mutlId = result.Entity.Id;
                            //        if (mutlId.HasValue)
                            //        {
                            //            multi.PossibleAnswers.ForEach(e => e.MultiQuestionId = mutlId.Value);
                            //            await _hopSkillsDb.Answers.AddRangeAsync(multi.PossibleAnswers);
                            //            var imagesFiles = chaptersToCreate.FirstOrDefault(q => q.Question == multi.Question).ImageFiles;
                            //            var audioFiles = chaptersToCreate.FirstOrDefault(q => q.Question == multi.Question).AudioFiles;
                            //            if (imagesFiles.Count != 0)
                            //            {
                            //                var bytes = imagesFiles.Select(Convert.FromBase64String);
                            //                int count = 0;
                            //                foreach (var b in bytes)
                            //                {
                            //                    var blobClient = _containerClient.GetBlobClient($"{trainingId.Value}/{mutlId.Value}_{count++}.png");
                            //                    await blobClient.UploadAsync(BinaryData.FromBytes(b));
                            //                }
                            //            }
                            //            if (audioFiles.Count > 0)
                            //            {
                            //                _containerClient = _blobServiceClient.GetBlobContainerClient(AudioContainerName);
                            //                var bytes = audioFiles.Select(Convert.FromBase64String);
                            //                int count = 0;
                            //                foreach (var b in bytes)
                            //                {
                            //                    var blobClient = _containerClient.GetBlobClient($"{trainingId.Value}/{mutlId.Value}_{count++}.mp3");
                            //                    await blobClient.UploadAsync(BinaryData.FromBytes(b));
                            //                }
                            //            }
                            //        }
                            //    }
                            //}

                        }
                        await _hopSkillsDb.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<TrainingModel>> GetAll()
        {
            //return _hopSkillsDb.Games.Select(g => new GameViewModel
            //{
            //    Id = g.Id,
            //    Title = g.Title,
            //    Duration = g.TotalDuration,
            //    NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
            //    Status = g.Status,
            //    Theme = g.Theme
            //}); 
            return null;
        }

        public Task<IEnumerable<TrainingModel>> GetTrainingsByCustomer(string companyId)
        {
            //try
            //{
            //    var company = _customerService.GetCustomerById(companyId);
            //    if (company is not null)
            //    {
            //        return Task.FromResult(_hopSkillsDb.Games.Where(u => u.CustomerId.ToString().Equals(company.Id)).Select(g => new GameViewModel
            //        {
            //            Id = g.Id,
            //            Title = g.Title,
            //            Duration = g.TotalDuration,
            //            NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
            //            Status = g.Status,
            //            Theme = g.Theme
            //        }).AsEnumerable());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //return Task.FromResult(Enumerable.Empty<GameViewModel>());

            return null;
        }

        public async Task<IEnumerable<TrainingModel>> GetTrainingsByUser(string usermail)
        {
            //try
            //{
            //    var user = await _userService.GetUserAsync(usermail);
            //    return _hopSkillsDb.Games.Where(u => u.UserId.ToString().Equals(user.Id)).Select(g => new GameViewModel
            //    {
            //        Id = g.Id,
            //        Title = g.Title,
            //        Duration = g.TotalDuration,
            //        NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
            //        Status = g.Status,
            //        Theme = g.Theme
            //    });
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //return Enumerable.Empty<GameViewModel>();

            return null;
        }

        public async Task<bool> DeleteTraining(List<TrainingModel> games)
        {
            //try
            //{
            //    foreach (var item in games)
            //    {
            //        var gameToDelete = await _hopSkillsDb.Games
            //    .FirstOrDefaultAsync(g => g.Id.ToString().Equals(item.Id.ToString()));
            //        if (gameToDelete != null)
            //        {
            //            var r = _hopSkillsDb.Games.Remove(gameToDelete);
            //            await _hopSkillsDb.SaveChangesAsync();
            //            var g = await _hopSkillsDb.Games.FindAsync(gameToDelete.Id);
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            return false;
        }

        public async Task<bool> UpdateTrainingStatus(string id)
        {
            //try
            //{
            //    var gameFound = _hopSkillsDb.Games.FirstOrDefault(g => g.Id.ToString().Equals(id));
            //    if (gameFound is not null)
            //    {
            //        var newStatus = gameFound.Status != "Published" ? "Published" : "Draft";
            //        gameFound.Status = newStatus;
            //        _hopSkillsDb.Games.Update(gameFound);
            //        await _hopSkillsDb.SaveChangesAsync();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            return false;
        }
    }
}
