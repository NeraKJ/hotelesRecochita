﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static lib_dominio.Entidades.Hoteles;
using Newtonsoft.Json;



namespace lib_dominio.Entidades
{

    public class Hoteles
    {
        [Key]  // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Para que sea autoincrementa
        public int Id_Hotel { get; set; }
        public string? Nombre { get; set; }
        public string? Dueños { get; set; }

      

    }






}