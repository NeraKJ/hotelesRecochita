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
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id_Empleado_ServiciosExtra { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_ServicioExtra { get; set; }

        public double Pago_servicio { get; set; }
        public double Precio_servicio { get; set; }


        public Empleados? _Empleado { get; set; }
        public ServiciosExtras? _ServicioExtra { get; set; }


    }
}
