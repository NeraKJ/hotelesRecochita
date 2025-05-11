using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IReservas_HabitacionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Reservas_Habitaciones> PorCodigo(Reservas_Habitaciones? entidad);
        List<Reservas_Habitaciones> Listar();
        Reservas_Habitaciones? Guardar(Reservas_Habitaciones? entidad);
        Reservas_Habitaciones? Modificar(Reservas_Habitaciones? entidad);
        Reservas_Habitaciones? Borrar(Reservas_Habitaciones? entidad);
    }
}