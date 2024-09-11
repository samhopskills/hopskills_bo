using Azure.Storage.Blobs;
using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using HopSkills.BO.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;

namespace HopSkills.BackOffice.Services
{
    public class GameService : IGameService
    {
        private readonly HopSkillsDbContext _hopSkillsDb;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<GameService> _logger;
        private const string ImageContainerName = "images";
        private const string AudioContainerName = "audio";
        public GameService(HopSkillsDbContext hopSkillsDb,
            IUserService userService,
            ICustomerService customerService,
            BlobServiceClient blobServiceClient,
            ILogger<GameService> logger)
        {
            _hopSkillsDb = hopSkillsDb;
            _userService = userService;
            _customerService = customerService;
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }
        public async Task AddGame(CreateGameModel createGameModel)
        {
            _logger.LogInformation("Add a game");
            try
            {
                var creator = _hopSkillsDb.Users.FirstOrDefault(u => u.Email == createGameModel.Creator);
                if (creator != null)
                {
                    var newAppGame = new ApplicationGame
                    {
                        TotalDuration = createGameModel.TotalDuration,
                        Status = createGameModel.Status.ToString(),
                        Theme = createGameModel.Theme.ToString(),
                        TotalXp = createGameModel.TotalXperience,
                        Title = createGameModel.Title,
                        UserId = new Guid(creator.Id),
                        CustomerId = creator.CustomerId,
                        ImageUri = ""
                    };
                    var resultGame = await _hopSkillsDb.Games.AddAsync(newAppGame);
                    Guid? gameId = resultGame.Entity.Id;
                    if (gameId != null)
                    {
                        BlobContainerClient _containerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
                        var creaMutliQ = createGameModel.multipleQuestions;
                        if (!string.IsNullOrEmpty(createGameModel.Image))
                        {
                            var blobClient = _containerClient.GetBlobClient($"{gameId.Value}.png");
                            var bytes = Convert.FromBase64String(createGameModel.Image);
                            await blobClient.UploadAsync(BinaryData.FromBytes(bytes));
                        }
                        if (creaMutliQ.Count != 0)
                        {
                            var multiQ = creaMutliQ.Select(
                            e => new ApplicationMultiQuestion
                            {
                                CorrectAnswerExplanation = e.CorrectAnswerExplanation,
                                Duration = e.Duration,
                                PossibleAnswers = e.PossibleAnswers.Select(e => new ApplicationAnswer
                                {
                                    Answer = e.Answer,
                                    IsCorrect = e.IsCorrect
                                }).ToList(),
                                Question = e.Question,
                                Xperience = e.Xperience,
                                GameId = gameId.Value
                            }).ToList();
                            foreach (var multi in multiQ)
                            {
                                var result = await _hopSkillsDb.MultiQuestions.AddAsync(multi);
                                if (result != null)
                                {
                                    Guid? mutlId = result.Entity.Id;
                                    if (mutlId.HasValue)
                                    {
                                        multi.PossibleAnswers.ForEach(e => e.MultiQuestionId = mutlId.Value);
                                        await _hopSkillsDb.Answers.AddRangeAsync(multi.PossibleAnswers);
                                        var imagesFiles = creaMutliQ.FirstOrDefault(q => q.Question == multi.Question).ImageFiles;
                                        var audioFiles = creaMutliQ.FirstOrDefault(q => q.Question == multi.Question).AudioFiles;
                                        if (imagesFiles.Count != 0)
                                        {
                                            var bytes = imagesFiles.Select(Convert.FromBase64String);
                                            int count = 0;
                                            foreach (var b in bytes)
                                            {
                                                var blobClient = _containerClient.GetBlobClient($"{gameId.Value}/{mutlId.Value}_{count++}.png");
                                                await blobClient.UploadAsync(BinaryData.FromBytes(b));
                                            }
                                        }
                                        if(audioFiles.Count > 0)
                                        {
                                            _containerClient = _blobServiceClient.GetBlobContainerClient(AudioContainerName);
                                            var bytes = audioFiles.Select(Convert.FromBase64String);
                                            int count = 0;
                                            foreach (var b in bytes)
                                            {
                                                var blobClient = _containerClient.GetBlobClient($"{gameId.Value}/{mutlId.Value}_{count++}.mp3");
                                                await blobClient.UploadAsync(BinaryData.FromBytes(b));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        await _hopSkillsDb.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR] : {ex.Message}");
            }
        }

        public async Task<IEnumerable<GameViewModel>> GetAll()
        {
            return _hopSkillsDb.Games.Select(g => new GameViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Duration = g.TotalDuration,
                NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
                Status = g.Status,
                Theme = g.Theme
            });
        }

        public Task<IEnumerable<GameViewModel>> GetGamesByCustomer(string companyId)
        {
            try
            {
                var company = _customerService.GetCustomerById(companyId);
                if (company is not null)
                {
                    return Task.FromResult(_hopSkillsDb.Games.Where(u => u.CustomerId.ToString().Equals(company.Id)).Select(g => new GameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Duration = g.TotalDuration,
                        NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
                        Status = g.Status,
                        Theme = g.Theme
                    }).AsEnumerable());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.FromResult(Enumerable.Empty<GameViewModel>());
        }

        public async Task<IEnumerable<GameViewModel>> GetGamesByUser(string usermail)
        {
            try
            {
                var user = await _userService.GetUserAsync(usermail);
                return _hopSkillsDb.Games.Where(u => u.UserId.ToString().Equals(user.Id)).Select(g => new GameViewModel
                {   
                    Id = g.Id,
                    Title = g.Title,
                    Duration = g.TotalDuration,
                    NumberOfQuestion = _hopSkillsDb.MultiQuestions.Where(m => m.GameId == g.Id).Select(m => m).Count(),
                    Status = g.Status,
                    Theme = g.Theme
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Enumerable.Empty<GameViewModel>();
        }

        public async Task<bool> DeleteGame(List<GameViewModel> games)
        {
            try
            {
                foreach(var item in games)
                {
                    var gameToDelete = await _hopSkillsDb.Games
                .FirstOrDefaultAsync(g => g.Id.ToString().Equals(item.Id.ToString()));
                    if (gameToDelete != null)
                    {
                        var r = _hopSkillsDb.Games.Remove(gameToDelete);
                        await _hopSkillsDb.SaveChangesAsync();
                        var g = await _hopSkillsDb.Games.FindAsync(gameToDelete.Id);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> UpdateGameStatus(string id)
        {
            try
            {
                var gameFound = _hopSkillsDb.Games.FirstOrDefault(g => g.Id.ToString().Equals(id));
                if(gameFound is not null)
                {
                    var newStatus = gameFound.Status != "Published" ? "Published" : "Draft";
                    gameFound.Status = newStatus;
                    _hopSkillsDb.Games.Update(gameFound);
                    await _hopSkillsDb.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
