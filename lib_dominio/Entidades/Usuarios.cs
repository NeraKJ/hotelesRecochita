
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {
        [Key] public int Id_Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Contraseña { get; set; }
        public int Rol { get; set; }
        [ForeignKey("Id_Rol")] public Roles? Roles { get; set; }

    }
}
