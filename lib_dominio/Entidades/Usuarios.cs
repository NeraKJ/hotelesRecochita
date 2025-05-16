using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
  
    
        public class Usuarios
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Usuario { get; set; }
            public string Nombre { get; set; }
            public int Contraseña { get; set; }
           

            public Usuarios() { }

            public Usuarios(string usuario, string accion)
            {
                Nombre = Nombre;
                Contraseña = Contraseña;
            }

            public override string ToString()
            {
                return $"[{Nombre}]  Accedio con exito :)";
            }
        }
    

}
