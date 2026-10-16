using System.ComponentModel.DataAnnotations;

namespace cursoMetta_Domain.Models
{
    public class AulaMarcada
    {
        [Key]
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public string Dia { get; set; } = string.Empty;
        public int Duracao { get; set; }
        public TimeOnly Inicio { get; set; }
    }
}
