using cursoMetta_Application.DTOs.Requests;

namespace cursoMetta_Application.Validators
{
    public class CadastrarProfessorValidator
    {
        public List<string> Validar(CadastrarProfessorRequest request)
        {
            var msg = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Nome))
                msg.Add("insira um nome válido.");

            if (request.Idiomas == null || request.Idiomas.Count == 0)
                msg.Add("insira um idioma válido.");

            return msg;
        }
    }
}
