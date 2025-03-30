using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reservas
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id_Reserva { get; set; }
        public int Id_Sede { get; set; }
        public Sedes? _Sede { get; set; }

        public int Huesped { get; set; }
        public Huespedes? _Huesped { get; set; }

        public string? Estado { get; set; }
        public DateTime FechaReserva { get; set; }
        public int NumeroHuespedes { get; set; }

        public Estadias? _Estadia { get; set; }
        public Facturas? _Factura { get; set; }

        public List<Reservas_Habitaciones>? ReservasHabitaciones { get; set; }
    }
}
