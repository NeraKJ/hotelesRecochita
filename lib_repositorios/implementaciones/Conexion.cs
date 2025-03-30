using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string StringConnection { get; set; }

        public Conexion(string stringConnection)
        {
            this.StringConnection = stringConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.StringConnection);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estadias>()
                .HasOne(e => e._Factura)    // Una estadía tiene una factura
                .WithOne(f => f._Estadia)   // Una factura pertenece a una estadía
                .HasForeignKey<Facturas>(f => f.Id_Estadia) // Factura depende de Estadía
                .OnDelete(DeleteBehavior.Cascade); // Elimina la factura si se borra la estadía

            modelBuilder.Entity<Estadias>()
                .HasOne(e => e._Reserva)
                .WithOne(r => r._Estadia)
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
