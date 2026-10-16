namespace cursoMetta_Application.DTOs.Responses
{
    public class AgendarAulaResponse
    {
        public List<string> Mensagens { get; set; } = new();
        public bool Erro { get; set; }
    }
}
