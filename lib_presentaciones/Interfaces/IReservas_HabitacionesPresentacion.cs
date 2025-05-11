using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IReservas_HabitacionesPresentacion
    {
        Task<List<Reservas_Habitaciones>> Listar();
        Task<List<Reservas_Habitaciones>> PorId(Reservas_Habitaciones? entidad);
        Task<Reservas_Habitaciones?> Guardar(Reservas_Habitaciones? entidad);
        Task<Reservas_Habitaciones?> Modificar(Reservas_Habitaciones? entidad);
        Task<Reservas_Habitaciones?> Borrar(Reservas_Habitaciones? entidad);
    }
}