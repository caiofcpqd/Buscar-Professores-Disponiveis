using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.DTOs.Responses;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.Validators;

namespace cursoMetta_Application.UseCases
{
    public class BuscarProfessorDisponivel
    {
        private readonly IProfessorRepository _prof;
        private readonly BuscarProfessorValidator _validator;

        public BuscarProfessorDisponivel(IProfessorRepository prof, BuscarProfessorValidator validator)
        {
            _prof = prof;
            _validator = validator;
        }

        public async Task<BuscarProfessorResponse> Buscar(BuscarProfessorRequest request)
        {
            var response = new BuscarProfessorResponse();
            
            var mensagens = _validator.Validar(request);
            if (mensagens.Count > 0)
            {
                response.Mensagens = mensagens;
                return response;
            }

            var professores = await _prof.BuscarProfessorDisponivel();
            foreach (var professor in professores)
            {
                // checar se o idioma solicitado corresponde ao do professor
                if (!professor.Idiomas.Any(i => string.Equals(i, request.Idioma, StringComparison.OrdinalIgnoreCase)))
                {
                    response.NaoElegiveis.Add(new ProfessorNaoElegivelResponse
                    {
                        Id = professor.Id,
                        Nome = professor.Nome,
                        Motivo = "Não ensina o idioma."
                    });

                    continue;
                }

                // verificar se a agenda se encontra bloqueada
                if (professor.AgendaBloqueada)
                {
                    response.NaoElegiveis.Add(new ProfessorNaoElegivelResponse
                    {
                        Id = professor.Id,
                        Nome = professor.Nome,
                        Motivo = "Agenda bloqueada."
                    });

                    continue;
                }

                var horariosPossiveis = new List<string>();

                // converter o horario para minutos
                var inicioFaixa = request.Inicio.Hour * 60 + request.Inicio.Minute; // 1170 minutos
                var fimFaixa = request.Fim.Hour * 60 + request.Fim.Minute; // 1240 minutos
                var primeiroInicio = ((inicioFaixa + 29) / 30) * 30;

                for (int minuto = primeiroInicio; minuto + request.DuracaoMinutos <= fimFaixa; minuto += 30)
                {
                    var inicioAula = TimeOnly.MinValue.AddMinutes(minuto);
                    var fimAula = inicioAula.AddMinutes(request.DuracaoMinutos);

                    bool funcionaTodosOsDias = request.Dias.All(dia =>
                    {
                        bool disponivel = professor.HorariosDisponiveis.Any(h =>
                            string.Equals(h.Dia, dia, StringComparison.OrdinalIgnoreCase)
                            && h.Inicio <= inicioAula
                            && h.Fim >= fimAula);

                        if (!disponivel)
                            return false;

                        // verificar conflitos com aulas ja marcadas
                        bool conflito = professor.AulasMarcadas.Any(a =>
                        {
                            if (!string.Equals(a.Dia, dia, StringComparison.OrdinalIgnoreCase))
                                return false;

                            var fimMarcada = a.Inicio.AddMinutes(a.Duracao);

                            return inicioAula < fimMarcada && fimAula > a.Inicio;
                        });

                        return !conflito;
                    });

                    if (funcionaTodosOsDias)
                        horariosPossiveis.Add(inicioAula.ToString("HH:mm"));
                }

                // caso não aja horario comum
                if (horariosPossiveis.Count == 0)
                {
                    response.NaoElegiveis.Add(new ProfessorNaoElegivelResponse
                    {
                        Id = professor.Id,
                        Nome = professor.Nome,
                        Motivo = "Sem horário em comum nos dias pedidos."
                    });

                    continue;
                }

                // caso não aja qualquer problema, registrar o professor elegível
                response.Professores.Add(new ProfessorDisponivelResponse
                {
                    Id = professor.Id,
                    Nome = professor.Nome,
                    AulasNaSemana = professor.AulasMarcadas.Count,
                    Horarios = horariosPossiveis
                });
            }

            // ordernar por quantidade de aulas e nome
            response.Professores = response.Professores.OrderBy(p => p.AulasNaSemana).ThenBy(p => p.Nome).ToList();

            return response;
        }
    }
}
