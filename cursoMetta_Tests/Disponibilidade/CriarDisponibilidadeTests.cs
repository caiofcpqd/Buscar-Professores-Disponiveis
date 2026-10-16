using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.UseCases;
using cursoMetta_Application.Validators;
using Moq;
using Xunit;

namespace cursoMetta_Tests.Disponibilidade
{
    public class CriarDisponibilidadeTests
    {
        [Fact]
        public async Task Deve_Rejeitar_Disponibilidade_Nos_Dias_Invalidos()
        {
            var mockRepositorio = new Mock<IDisponibilidadeRepository>();
            mockRepositorio.Setup(x => x.ListaDeDisponibilidade()).ReturnsAsync(new List<cursoMetta_Domain.Models.Disponibilidade>
            {
                new cursoMetta_Domain.Models.Disponibilidade
                {
                    ProfessorId = 3,
                    Dia = "sabado",
                    Inicio = new TimeOnly(20, 0, 0),
                    Fim = new TimeOnly(21, 0, 0)
                }
            });

            var validator = new CriarDisponibilidadeValidator();
            var useCase = new CriarDisponibilidade(mockRepositorio.Object, validator);
            var request = new CriarDisponibilidadeRequest
            {
                Dia = "sabado",
                ProfessorId = 3,
                Inicio = new TimeOnly(20, 0, 0),
                Fim = new TimeOnly(21, 0, 0)
            };

            var resultado = await useCase.Criar(request);

            Assert.True(resultado.Erro);
            Assert.Contains(resultado.Mensagens, mensagem => mensagem.Contains("msg"));

            mockRepositorio.Verify(x => x.Criar(It.IsAny<cursoMetta_Domain.Models.Disponibilidade>()), Times.Never);
        }
    }
}