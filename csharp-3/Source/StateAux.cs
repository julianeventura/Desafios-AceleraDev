using System;

namespace Codenation.Challenge
{
    public class StateAux : IComparable<StateAux>
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public double Size { get; set; }

        public int CompareTo(StateAux other)
        {
            int compare = this.Size.CompareTo(other.Size);

            return -compare;
        }

    }

}
