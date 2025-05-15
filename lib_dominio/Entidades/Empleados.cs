using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Empleados
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincremental
        public int Id_Empleado { get; set; }

        public int Id_Hotel { get; set; }
        public int Id_Sede { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        [NotMapped] // No se guarda en la base de datos
        public int Edad
        {
            get
            {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - Fecha_Naci.Year;
                if (Fecha_Naci.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }
                return edad;
            }
        }

        public DateTime Fecha_Naci { get; set; }
        public string? Sexo { get; set; }
        public DateTime Fecha_Contratacion { get; set; }
        public string? Rol { get; set; }
        [ForeignKey("Id_Hotel")]
        public Hoteles? Hoteles { get; set; }
        [ForeignKey("Id_Sede")]
        public Sedes? Sedes { get; set; }
        public List<Auditoria>? Auditoria { get; set; }
    }
}
