using cursoMetta_Application.Interfaces;
using cursoMetta_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoMetta_Infrastructure.Persistence.Repositories
{
    public class AgendamentoRepository : IAgendaRepository
    {
        private readonly WebDbContext _webDbContext;

        public AgendamentoRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public async Task<AulaMarcada> Agendar(AulaMarcada aula)
        {
            await _webDbContext.aulas.AddAsync(aula);
            await _webDbContext.SaveChangesAsync();
            return aula;
        }

        public async Task<List<AulaMarcada>> ListaDeAgendas()
        {
            var listaAgenda = await _webDbContext.aulas.ToListAsync();
            return listaAgenda;
        }
    }
}
