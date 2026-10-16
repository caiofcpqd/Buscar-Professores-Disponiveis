namespace cursoMetta_Application.DTOs.Responses
{
    public class ProfessorDisponivelResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int AulasNaSemana { get; set; }
        public List<string> Horarios { get; set; } = new();
    }
}
