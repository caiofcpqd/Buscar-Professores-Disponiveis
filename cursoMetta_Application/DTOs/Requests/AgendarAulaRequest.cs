namespace cursoMetta_Application.DTOs.Requests
{
    public class AgendarAulaRequest
    {
        public int ProfessorId { get; set; }
        public string Dia { get; set; } = string.Empty;
        public TimeOnly Inicio { get; set; }
        public int DuracaoMinutos { get; set; }
    }
}
