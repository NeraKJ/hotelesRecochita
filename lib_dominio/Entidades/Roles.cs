
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Roles
    {
        [Key] public int Id_Rol { get; set; }
        public string? Nombre { get; set; }

    }
}
