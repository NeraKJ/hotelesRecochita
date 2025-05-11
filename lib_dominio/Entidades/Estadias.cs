using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Estadias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Estadia { get; set; }

        public DateTime Fecha_Entrada { get; set; }

        public DateTime Fecha_Salida { get; set; }
        public int Id_Reserva { get; set; }
        [ForeignKey("Id_Reserva")]
        public Reservas? Reservas { get; set; }

        public Facturas? Facturas { get; set; }
    }
}
