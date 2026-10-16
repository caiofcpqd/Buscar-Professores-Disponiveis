using cursoMetta_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoMetta_Infrastructure.Persistence
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {

        }

        public DbSet<Professor> professores { get; set; }
        public DbSet<Disponibilidade> disponibilidade { get; set; }
        public DbSet<AulaMarcada> aulas { get; set; }
    }
}
