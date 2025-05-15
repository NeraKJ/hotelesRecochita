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
        [ForeignKey("Id_Sede")]
        public Sedes? Sedes { get; set; }
        public string? Piscina { get; set; }
        public string? Restaurante { get; set; }
        public string? Limpieza { get; set; }
        public string? Mantenimiento { get; set; }
        public string? Gimnasio { get; set; }
        public string? Jacuzzi { get; set; }

        public Facturas? Factura { get; set; }
        //public List<Facturas>? Facturas { get; set; }
        public List<Auditoria>? Auditoria { get; set; }
        public List<Sedes_ServiciosExtras>? Sedes_ServiciosExtras { get; set; }



    }
}