namespace cursoMetta_Application.DTOs.Responses
{
    public class CadastrarProfessorResponse
    {
        public List<string> Mensagens { get; set; } = new();
        public bool Erro { get; set; } = false;
    }
}
