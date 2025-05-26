using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }
        DbSet<Hoteles>? Hoteles { get; set; }
        DbSet<Huespedes>? Huespedes { get; set; }
        DbSet<Sedes>? Sedes { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Habitaciones>? Habitaciones { get; set; }
        DbSet<Empleados_ServiciosExtras> Empleados_ServiciosExtras { get; set; }
        DbSet<Estadias> Estadias { get; set; }
        DbSet<Facturas> Facturas { get; set; }
        DbSet<Reservas> Reservas { get; set; }
        DbSet<Reservas_Habitaciones> Reservas_Habitaciones { get; set; }
        DbSet<Sedes_ServiciosExtras> Sedes_ServiciosExtras { get; set; }
        DbSet<ServiciosExtras> ServiciosExtras { get; set; }
        DbSet<Auditorias> Auditorias { get; set; }
        


        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
