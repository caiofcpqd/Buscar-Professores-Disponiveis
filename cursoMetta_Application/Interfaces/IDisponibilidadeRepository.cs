using cursoMetta_Domain.Models;

namespace cursoMetta_Application.Interfaces
{
    public interface IDisponibilidadeRepository
    {
        public Task<Disponibilidade> Criar(Disponibilidade horario);
        public Task<List<Disponibilidade>> ListaDeDisponibilidade();
    }
}
