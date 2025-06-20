﻿using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

     


        public DbSet<Hoteles> Hoteles { get; set; } = null!;
        public DbSet<Huespedes> Huespedes { get; set; } = null!;
        public DbSet<Empleados> Empleados { get; set; } = null!;
        public DbSet<Sedes> Sedes { get; set; } = null!;
        public DbSet<Estadias> Estadias { get; set; } = null!;
        public DbSet<Facturas> Facturas { get; set; } = null!;
        public DbSet<Habitaciones> Habitaciones { get; set; } = null!;
        public DbSet<Reservas> Reservas { get; set; } = null!;
        public DbSet<Reservas_Habitaciones> Reservas_Habitaciones { get; set; } = null!;
        public DbSet<Sedes_ServiciosExtras> Sedes_ServiciosExtras { get; set; } = null!;
        public DbSet<ServiciosExtras> ServiciosExtras { get; set; } = null!;
        public DbSet<Empleados_ServiciosExtras> Empleados_ServiciosExtras { get; set; } = null!;
        public DbSet<Auditorias> Auditorias { get; set;} = null!;
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }




    }
}
