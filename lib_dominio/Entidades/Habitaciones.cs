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
    public class Habitaciones
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Habitacion { get; set; }
        public int Id_Sede { get; set; }
        public string? Numero_Habitacion { get; set; }

        public decimal? Precio_Habitacion { get; set; }
        public int Camas { get; set; }
        public string? Estado { get; set; }
        public string? Capacidad { get; set; }
        public int Id_Hotel { get; set; }
        [ForeignKey("Id_Hotel")]
        public Hoteles? Hoteles { get; set; }
        [ForeignKey("Id_Sede")]
        public Sedes? Sedes { get; set; }

       

    }
}
