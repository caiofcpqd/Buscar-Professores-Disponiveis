using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.UseCases;
using cursoMetta_Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cursoMetta_Api.Controllers
{
    [Route("api/agendamento")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly AgendarAula _agendarAula;

        public AgendaController(AgendarAula agendarAula)
        {
            _agendarAula = agendarAula;
        }

        [HttpPost("agendar")]
        public async Task<IActionResult> Agendar(AgendarAulaRequest request)
        {
            var agendamento = await _agendarAula.Agendar(request);

            if (agendamento.Erro)
            {
                foreach (var agenda in agendamento.Mensagens)
                {
                    return BadRequest(agenda);
                }
            }

            return Ok("agendamento realizado com sucesso");
        }
    }
}
