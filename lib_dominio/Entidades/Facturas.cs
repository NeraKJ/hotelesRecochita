using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Facturas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Factura { get; set; }
        public int Id_ServicioExtra { get; set; }

        [ForeignKey("Id_ServicioExtra")]
        public ServiciosExtras? ServiciosExtras { get; set; }
        public int Id_Reserva { get; set; }
        public int Id_Estadia { get; set; }
        public decimal Total { get; set; }
        public string? Metodo_Pago { get; set; }
        public string? Cargos_Extra { get; set; }
        public string? Reseña { get; set; }


        [ForeignKey("Id_Estadia")]
        public Estadias? Estadias { get; set; }
        [ForeignKey("Id_Reserva")]
        public Reservas? Reserva { get; set; }

    }
}
