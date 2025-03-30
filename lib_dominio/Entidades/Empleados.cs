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
            public int Id_Empleados { get; set; }

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
                    int edad = hoy.Year - FechaNaci.Year;
                    if (FechaNaci.Date > hoy.AddYears(-edad))
                    {
                        edad--;
                    }
                    return edad;
                }
            }

            public DateTime FechaNaci { get; set; }
            public string? Sexo { get; set; }
            public DateTime Fecha_Contratacion { get; set; }
            public string? Rol { get; set; }

            public Hoteles? _Hotel { get; set; }
            public Sedes? _Sede { get; set; }
            public List<Empleados_ServiciosExtras>? Empleados_ServiciosExtras { get; set; }
        }
    }
