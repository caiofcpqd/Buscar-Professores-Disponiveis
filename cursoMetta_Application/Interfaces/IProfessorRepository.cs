using cursoMetta_Domain.Models;

namespace cursoMetta_Application.Interfaces
{
    public interface IProfessorRepository
    {
        public Task<Professor> CadastrarProfessor(Professor professor);
        public Task<List<Professor>> BuscarProfessorDisponivel();
        public Task<bool> ProfessorExiste(string Nome);
    }
}
