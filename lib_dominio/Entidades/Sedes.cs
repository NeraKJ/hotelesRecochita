using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Sedes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Sede { get; set; }
        public int Id_Hotel { get; set; }

        [ForeignKey("Id_Hotel")]
        public Hoteles? Hotel { get; set; }

        public string? Direccion { get; set; }
        public string? Locacion { get; set; }

        public List<Reservas>? Reservas { get; set; }
        public List<Sedes_ServiciosExtras>? SedesServiciosExtras { get; set; }
        public List<Empleados>? Empleados { get; set; }
        public List<Habitaciones>? Habitaciones { get; set; }
    }
}
