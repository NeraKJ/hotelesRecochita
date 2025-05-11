using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IServiciosExtrasPresentacion
    {
        Task<List<ServiciosExtras>> Listar();
        Task<List<ServiciosExtras>> PorId(ServiciosExtras? entidad);
        Task<ServiciosExtras?> Guardar(ServiciosExtras? entidad);
        Task<ServiciosExtras?> Modificar(ServiciosExtras? entidad);
        Task<ServiciosExtras?> Borrar(ServiciosExtras? entidad);
    }
}