using HopSkills.BackOffice.Client.ViewModels;
using HopSkills.BackOffice.Data;
using HopSkills.BackOffice.Model;
using HopSkills.BackOffice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HopSkills.BackOffice.Services
{
    public class GameService : IGameService
    {
        private readonly HopSkillsDbContext _hopSkillsDb;
        private readonly IUserService _userService;

        public GameService(HopSkillsDbContext hopSkillsDb, IUserService userService)
        {
            _hopSkillsDb = hopSkillsDb;
            _userService = userService;
        }
        public async Task AddGame(CreateGameModel createGameModel)
        {
            var creator = _hopSkillsDb.Users.FirstOrDefault(u => u.Email == createGameModel.UserEmail);
            if(creator != null)
            {
                var newAppGame = new ApplicationGame
                {
                    Duration = createGameModel.TotalDuration,
                    Status = createGameModel.Status.ToString(),
                    Theme = createGameModel.Theme.ToString(),
                    TotalXp = createGameModel.TotalXperience,
                    Title = createGameModel.Title,
                    UserId = new Guid(creator.Id),
                    CustomerId = creator.CustomerId
                };
                var resultGame = await _hopSkillsDb.Games.AddAsync(newAppGame);
                await _hopSkillsDb.SaveChangesAsync();
                var gameId = _hopSkillsDb.Games.FirstOrDefault(g => g.Title == newAppGame.Title).Id;
                if (createGameModel.multipleQuestions.Any())
                {
                    var multiQ = createGameModel.multipleQuestions.Select(
                    e => new ApplicationMultiQuestion
                    {
                        CorrectAnswerExplanation = e.CorrectAnswerExplanation,
                        Duration = e.Duration,
                        PossibleAnswers = e.PossibleAnswers.Select(p => new ApplicationAnswer
                        {
                            Answer = p.Answer,
                            IsCorrect = p.IsCorrect
                        }).ToList(),
                        Question = e.Question,
                        Xperience = e.Xperience,
                        GameId = gameId
                    }).ToList();
                }
            }
            
            
        }

        public async Task<GameViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<GameViewModel> GetGamesByCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
