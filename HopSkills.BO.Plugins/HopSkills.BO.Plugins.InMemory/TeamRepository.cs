﻿using HopSkills.BO.CoreBusiness;
using HopSkills.BO.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.BO.Plugins.InMemory
{
    public class TeamRepository : ITeamRepository
    {
        private List<Team> _teams;

        public TeamRepository()
        {
            _teams = GenerateRandomTeams(15);
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return _teams;
        }

        public Task AddTeamAsync(Team team)
        {
            var maxId = 0;
            if(_teams.Any())
                maxId = _teams.Max(u => u.TeamId);

            team.TeamId = maxId + 1;

            _teams.Add(team);
            return Task.CompletedTask;
        }

        public Task UpdateTeamAsync(Team team)
        {
            var tem = _teams.FirstOrDefault(u => u.TeamId == team.TeamId);
            if (tem is not null)
            {
                tem.Name = team.Name;
                tem.UpdateDate = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTeamAsync(List<Team> teams)
        {
            foreach (var team in teams)
            {
                _teams.Remove(team);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Team>> GetTeamByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_teams);

            return _teams.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Team? GetTeamById(int teamId)
        {
            return _teams.FirstOrDefault(u => u.TeamId == teamId);
        }

        public Task UpdateTeam(Team team)
        {
            var teamFirst = _teams.FirstOrDefault(u => u.TeamId == team.TeamId);
            if (teamFirst is not null)
            {
                teamFirst.Name = team.Name;
            }
            return Task.CompletedTask;
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

        public static Team CreateRandomTeamFalse(int teamId)
        {
            return new Team
            {
                TeamId = teamId,
                Name = RandomString(8),
                IsActive=false,
                NumberOfUser = random.Next(1, 100)
            };
        }

        public static Team CreateRandomTeamTrue(int teamId)
        {
            return new Team
            {
                TeamId = teamId,
                Name = RandomString(8),
                NumberOfUser = random.Next(1, 100)
            };
        }

        public static List<Team> GenerateRandomTeams(int count)
        {
            var users = new List<Team>();
            for (int i = 1; i <= count; i++)
            {
                var newTeam = CreateRandomTeamFalse(i);
                newTeam.IsActive = i % 2 == 0;
                users.Add(newTeam);
            }
            return users;
        }

       
    }
}