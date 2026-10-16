namespace cursoMetta_Application.DTOs.Requests
{
    public class CadastrarProfessorRequest
    {
        public string Nome { get; set; } = string.Empty;
        public List<string> Idiomas { get; set; }
    }
}
