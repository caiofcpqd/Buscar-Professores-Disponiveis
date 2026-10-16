namespace cursoMetta_Application.DTOs.Responses
{
    public class CriarDisponibilidadeResponse
    {
        public List<string> Mensagens { get; set; } = new();
        public bool Erro { get; set; } = false;
    }
}
