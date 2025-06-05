




using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {

        [Key] public int Id { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public int Rol { get; set; }
        [ForeignKey("Rol")] public Roles? Roles { get; set; }



    }
}
