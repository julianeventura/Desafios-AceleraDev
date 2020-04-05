using System;
using System.Collections.Generic;
using System.Text;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    class Jogador
    {
        public long Id { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get; set; }
        public bool Capitao { get; set; }
    }
}
