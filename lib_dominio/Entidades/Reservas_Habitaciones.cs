using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reservas_Habitaciones
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Reserva_Habitacion { get; set; }
        public int Id_Reserva { get; set; }
        public int Id_Habitacion { get; set; }

        public double Precio_Dia { get; set; }

        public Reservas? _Reserva { get; set; }
        public Habitaciones? _Habitacion { get; set; }


    }
}
