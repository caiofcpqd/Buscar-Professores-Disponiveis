using System.ComponentModel.DataAnnotations;

namespace cursoMetta_Domain.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<string> Idiomas { get; set; } = new();
        public bool AgendaBloqueada { get; set; } = false;
        public List<Disponibilidade> HorariosDisponiveis { get; set; } = new();
        public List<AulaMarcada> AulasMarcadas { get; set; } = new();
    }
}
