
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Auditorias
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Entidad { get; set; }
        public string? Operacion { get; set; }
        public string? Datos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
