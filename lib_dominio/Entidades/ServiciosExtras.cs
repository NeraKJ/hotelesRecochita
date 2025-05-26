using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class ServiciosExtras
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
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

        
        
      



    }
}