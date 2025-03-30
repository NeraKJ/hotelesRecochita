using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class ServiciosExtras
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id_ServicioExtra { get; set; }

        public int Id_Sede { get; set; }
        public bool Piscina { get; set; }
        public bool Restaurante { get; set; }
        public bool Limpieza { get; set; }
        public bool Mantenimiento { get; set; }
        public bool Gimnasio { get; set; }
        public bool Jacuzzi { get; set; }
        public int Id_Factura { get; set; }
        public Facturas? _Factura { get; set; }

        public List<Empleados_ServiciosExtras>? Empleados_ServiciosExtras { get; set; }
        public List<Sedes_ServiciosExtras>? Sedes_ServiciosExtras { get; set; }



    }
}