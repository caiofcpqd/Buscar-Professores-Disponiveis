using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.DTOs.Responses;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.Validators;
using cursoMetta_Domain.Models;

namespace cursoMetta_Application.UseCases
{
    public class CriarDisponibilidade
    {
        private readonly IDisponibilidadeRepository _disponibilidade;
        private readonly CriarDisponibilidadeValidator _validator;

        public CriarDisponibilidade(IDisponibilidadeRepository disponibilidade, CriarDisponibilidadeValidator validator)
        {
            _disponibilidade = disponibilidade;
            _validator = validator;
        }

        public async Task<CriarDisponibilidadeResponse> Criar(CriarDisponibilidadeRequest request)
        {
            var msg = new List<string>();
            var response = new CriarDisponibilidadeResponse();

            var mensagens = _validator.Validar(request);
            if (mensagens.Count > 0)
            {
                response.Mensagens = mensagens;
                response.Erro = true;

                return response;
            }

            var listaDisponibilidade = await _disponibilidade.ListaDeDisponibilidade();
            var disponibilidadeExistente = listaDisponibilidade.Any(x =>
                x.ProfessorId == request.ProfessorId &&
                string.Equals(x.Dia, request.Dia, StringComparison.OrdinalIgnoreCase) &&
                request.Inicio < x.Fim && request.Fim > x.Inicio);
            if (disponibilidadeExistente)
            {
                msg.Add("já existe uma disponibilidade nesse dia e horario desse professor.");

                response.Mensagens = msg;
                response.Erro = true;

                return response;
            }

            var novaDisponibilidade = new Disponibilidade
            {
                Dia = request.Dia,
                ProfessorId = request.ProfessorId,
                Inicio = request.Inicio,
                Fim = request.Fim
            };

            await _disponibilidade.Criar(novaDisponibilidade);
            response.Erro = false;

            return response;
        }
    }
}
