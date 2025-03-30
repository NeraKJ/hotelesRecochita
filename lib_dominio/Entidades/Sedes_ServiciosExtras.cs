using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Sedes_ServiciosExtras
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id_Sede_ServicioExtra { get; set; }
        public int Id_Sede { get; set; }
        public int Id_ServicioExtra { get; set; }

        public double Descuento_Sede { get; set; }


        public Sedes? _Sede { get; set; }
        public ServiciosExtras? _ServicioExtra { get; set; }


    }
}