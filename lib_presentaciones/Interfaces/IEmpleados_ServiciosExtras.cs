using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEmpleados_ServiciosExtrasPresentacion
    {
        Task<List<Empleados_ServiciosExtras>> Listar();
        Task<List<Empleados_ServiciosExtras>> PorId(Empleados_ServiciosExtras? entidad);
        Task<Empleados_ServiciosExtras?> Guardar(Empleados_ServiciosExtras? entidad);
        Task<Empleados_ServiciosExtras?> Modificar(Empleados_ServiciosExtras? entidad);
        Task<Empleados_ServiciosExtras?> Borrar(Empleados_ServiciosExtras? entidad);
    }
}