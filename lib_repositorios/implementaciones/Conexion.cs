using lib_dominio.Entidades;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estadias>()
                .HasOne(e => e.Facturas)    // Una estadía tiene una factura
                .WithOne(f => f.Estadias)   // Una factura pertenece a una estadía
                .HasForeignKey<Facturas>(f => f.Id_Estadia) // Factura depende de Estadía
                .OnDelete(DeleteBehavior.Cascade); // Elimina la factura si se borra la estadía

            modelBuilder.Entity<Estadias>()
                .HasOne(e => e.Reservas)
                .WithOne(r => r.Estadias)
                .HasForeignKey<Estadias>(e => e.Id_Reserva)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Hoteles> Hoteles { get; set; } = null!;
        public DbSet<Huespedes> Huespedes { get; set; } = null!;
        public DbSet<Empleados> Empleados { get; set; } = null!;
        public DbSet<Sedes> Sedes { get; set; } = null!;
        public DbSet<Empleados_ServiciosExtras> Empleados_ServiciosExtras { get; set; } = null!;
        public DbSet<Estadias> Estadias { get; set; } = null!;
        public DbSet<Facturas> Facturas { get; set; } = null!;
        public DbSet<Habitaciones> Habitaciones { get; set; } = null!;
        public DbSet<Reservas> Reservas { get; set; } = null!;
        public DbSet<Reservas_Habitaciones> Reservas_Habitaciones { get; set; } = null!;
        public DbSet<Sedes_ServiciosExtras> Sedes_ServiciosExtras { get; set; } = null!;
        public DbSet<ServiciosExtras> ServiciosExtras { get; set; } = null!;
    }
}
