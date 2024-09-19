using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Migrations;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using HopSkills.BO.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace HopSkills.BackOffice.Services
{
    public class GameService(HopSkillsDbContext hopSkillsDb,
        IUserService userService,
        ICustomerService customerService,
        BlobServiceClient blobServiceClient,
        ILogger<GameService> logger) : IGameService
    {
        private readonly HopSkillsDbContext _hopSkillsDb = hopSkillsDb;
        private readonly IUserService _userService = userService;
        private readonly ICustomerService _customerService = customerService;
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
        private readonly ILogger<GameService> _logger = logger;
        private const string ImageContainerName = "images";
        private const string AudioContainerName = "audio";

        public async Task AddGame(CreateGameModel createGameModel)
        {
            _logger.LogInformation($"[Game][Create] : {JsonConvert.SerializeObject(createGameModel)}");
            try
            {
                var creator = _hopSkillsDb.Users
                    .FirstOrDefault(u => u.Email == createGameModel.Creator);
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
                        Description = createGameModel.Description,
                        ImageUri = createGameModel.Image?.Title,
                        DifficultyLevel = createGameModel.DifficultyLevel,
                        ElligibleSub = createGameModel.ElligibleSub,
                        CreatedOn = DateTime.UtcNow,
                        PriorGame = createGameModel.PriorGame
                    };
                    var resultGame = await _hopSkillsDb.Games.AddAsync(newAppGame);
                    Guid? gameId = resultGame.Entity.Id;
                    if (gameId != null)
                    {
                        _logger.LogInformation("[Game Added To DB OK]");
                        BlobContainerClient _containerClient = _blobServiceClient
                            .GetBlobContainerClient(ImageContainerName);
                        var creaMutliQ = createGameModel.multipleQuestions;
                        if (createGameModel.Image is not null)
                        {
                            var blobClient = _containerClient.GetBlobClient($"{gameId.Value}.png");
                            BlobUploadOptions options = new()
                            {
                                Tags = new Dictionary<string, string>
                                {
                                    {"Title",  createGameModel.Image?.Title},
                                    {"Date",  DateTime.UtcNow.ToShortDateString()}
                                }
                            };
                            var bytes = Convert.FromBase64String(createGameModel.Image.Content);
                            await blobClient.UploadAsync(BinaryData.FromBytes(bytes), options);
                            _logger.LogInformation("[Game Image added to container]");
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
                                            _logger.LogInformation("[Multi Form Images Added to Container]");
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
                                            _logger.LogInformation("[Multi Form Audios Added to Container]");
                                        }
                                    }
                                }
                            }
                        }
                        await _hopSkillsDb.SaveChangesAsync();
                        _logger.LogInformation("[Save Changes]");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR] : {ex} {ex.Message}");
            }
        }

        public async Task EditGame(EditGameModel editGameModel)
        {
            try
            {
                var creator = await _hopSkillsDb.Users.SingleAsync(u => u.Email == editGameModel.Creator);
                if (creator != null) {
                    var gameToEdit = await _hopSkillsDb.Games.SingleAsync(g => g.Id == editGameModel.Id);
                    if (gameToEdit != null)
                    {
                        if (creator != null)
                        {

                            gameToEdit.TotalDuration = editGameModel.TotalDuration;
                            gameToEdit.Status = editGameModel.Status.ToString();
                            gameToEdit.Theme = editGameModel.Theme.ToString();
                            gameToEdit.TotalXp = editGameModel.TotalXperience;
                            gameToEdit.Title = editGameModel.Title;
                            gameToEdit.UserId = new Guid(creator.Id);
                            gameToEdit.Description = editGameModel.Description;
                            gameToEdit.DifficultyLevel = editGameModel.DifficultyLevel;
                            gameToEdit.ElligibleSub = editGameModel.ElligibleSub;
                            gameToEdit.PriorGame = editGameModel.PriorGame;

                            //BlobContainerClient _containerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
                            //var editMutliQ = editGameModel.multipleQuestions;
                            //var blobClient = _containerClient.GetBlobClient($"{editedAppGame.Id}.png");
                            //var bytes = Convert.FromBase64String(editGameModel.Image);
                            //await blobClient.UploadAsync(BinaryData.FromBytes(bytes));
                            //if (editMutliQ.Count != 0)
                            //{
                            //    var multiQ = editMutliQ.Select(
                            //    e => new ApplicationMultiQuestion
                            //    {
                            //        CorrectAnswerExplanation = e.CorrectAnswerExplanation,
                            //        Duration = e.Duration,
                            //        PossibleAnswers = e.PossibleAnswers.Select(e => new ApplicationAnswer
                            //        {
                            //            Answer = e.Answer,
                            //            IsCorrect = e.IsCorrect
                            //        }).ToList(),
                            //        Question = e.Question,
                            //        Xperience = e.Xperience,
                            //        GameId = gameId.Value
                            //    }).ToList();
                            //    foreach (var multi in multiQ)
                            //    {
                            //        var result = await _hopSkillsDb.MultiQuestions.AddAsync(multi);
                            //        if (result != null)
                            //        {
                            //            Guid? mutlId = result.Entity.Id;
                            //            if (mutlId.HasValue)
                            //            {
                            //                multi.PossibleAnswers.ForEach(e => e.MultiQuestionId = mutlId.Value);
                            //                await _hopSkillsDb.Answers.AddRangeAsync(multi.PossibleAnswers);
                            //                var imagesFiles = editMutliQ.FirstOrDefault(q => q.Question == multi.Question).ImageFiles;
                            //                var audioFiles = editMutliQ.FirstOrDefault(q => q.Question == multi.Question).AudioFiles;
                            //                if (imagesFiles.Count != 0)
                            //                {
                            //                    var bytes = imagesFiles.Select(Convert.FromBase64String);
                            //                    int count = 0;
                            //                    foreach (var b in bytes)
                            //                    {
                            //                        var blobClient = _containerClient.GetBlobClient($"{gameId.Value}/{mutlId.Value}_{count++}.png");
                            //                        await blobClient.UploadAsync(BinaryData.FromBytes(b));
                            //                    }
                            //                }
                            //                if (audioFiles.Count > 0)
                            //                {
                            //                    _containerClient = _blobServiceClient.GetBlobContainerClient(AudioContainerName);
                            //                    var bytes = audioFiles.Select(Convert.FromBase64String);
                            //                    int count = 0;
                            //                    foreach (var b in bytes)
                            //                    {
                            //                        var blobClient = _containerClient.GetBlobClient($"{gameId.Value}/{mutlId.Value}_{count++}.mp3");
                            //                        await blobClient.UploadAsync(BinaryData.FromBytes(b));
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            await _hopSkillsDb.SaveChangesAsync();
                        }
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

        public async Task<EditGameModel?> GetGameById(string id)
        {
            try
            {
                var gameFound = await _hopSkillsDb.Games.FirstOrDefaultAsync(g => g.Id.ToString().Equals(id));
                var creator = await _hopSkillsDb.Users.FirstOrDefaultAsync(u => u.Id.ToString().Equals(gameFound.UserId.ToString()));
                if (gameFound is not null && creator is not null)
                {
                    var gameToEdit = new EditGameModel
                    {
                        Id = gameFound.Id,
                        Title = gameFound.Title,
                        Status = gameFound.Status,
                        Theme = gameFound.Theme,
                        TotalDuration = gameFound.TotalDuration,
                        Creator = creator.Email,
                        Description = gameFound.Description,
                        TotalXperience = gameFound.TotalXp,
                        PriorGame = gameFound.PriorGame,
                        DifficultyLevel = gameFound.DifficultyLevel,
                        ElligibleSub = gameFound.ElligibleSub
                    };
                    var _containerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
                    BlobClient blobClient = _containerClient.GetBlobClient($"{gameFound.Id}.png");
                    if (blobClient.Exists())
                    {
                        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
                        Response<GetBlobTagResult> tagsResponse = await blobClient.GetTagsAsync();
                        gameToEdit.Image = new EditGameImage
                        {
                            Title = tagsResponse.Value.Tags.FirstOrDefault(t => t.Key.Equals("Title")).Value,
                            Content = Convert.ToBase64String(downloadResult.Content)
                        };
                    }

                    var editMultiQ = _hopSkillsDb
                        .MultiQuestions.Where(c => c.GameId.ToString().Equals(id))
                        .Select(m => 
                        new MultipleQuestionsModel
                        {
                            CorrectAnswerExplanation = m.CorrectAnswerExplanation,
                            Duration = m.Duration,
                            Question = m.Question,
                            Xperience = m.Xperience,
                            PossibleAnswers = m.PossibleAnswers.Select(
                                p => new AnswerModel
                                {
                                    Answer = p.Answer,
                                    IsCorrect = p.IsCorrect
                                }
                                ).ToList()
                        }).ToList();
                    gameToEdit.multipleQuestions = editMultiQ;
                    return gameToEdit;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);    
            }
            return null;
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
