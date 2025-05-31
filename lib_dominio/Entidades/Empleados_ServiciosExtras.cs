using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{

    public class Empleados_ServiciosExtras
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Empleado_ServicioExtra { get; set; }
        public int Id_E{ get; set; }
        public int Id_ServicioExtra { get; set; }

        public decimal Pago_Servicio { get; set; }
        public decimal Precio_Servicio { get; set; }

        [ForeignKey("Id_E")]
        public Empleados? Empleados { get; set; }

        [ForeignKey("Id_ServicioExtra")]
        public ServiciosExtras? ServiciosExtras { get; set; }


    }
}
