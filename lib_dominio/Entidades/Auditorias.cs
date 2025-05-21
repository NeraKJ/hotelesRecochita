using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{



    public class Auditorias
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Auditoria { get; set; }
        public string? Lugar { get; set; }
        public string? Daticos { get; set; }
        public string? Usuario { get; set; }
        public string? Accion { get; set; }
        public DateTime Fecha { get; set; }


    }
}
