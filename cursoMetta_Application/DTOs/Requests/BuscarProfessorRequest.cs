namespace cursoMetta_Application.DTOs.Requests
{
    public class BuscarProfessorRequest
    {
        public string Idioma { get; set; } = string.Empty;
        public List<string> Dias { get; set; } = new();
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
        public int DuracaoMinutos { get; set; }
    }
}
