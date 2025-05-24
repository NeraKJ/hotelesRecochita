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
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Sedes_ServiciosExtras { get; set; }
        public int Id_Sede { get; set; }
        public int Id_ServicioExtra { get; set; }

        public decimal Descuento_Sede { get; set; }

        [ForeignKey("Id_Sede")]
        public Sedes? Sedes { get; set; }
        [ForeignKey("Id_ServicioExtra")]
        public ServiciosExtras? ServicioExtra { get; set; }


    }
}