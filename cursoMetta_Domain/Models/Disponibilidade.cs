using System.ComponentModel.DataAnnotations;

namespace cursoMetta_Domain.Models
{
    public class Disponibilidade
    {
        [Key]
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public string Dia { get; set; } = string.Empty;
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
    }
}
