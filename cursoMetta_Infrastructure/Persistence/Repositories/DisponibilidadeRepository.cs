using cursoMetta_Application.Interfaces;
using cursoMetta_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoMetta_Infrastructure.Persistence.Repositories
{
    public class DisponibilidadeRepository : IDisponibilidadeRepository
    {
        private readonly WebDbContext _webDbContext;

        public DisponibilidadeRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public async Task<Disponibilidade> Criar(Disponibilidade horario)
        {
            await _webDbContext.disponibilidade.AddAsync(horario);
            await _webDbContext.SaveChangesAsync();
            return horario;
        }

        public async Task<List<Disponibilidade>> ListaDeDisponibilidade()
        {
            var listaDisponibilidade = await _webDbContext.disponibilidade.ToListAsync();
            return listaDisponibilidade;
        }
    }
}
