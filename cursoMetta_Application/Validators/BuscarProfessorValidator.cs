using cursoMetta_Application.DTOs.Requests;

namespace cursoMetta_Application.Validators
{
    public class BuscarProfessorValidator
    {
        private static readonly List<string> diasValidos = new() { "segunda", "terca", "quarta", "quinta", "sexta" };

        public List<string> Validar(BuscarProfessorRequest request)
        {
            var msg = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Idioma))
                msg.Add("o idioma está vazio.");

            if (request.Dias == null || request.Dias.Count == 0)
                msg.Add("nenhum dia foi informado.");

            if (request.Dias.Any(x => !diasValidos.Contains(x, StringComparer.OrdinalIgnoreCase)))
                msg.Add("algum dia é inválido.");

            if (request.Inicio.Second != 0 || request.Inicio.Millisecond != 0 || request.Fim.Second != 0 || request.Fim.Millisecond != 0)
                msg.Add("algum horário está fora do formato HH:mm.");

            if (request.Fim <= request.Inicio)
                msg.Add("o fim da faixa é igual ou anterior ao início.");

            if (request.DuracaoMinutos is not (30 or 60 or 90))
                msg.Add("a duração é diferente de 30, 60 ou 90 minutos.");

            if (request.Inicio.AddMinutes(request.DuracaoMinutos) <= request.Inicio)
                msg.Add("a aula não pode ultrapassar a meia-noite.");

            return msg;
        }
    }
}
