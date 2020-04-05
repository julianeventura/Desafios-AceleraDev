using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class CompanyModelTest : ModelBaseTest
    {
        public CompanyModelTest():base(new CodenationContext())
        {
            base.Model = "Codenation.Challenge.Models.Company";
            base.Table = "company";
        }

        [Fact]
        public void Deveria_ter_Tabela()
        {
            AssertTable();
        }

        [Fact]
        public void Devera_Ter_Primary_Key()
        {
            ComparePrimaryKeys("Endereco_Id");
        }

        [Theory]
        [InlineData("Id", false, typeof(int), null)]
        [InlineData("Name", false, typeof(string), 100)]

        public void Devera_Ter_Campos(string campoNome, bool ehNulo, Type campoTipo, int? campoTamanho)
        {
            CompararCampos(campoNome, ehNulo, campoTipo, campoTamanho);
        }

    }
}