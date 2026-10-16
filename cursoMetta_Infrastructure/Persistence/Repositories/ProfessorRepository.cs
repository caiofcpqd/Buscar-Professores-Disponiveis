using cursoMetta_Application.Interfaces;
using cursoMetta_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoMetta_Infrastructure.Persistence.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly WebDbContext _webDbContext;

        public ProfessorRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public async Task<List<Professor>> BuscarProfessorDisponivel()
        {
            return await _webDbContext.professores.Include(p => p.HorariosDisponiveis).Include(p => p.AulasMarcadas).ToListAsync();
        }

        public async Task<Professor> CadastrarProfessor(Professor professor)
        {
            await _webDbContext.professores.AddAsync(professor);
            await _webDbContext.SaveChangesAsync();
            return professor;
        }

        public async Task<bool> ProfessorExiste(string Nome)
        {
            var professor = await _webDbContext.professores.AnyAsync(x => x.Nome == Nome);
            return professor;
        }
    }
}
