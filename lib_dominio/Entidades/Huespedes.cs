using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Huespedes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Huesped { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        [NotMapped]
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

        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Historial_Reserva { get; set; }


        public int Id_Hotel { get; set; }
        [ForeignKey("Id_Hotel")]
        public Hoteles? Hotel { get; set; }

        public List<Reservas>? Reservas { get; set; }



    }

}
