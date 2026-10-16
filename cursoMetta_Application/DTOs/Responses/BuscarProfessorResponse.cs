namespace cursoMetta_Application.DTOs.Responses
{
    public class BuscarProfessorResponse
    {
        public List<ProfessorDisponivelResponse> Professores { get; set; } = new();
        public List<ProfessorNaoElegivelResponse> NaoElegiveis { get; set; } = new();
        public List<string> Mensagens { get; set; } = new();
    }
}
