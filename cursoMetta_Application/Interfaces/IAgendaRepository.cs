using cursoMetta_Domain.Models;

namespace cursoMetta_Application.Interfaces
{
    public interface IAgendaRepository
    {
        public Task<AulaMarcada> Agendar(AulaMarcada aula);
        public Task<List<AulaMarcada>> ListaDeAgendas();
    }
}
