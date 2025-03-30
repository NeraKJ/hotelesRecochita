using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Habitaciones
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id_Habitacion { get; set; }
        public int Id_Sede { get; set; }
        public string? Numero_Habitacion { get; set; }
        public int Camas { get; set; }
        public string? Estado { get; set; }
        public int Capacidad { get; set; }
        public int Id_Hotel { get; set; }
        public Hoteles? _Hotel { get; set; }
        public Sedes? _Sede { get; set; }


        public List<Reservas_Habitaciones>? Reservas_Habitaciones { get; set; }




    }
}
