using cursoMetta_Application.Interfaces;
using cursoMetta_Application.UseCases;
using cursoMetta_Application.Validators;
using cursoMetta_Infrastructure.Persistence;
using cursoMetta_Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cursoMetta_Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConn")));

            services.AddScoped<CadastrarProfessor>();
            services.AddScoped<BuscarProfessorDisponivel>();
            services.AddScoped<AgendarAula>();
            services.AddScoped<CriarDisponibilidade>();

            services.AddScoped<AgendarAulaValidator>();
            services.AddScoped<CadastrarProfessorValidator>();
            services.AddScoped<BuscarProfessorValidator>();
            services.AddScoped<CriarDisponibilidadeValidator>();

            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IAgendaRepository, AgendamentoRepository>();
            services.AddScoped<IDisponibilidadeRepository, DisponibilidadeRepository>();

            return services;
        }
    }
}
