using cursoMetta_Application.DTOs.Requests;
using cursoMetta_Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cursoMetta_Api.Controllers
{
    [Route("api/disponibilidade")]
    [ApiController]
    public class DisponibilidadeController : ControllerBase
    {
        private readonly CriarDisponibilidade _criarDisponibilidade;

        public DisponibilidadeController(CriarDisponibilidade criarDisponibilidade)
        {
            _criarDisponibilidade = criarDisponibilidade;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(CriarDisponibilidadeRequest request)
        {
            var disponibilidade = await _criarDisponibilidade.Criar(request);

            if (disponibilidade.Erro)
            {
                foreach (var disp in disponibilidade.Mensagens)
                {
                    return BadRequest(disp);
                }
            }

            return Ok("disponibilidade cadastrada com sucesso");
        }
    }
}
