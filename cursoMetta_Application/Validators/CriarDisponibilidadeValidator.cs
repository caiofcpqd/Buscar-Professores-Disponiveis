using cursoMetta_Application.DTOs.Requests;

namespace cursoMetta_Application.Validators
{
    public class CriarDisponibilidadeValidator
    {
        private static readonly List<string> diasValidos = new() { "segunda", "terca", "quarta", "quinta", "sexta" };

        public List<string> Validar(CriarDisponibilidadeRequest request)
        {
            var msg = new List<string>();

            if (!diasValidos.Contains(request.Dia, StringComparer.OrdinalIgnoreCase))
                msg.Add("algum dia é inválido.");

            if (request.Inicio.Second != 0 || request.Inicio.Millisecond != 0 || request.Fim.Second != 0 || request.Fim.Millisecond != 0)
                msg.Add("algum horário está fora do formato HH:mm.");

            if (request.Fim <= request.Inicio)
                msg.Add("o fim da faixa é igual ou anterior ao início.");

            return msg;
        }
    }
}
