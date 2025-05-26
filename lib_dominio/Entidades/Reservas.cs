using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class Reservas
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Reserva { get; set; }
        public int Id_Sede { get; set; }
        [ForeignKey("Id_Sede")]
        public Sedes? Sedes { get; set; }

        public int Id_Huesped { get; set; }
        [ForeignKey("Id_Huesped")]
        public Huespedes? Huespedes { get; set; }

        public string? Estado_Actual { get; set; }
        public string? Numero_Huespedes { get; set; }
        public DateTime Fecha_Reserva { get; set; }
        public Estadias? Estadias { get; set; }


        public Facturas? Factura { get; set; }
        [JsonIgnore]
        public List<Reservas_Habitaciones>? ReservasHabitaciones { get; set; }
    }
}
