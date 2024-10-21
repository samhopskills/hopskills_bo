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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
                                            var bytes = imagesFiles.Select(i => Convert.FromBase64String(i.Content));
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
                                            var bytes = audioFiles.Select(i => Convert.FromBase64String(i.Content));
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

        public async Task UpdateGamePartial(GameChangesModel changes)
        {
            var gameToUpdate = await _hopSkillsDb.Games.FindAsync(changes.Id);
            if (gameToUpdate == null)
            {
                throw new Exception("Game not found");
            }

            // Update main game properties
            if (!string.IsNullOrEmpty(changes.Title)) gameToUpdate.Title = changes.Title;
            if (!string.IsNullOrEmpty(changes.Description)) gameToUpdate.Description = changes.Description;
            if (!string.IsNullOrEmpty(changes.Theme)) gameToUpdate.Theme = changes.Theme;
            if (!string.IsNullOrEmpty(changes.ElligibleSub)) gameToUpdate.ElligibleSub = changes.ElligibleSub;
            if (!string.IsNullOrEmpty(changes.Status)) gameToUpdate.Status = changes.Status;
            if (!string.IsNullOrEmpty(changes.DifficultyLevel)) gameToUpdate.DifficultyLevel = changes.DifficultyLevel;
            if (!string.IsNullOrEmpty(changes.PriorGame)) gameToUpdate.PriorGame = changes.PriorGame;
            if (changes.TotalDuration != default) gameToUpdate.TotalDuration = changes.TotalDuration;
            if (changes.TotalXperience != 0) gameToUpdate.TotalXp = changes.TotalXperience.HasValue ? changes.TotalXperience.Value : 0;

            // Update image if changed
            if (changes.Image != null)
            {
                await UpdateGameImage(gameToUpdate.Id, changes.Image);
            }

            // Update questions
            if (changes.Questions != null)
            {
                foreach (var questionChange in changes.Questions)
                {
                    switch (questionChange.ChangeType)
                    {
                        case "Added":
                            await AddNewQuestion(gameToUpdate.Id, questionChange);
                            break;
                        case "Updated":
                            await UpdateExistingQuestion(gameToUpdate.Id, questionChange);
                            break;
                        case "Deleted":
                            await DeleteQuestion(gameToUpdate.Id, questionChange);
                            break;
                    }
                }
            }
            await _hopSkillsDb.SaveChangesAsync();
        }

        private async Task UpdateGameImage(Guid gameId, EditGameImageViewModel image)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
            var blobClient = containerClient.GetBlobClient($"{gameId}.png");

            BlobUploadOptions options = new()
            {
                Tags = new Dictionary<string, string>
        {
            {"Title", image.Title},
            {"Date", DateTime.UtcNow.ToShortDateString()}
        }
            };

            var bytes = Convert.FromBase64String(image.Content);
            await blobClient.UploadAsync(BinaryData.FromBytes(bytes), options);
        }

        private async Task UpdateQuestion(Guid gameId, QuestionChangeModel questionChange)
        {
            switch (questionChange.ChangeType)
            {
                case "Added":
                    await AddNewQuestion(gameId, questionChange);
                    break;
                case "Updated":
                    await UpdateExistingQuestion(gameId, questionChange);
                    break;
                case "Deleted":
                    await DeleteQuestion(gameId, questionChange); // Assuming Question field is used as a unique identifier
                    break;
            }
        }

        private async Task AddNewQuestion(Guid gameId, QuestionChangeModel questionChange)
        {
            var newQuestion = new ApplicationMultiQuestion
            {
                GameId = gameId,
                Question = questionChange.Question,
                CorrectAnswerExplanation = questionChange.CorrectAnswerExplanation,
                Duration = new TimeOnly(0, questionChange.Duration.Value.Minutes, questionChange.Duration.Value.Seconds),
                Xperience = questionChange.Xperience.Value,
                PossibleAnswers = questionChange.Answers.Select(a => new ApplicationAnswer
                {
                    Answer = a.Answer,
                    IsCorrect = a.IsCorrect.Value
                }).ToList()
            };

            await _hopSkillsDb.MultiQuestions.AddAsync(newQuestion);
            await UploadQuestionFiles(gameId, newQuestion.Id, questionChange);
        }

        private async Task UpdateExistingQuestion(Guid gameId, QuestionChangeModel questionChange)
        {
            var existingQuestion = await _hopSkillsDb.MultiQuestions
                .FirstOrDefaultAsync(q => q.GameId == gameId && q.Id.ToString() == questionChange.UniqueId.ToLower());

            if (existingQuestion != null)
            {
                if(!string.IsNullOrEmpty(questionChange.Question)
                    && questionChange.Question != existingQuestion.Question) existingQuestion.Question = questionChange.Question;
                if (!string.IsNullOrEmpty(questionChange.CorrectAnswerExplanation)
                   && questionChange.CorrectAnswerExplanation != existingQuestion.CorrectAnswerExplanation) existingQuestion.CorrectAnswerExplanation = questionChange.CorrectAnswerExplanation;
                if (questionChange.Duration is not null) existingQuestion.Duration = new TimeOnly(0, questionChange.Duration.Value.Minutes, questionChange.Duration.Value.Seconds);
                if (questionChange.Xperience.HasValue 
                    && questionChange.Xperience.Value != existingQuestion.Xperience) existingQuestion.Xperience = questionChange.Xperience.Value;

                // Update answers
                var existingAnswers = await _hopSkillsDb.Answers.Where(a => a.MultiQuestionId.ToString() == existingQuestion.Id.ToString()).ToListAsync();
                foreach (var answerChange in questionChange.Answers)
                {
                    var existingAnswer = existingAnswers.FirstOrDefault(a => a.Id.ToString() == answerChange.UniqueId);
                    if (existingAnswer != null)
                    {
                        if(answerChange.ChangeType == "Deleted")
                        {
                            _hopSkillsDb.Answers.Remove(existingAnswer);
                        }
                        else
                        {
                            existingAnswer.IsCorrect = answerChange.IsCorrect.Value;
                            existingAnswer.Answer = answerChange.Answer;
                        }
                    }
                    else
                    {
                        existingAnswers.Add(new ApplicationAnswer
                        {
                            Answer = answerChange.Answer,
                            IsCorrect = answerChange.IsCorrect.Value
                        });
                    }
                }

                await UploadQuestionFiles(gameId, existingQuestion.Id, questionChange);
            }
        }

        private async Task DeleteQuestion(Guid gameId, QuestionChangeModel questionChange)
        {
            var questionToDelete = await _hopSkillsDb.MultiQuestions.FirstOrDefaultAsync(q => q.GameId == gameId 
            && q.Id.ToString() == questionChange.UniqueId.ToLower());
            if (questionToDelete != null)
            {
                _hopSkillsDb.MultiQuestions.Remove(questionToDelete);
            }
        }

        private async Task UploadQuestionFiles(Guid gameId, Guid questionId, QuestionChangeModel questionChange)
        {
            try
            {
                if (questionChange.ImageFiles != null)
                {
                    var containerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
                    if (questionChange.ImageFiles.Count != 0)
                    {
                        for (int i = 0; i < questionChange.ImageFiles.Count; i++)
                        {
                            if(!questionChange.ImageFiles[i].Delete)
                            {
                                var blobClient = containerClient.GetBlobClient($"{gameId}/{questionId}_{i}.png");
                                if (!await blobClient.ExistsAsync())
                                {
                                    var bytes = Convert.FromBase64String(questionChange.ImageFiles[i].Content);
                                    await blobClient.UploadAsync(BinaryData.FromBytes(bytes));
                                }
                            }
                            else
                                await containerClient.DeleteBlobIfExistsAsync($"{gameId}/{questionId}_{i}.png");
                        }
                    }
                    else
                    {
                        var blobExist = true;
                        var i = 0;
                        do
                        {
                            blobExist = await containerClient.DeleteBlobIfExistsAsync($"{gameId}/{questionId}_{i}.png");
                            i++;
                        } while (blobExist);
                    }
                }

                if (questionChange.AudioFiles != null)
                {
                    var containerClient = _blobServiceClient.GetBlobContainerClient(AudioContainerName);
                    if (questionChange.AudioFiles.Count != 0)
                    {
                        for (int i = 0; i < questionChange.AudioFiles.Count; i++)
                        {
                            if(!questionChange.AudioFiles[i].Delete)
                            {
                                var blobClient = containerClient.GetBlobClient($"{gameId}/{questionId}_{i}.mp3");
                                if (!await blobClient.ExistsAsync())
                                {
                                    var bytes = Convert.FromBase64String(questionChange.AudioFiles[i].Content);
                                    await blobClient.UploadAsync(BinaryData.FromBytes(bytes));
                                }
                            }
                            else
                                await containerClient.DeleteBlobIfExistsAsync($"{gameId}/{questionId}_{i}.mp3");
                        }
                    }
                    else
                    {
                        var blobExist = true;
                        var i = 0;
                        do
                        {
                            blobExist = await containerClient.DeleteBlobIfExistsAsync($"{gameId}/{questionId}_{i}.mp3");
                            i++;
                        } while (blobExist);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task<(List<GameFileModel> ImageFiles, List<GameFileModel> AudioFiles)> GetQuestionFiles(string gameId, string questionId)
        {
            List<GameFileModel> imageFiles = [];
            List<GameFileModel> audioFiles = [];

            // Récupérer les images
            var imageContainerClient = _blobServiceClient.GetBlobContainerClient(ImageContainerName);
            for (int i = 0; ; i++)
            {
                var blobClient = imageContainerClient.GetBlobClient($"{gameId}/{questionId.ToLower()}_{i}.png");
                if (!await blobClient.ExistsAsync())
                {
                    break;
                }
                var blobDownloadInfo = await blobClient.DownloadContentAsync();
                var content = Convert.ToBase64String(blobDownloadInfo.Value.Content);
                imageFiles.Add(new GameFileModel
                {
                    Content = content
                });
            }

            // Récupérer les fichiers audio
            var audioContainerClient = _blobServiceClient.GetBlobContainerClient(AudioContainerName);
            for (int i = 0; ; i++)
            {
                var blobClient = audioContainerClient.GetBlobClient($"{gameId}/{questionId.ToLower()}_{i}.mp3");
                if (!await blobClient.ExistsAsync())
                {
                    break;
                }
                var blobDownloadInfo = await blobClient.DownloadContentAsync();
                var content = Convert.ToBase64String(blobDownloadInfo.Value.Content);
                audioFiles.Add(new GameFileModel
                {
                    Content = content
                });
            }

            return (imageFiles, audioFiles);
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
                            
                            UniqueId = m.Id.ToString(),
                            CorrectAnswerExplanation = m.CorrectAnswerExplanation,
                            Duration = m.Duration,
                            Question = m.Question,
                            Xperience = m.Xperience,
                            PossibleAnswers = m.PossibleAnswers.Select(
                                p => new AnswerModel
                                {
                                    UniqueId = p.Id.ToString(),
                                    Answer = p.Answer,
                                    IsCorrect = p.IsCorrect
                                }
                                ).ToList()
                        }).ToList();
                    foreach (var q in editMultiQ)
                    {
                        (q.ImageFiles, q.AudioFiles) = await GetQuestionFiles(id, q.UniqueId);
                    }
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

        public async Task<Response<bool>> DeleteImageFromGame(string id)
        {
            try
            {
                BlobContainerClient _containerClient = _blobServiceClient
                            .GetBlobContainerClient(ImageContainerName);
                var blobClient = _containerClient.GetBlobClient($"{id}.png");
                return await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR] : {ex} {ex.Message}");
            }
            return null;
        }

        public async Task<Response<bool>> UploadImageForGame(EditGameImage Image, string Id)
        {
            try
            {
                BlobContainerClient _containerClient = _blobServiceClient
                           .GetBlobContainerClient(ImageContainerName);
                if(Image is not null)
                {
                    if (!string.IsNullOrEmpty(Image.Content))
                    {
                        var blobClient = _containerClient.GetBlobClient($"{Id}.png");
                        BlobUploadOptions options = new()
                        {
                            Tags = new Dictionary<string, string>
                                {
                                    {"Title",  Image.Title},
                                    {"Date",  DateTime.UtcNow.ToShortDateString()}
                                }
                        };
                        var bytes = Convert.FromBase64String(Image.Content);
                        await blobClient.UploadAsync(BinaryData.FromBytes(bytes), options);
                        _logger.LogInformation("[Game Image added to container]");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR] : {ex} {ex.Message}");
            }
            return null;
        }

    }
}
