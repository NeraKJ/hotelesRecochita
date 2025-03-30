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
        public int Total { get; set; }
        public string? MetodoPago { get; set; }
        public string? CargosExtra { get; set; }
        public string? Reseña { get; set; }

        public int Id_Estadia { get; set; }
        public Estadias? _Estadia { get; set; }

        public List<ServiciosExtras>? ServiciosExtras { get; set; }
    }
}
