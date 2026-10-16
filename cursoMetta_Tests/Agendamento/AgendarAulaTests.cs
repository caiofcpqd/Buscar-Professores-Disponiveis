using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.UseCases;
using cursoMetta_Application.Validators;
using Moq;
using Xunit;

namespace cursoMetta_Tests.Agendamento
{
    public class AgendarAulaTests
    {
        [Fact]
        public async Task Deve_Rejeitar_Agendamento_Nos_Dias_Invalidos()
        {
            var mockRepositorio = new Mock<IAgendaRepository>();
            mockRepositorio.Setup(x => x.ListaDeAgendas()).ReturnsAsync(new List<cursoMetta_Domain.Models.AulaMarcada>
            {
                new cursoMetta_Domain.Models.AulaMarcada
                {
                    ProfessorId = 3,
                    Dia = "segunda",
                    Inicio = new TimeOnly(23, 0, 0),
                    Duracao = 30
                }
            });

            var validator = new AgendarAulaValidator();
            var useCase = new AgendarAula(mockRepositorio.Object, validator);
            var request = new AgendarAulaRequest
            {
                ProfessorId = 3,
                Dia = "segunda",
                Inicio = new TimeOnly(23, 0, 0),
                DuracaoMinutos = 30
            };

            var resultado = await useCase.Agendar(request);

            Assert.True(resultado.Erro);
            Assert.Contains(resultado.Mensagens, mensagem => mensagem.Contains("msg"));

            mockRepositorio.Verify(x => x.Agendar(It.IsAny<cursoMetta_Domain.Models.AulaMarcada>()), Times.Never);
        }
    }
}
