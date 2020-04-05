using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        List<Time> Teams = new List<Time>();
        List<Jogador> Players = new List<Jogador>();

        public SoccerTeamsManager()
        {    

        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            Time team = new Time();
            team.Id = id;
            team.Name = name;
            team.DataCriacao = createDate;
            team.CorUniformePrincipal = mainShirtColor;
            team.CorUniformeSecundario = secondaryShirtColor;

            if (Teams.Any(t => t.Id == team.Id))
            {
                throw new Codenation.Challenge.Exceptions.UniqueIdentifierException();
            }
            else
            {
                Teams.Add(team);
            }
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            Jogador player = new Jogador();
            Time team = new Time();

            player.Id = id;
            player.TeamId = teamId;
            player.Name = name;
            player.BirthDate = birthDate;
            player.SkillLevel = skillLevel;
            player.Salary = salary;

            if(!Players.Contains(player))
            {
                Players.Add(player);
            }
            else
            {
                throw new Codenation.Challenge.Exceptions.UniqueIdentifierException();
            }

            if(Teams.Contains(team))
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

        }

        public void SetCaptain(long playerId)
        {
            var jogador = Players.Find(p => p.Id == playerId);
            if (jogador == null)
            {
                throw new Codenation.Challenge.Exceptions.PlayerNotFoundException();
            }
            var jogadorCapitao = Players.Find(t => t.Id == playerId);
            jogadorCapitao.Capitao = jogadorCapitao.Id == playerId;
        }

        public long GetTeamCaptain(long teamId)
        {
            var time = Teams.Find(t => t.Id == teamId);
            if (time == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            foreach (Jogador player in Players)
            {
                if (player.Capitao)
                {
                    return player.Id;
                }
            }

            throw new Codenation.Challenge.Exceptions.CaptainNotFoundException();
        }

        public string GetPlayerName(long playerId)
        {
            foreach (Jogador jogador in Players)
            {
                if (jogador.Id == playerId)
                {
                    return jogador.Name;
                }
            }

            throw new Codenation.Challenge.Exceptions.PlayerNotFoundException();
        }

        public string GetTeamName(long teamId)
        {
            foreach (Time time in Teams)
            {
                if (time.Id == teamId)
                {
                    return time.Name;
                }
            }

            throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            var time = Teams.Find(t => t.Id == teamId);
            if (time == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            List<long> ListaId = Players.OrderBy(p => p.Id).Select(p => p.Id).ToList();

            return ListaId;
        }

        public long GetBestTeamPlayer(long teamId)
        {
            var time = Teams.Find(t => t.Id == teamId);
            if(time == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            return Players.OrderByDescending(p => p.SkillLevel).ThenBy(p => p.Id).First().Id;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            var jogadorVelho = Players.Find(jv => jv.TeamId == teamId);
            
            if(jogadorVelho == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            return Players.OrderBy(jv => jv.BirthDate).Select(jv => jv.Id).First();
        }

        public List<long> GetTeams()
        {
            List<long> ListaId = Teams.OrderBy(t => t.Id).Select(p => p.Id).ToList();
            
            if (ListaId == null)
            {
                return ListaId = null;
            }

            return ListaId;
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            var salarioJogador = Players.Find(sj => sj.TeamId == teamId);

            if (salarioJogador == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            return Players.OrderByDescending(sj => sj.Salary).ThenBy(sj => sj.Id)
                .Select(jv => jv.Id).First();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            foreach (Jogador jogador in Players)
            {
                if (jogador.Id == playerId)
                {
                    return jogador.Salary;
                }
            }

            throw new Codenation.Challenge.Exceptions.PlayerNotFoundException();
        }

        public List<long> GetTopPlayers(int top)
        {
            List<long> ListaTop = Players.OrderByDescending(j => j.SkillLevel)
                .ThenBy(j => j.Id).Take(top).Select(j => j.Id).ToList();

            if(ListaTop == null)
            {
                return ListaTop = null;
            }
            return ListaTop;

        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            var timeCasa = Teams.Find(t => t.Id == teamId);
            if (timeCasa == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }

            var visitaCasa = Teams.Find(t => t.Id == visitorTeamId);
            if (visitaCasa == null)
            {
                throw new Codenation.Challenge.Exceptions.TeamNotFoundException();
            }   

            if (timeCasa.CorUniformePrincipal == visitaCasa.CorUniformePrincipal)
            {
                return visitaCasa.CorUniformeSecundario;
            }
            else
            {
                return visitaCasa.CorUniformePrincipal;
            }
        }

    }
}
