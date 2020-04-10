using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private Dictionary<long, Team> teams;
        private Dictionary<long, Player> players;

        public SoccerTeamsManager()
        {
            teams = new Dictionary<long, Team>();
            players = new Dictionary<long, Player>();
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (teams.ContainsKey(id))
            {
                throw new UniqueIdentifierException();
            }

            var team = new Team()
            {
                Id = id,
                Name = name,
                CreateDate = createDate,
                MainShirtColor = mainShirtColor,
                SecondaryShirtColor = secondaryShirtColor
            };

            teams.Add(id, team);
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (players.ContainsKey(id))
            {
                throw new UniqueIdentifierException();
            }

            Team team = GetTeam(teamId);

            var player = new Player()
            {
                Id = id,
                TeamId = teamId,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            };

            players.Add(id, player);
        }

        private Player GetPlayer(long playerId)
        {
            Player player;

            if(!players.TryGetValue(playerId, out player))
            {
                throw new PlayerNotFoundException();
            }

            return player;
        }

        private Team GetTeam(long teamId)
        {
            Team team;

            if (!teams.TryGetValue(teamId, out team))
            {
                throw new TeamNotFoundException();
            }

            return team;
        }

        public void SetCaptain(long playerId)
        {
            Player player = GetPlayer(playerId);

            teams[player.TeamId].CaptainId = playerId;
        }

        public long GetTeamCaptain(long teamId)
        {
            Team team = GetTeam(teamId);

            if(!team.CaptainId.HasValue)
            {
                throw new CaptainNotFoundException();
            }

            return team.CaptainId.Value;
        }

        public string GetPlayerName(long playerId)
        {
            Player player = GetPlayer(playerId);

            return player.Name;
        }

        public string GetTeamName(long teamId)
        {
            Team team = GetTeam(teamId);

            return team.Name; 
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            Team team = GetTeam(teamId);

            return players.Values
                .Where(x => x.TeamId == teamId)
                .Select(x => x.Id)
                .OrderBy(x => x)
                .ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            Team team = GetTeam(teamId);

            return players.Values
                .Where(x => x.TeamId == teamId)
                .OrderByDescending(x => x.SkillLevel)
                .ThenBy(y => y.Id)
                .First()
                .Id;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            Team team = GetTeam(teamId);

            return players.Values
                .Where(x => x.TeamId == teamId)
                .OrderBy(x => x.BirthDate)
                .ThenBy(y => y.Id)
                .First()
                .Id;
        }

        public List<long> GetTeams()
        {
            List<long> Teams = teams.Values
                .OrderBy(x => x.Id)
                .Select(y => y.Id)
                .ToList();

            if (Teams == null)
            {
                return null;
            }

            return Teams; 
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            Team team = GetTeam(teamId);

            return players.Values
                .OrderByDescending(x => x.Salary)
                .ThenBy(y => y.Id)
                .First()
                .Id;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            Player player = GetPlayer(playerId);

            return player.Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            List<long> TopPlayers = players.Values
                .OrderByDescending(x => x.SkillLevel)
                .ThenBy(z => z.Id)
                .Select(y => y.Id)
                .Take(top)
                .ToList();

            if (TopPlayers == null)
            {
                return null;
            }

            return TopPlayers; 
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            Team team = GetTeam(teamId);
            Team teamVisitor = GetTeam(visitorTeamId);

            if (team.MainShirtColor == teamVisitor.MainShirtColor)
            {
                return teamVisitor.SecondaryShirtColor;
            }
            else
            {
                return teamVisitor.MainShirtColor;
            }
        }

    }
}