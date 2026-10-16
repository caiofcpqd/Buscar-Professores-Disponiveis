using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.DTOs.Responses;
using cursoMetta_Application.Interfaces;
using cursoMetta_Application.Validators;
using cursoMetta_Domain.Models;

namespace cursoMetta_Application.UseCases
{
    public class CadastrarProfessor
    {
        private readonly IProfessorRepository _prof;
        private readonly CadastrarProfessorValidator _validator;

        public CadastrarProfessor(IProfessorRepository prof, CadastrarProfessorValidator validator)
        {
            _prof = prof;
            _validator = validator;
        }

        public async Task<CadastrarProfessorResponse> Cadastrar(CadastrarProfessorRequest request)
        {
            var msg = new List<string>();
            var response = new CadastrarProfessorResponse();

            var mensagens = _validator.Validar(request);
            if (mensagens.Count > 0)
            {
                response.Mensagens = mensagens;
                response.Erro = true;

                return response;
            }

            var ckProfessor = await _prof.ProfessorExiste(request.Nome);
            if (ckProfessor)
            {
                msg.Add("professor já cadastrado.");

                response.Mensagens = msg;
                response.Erro = true;

                return response;
            }

            var novoProfessor = new Professor
            {
                Nome = request.Nome,
                Idiomas = request.Idiomas
            };

            await _prof.CadastrarProfessor(novoProfessor);
            response.Erro = false;

            return response;
        }
    }
}
