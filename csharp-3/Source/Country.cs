using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Country
    {
        public const int qtdeEstados = 10;
        
        public State[] Top10StatesByArea()
        {
            var Estados = new List<StateAux>
            {
                new StateAux() {Acronym = "AC", Name = "Acre", Size = 164123.040},
                new StateAux() {Acronym = "AL", Name = "Alagoas", Size = 27778.506},
                new StateAux() {Acronym = "AP", Name = "Amapá", Size = 142828.521},
                new StateAux() {Acronym = "AM", Name = "Amazonas", Size = 1559159.148},
                new StateAux() {Acronym = "BA", Name = "Bahia", Size = 564733.117},
                new StateAux() {Acronym = "CE", Name = "Ceará", Size = 148920.472},
                new StateAux() {Acronym = "DF", Name = "Distrito Federal", Size = 5779.999},
                new StateAux() {Acronym = "ES", Name = "Espírito Santo", Size = 46095.583},
                new StateAux() {Acronym = "GO", Name = "Goias", Size = 340111.783},
                new StateAux() {Acronym = "MA", Name = "Maranhâo", Size = 331937.450},
                new StateAux() {Acronym = "MT", Name = "Mato Grosso", Size = 903366.192},
                new StateAux() {Acronym = "MS", Name = "Mato Grosso do Sul", Size = 357145.532},
                new StateAux() {Acronym = "MG", Name = "Minas Gerais", Size = 586522.122},
                new StateAux() {Acronym = "PA", Name = "Pará", Size = 1247954.666},
                new StateAux() {Acronym = "PB", Name = "Paraíba", Size = 56585.000},
                new StateAux() {Acronym = "PR", Name = "Paraná", Size = 199307.922},
                new StateAux() {Acronym = "PE", Name = "Pernambuco", Size = 98311.616},
                new StateAux() {Acronym = "PI", Name = "Piauí", Size = 251577.738},
                new StateAux() {Acronym = "RJ", Name = "Rio de Janeiro", Size = 43780.172},
                new StateAux() {Acronym = "RN", Name = "Rio Grande do Norte", Size = 52811.047},
                new StateAux() {Acronym = "RS", Name = "Rio Grande do Sul", Size = 281730.223},
                new StateAux() {Acronym = "RO", Name = "Rondônia", Size = 237590.547},
                new StateAux() {Acronym = "RR", Name = "Roraima", Size = 224300.506},
                new StateAux() {Acronym = "SC", Name = "Santa Catarina", Size = 95736.165},
                new StateAux() {Acronym = "SP", Name = "São Paulo", Size = 248222.362},
                new StateAux() {Acronym = "SE", Name = "Sergipe", Size = 21915.116},
                new StateAux() {Acronym = "TO", Name = "Tocantins", Size = 277720.520},
            };
            
            Estados.Sort();

            State[] ListaEstados = new State[qtdeEstados];

            for (int i = 0; i < qtdeEstados; i++)
            {
                ListaEstados[i] = new State(Estados[i].Name, Estados[i].Acronym);
            }

            return ListaEstados;
        }
    }
}
