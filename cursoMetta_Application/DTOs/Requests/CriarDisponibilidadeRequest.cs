namespace cursoMetta_Application.DTOs.Requests
{
    public class CriarDisponibilidadeRequest
    {
        public int ProfessorId { get; set; }
        public string Dia { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
    }
}
