using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cursoMetta_Api.Controllers
{
    [Route("api/professores")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly CadastrarProfessor _cadastrarProfessor;
        private readonly BuscarProfessorDisponivel _buscarProfessor;

        public ProfessorController(CadastrarProfessor cadastrarProfessor, BuscarProfessorDisponivel buscarProfessor)
        {
            _cadastrarProfessor = cadastrarProfessor;
            _buscarProfessor = buscarProfessor;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarProfessorRequest request)
        {
            var professor = await _cadastrarProfessor.Cadastrar(request);

            if (professor.Erro)
            {
                foreach (var prof in professor.Mensagens)
                {
                    return BadRequest(prof);
                }
            }

            return Ok("professor cadastrado com sucesso");
        }

        [HttpPost("busca")]
        public async Task<IActionResult> Busca(BuscarProfessorRequest request)
        {
            var professor = await _buscarProfessor.Buscar(request);

            return Ok(professor);
        }
    }
}
