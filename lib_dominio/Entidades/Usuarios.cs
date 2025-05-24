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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa

        public int Id_Usuario { get; set; }
            public string? Nombre { get; set; }
            public int Contraseña { get; set; }
           

        }
    

}
