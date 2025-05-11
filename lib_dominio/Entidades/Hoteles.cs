
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static lib_dominio.Entidades.Hoteles;

namespace lib_dominio.Entidades
{

    public class Hoteles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Hotel { get; set; }
        public string? Nombre { get; set; }
        public string? Dueños { get; set; }



        public List<Huespedes>? Huespedes { get; set; }
        public List<Habitaciones>? Habitacion { get; set; }
        public List<Sedes>? Sedes { get; set; }
        public List<Empleados>? Empleados { get; set; }


    }






}