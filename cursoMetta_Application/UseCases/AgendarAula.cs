using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.DTOs.Responses;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.Validators;
using cursoMetta_Domain.Models;

namespace cursoMetta_Application.UseCases
{
    public class AgendarAula
    {
        private readonly IAgendaRepository _agenda;
        private readonly AgendarAulaValidator _validator;

        public AgendarAula(IAgendaRepository agenda, AgendarAulaValidator validator)
        {
            _agenda = agenda;
            _validator = validator;
        }

        public async Task<AgendarAulaResponse> Agendar(AgendarAulaRequest request)
        {
            var msg = new List<string>();
            var response = new AgendarAulaResponse();

            var mensagens = _validator.Validar(request);
            if (mensagens.Count > 0)
            {
                response.Mensagens = mensagens; 
                response.Erro = true;

                return response;
            }

            var listaAgendas = await _agenda.ListaDeAgendas();
            var agendaExistente = listaAgendas.Any(x => 
                x.ProfessorId == request.ProfessorId &&
                string.Equals(x.Dia, request.Dia, StringComparison.OrdinalIgnoreCase) &&
                request.Inicio < x.Inicio.AddMinutes(x.Duracao) && request.Inicio.AddMinutes(request.DuracaoMinutos) > x.Inicio);
            if (agendaExistente)
            {
                msg.Add("já existe uma agenda nesse dia e horario desse professor.");

                response.Mensagens = msg;
                response.Erro = true;

                return response;
            }

            var novoAgendamento = new AulaMarcada
            {
                ProfessorId = request.ProfessorId,
                Dia = request.Dia,
                Inicio = request.Inicio,
                Duracao = request.DuracaoMinutos
            };

            await _agenda.Agendar(novoAgendamento);
            response.Erro = false;

            return response;
        }
    }
}
