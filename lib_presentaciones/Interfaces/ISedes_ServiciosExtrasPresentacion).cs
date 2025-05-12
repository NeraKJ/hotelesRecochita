using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ISedes_ServiciosExtrasPresentacion
    {
        Task<List<Sedes_ServiciosExtras>> Listar();
        Task<List<Sedes_ServiciosExtras>> PorId(Sedes_ServiciosExtras? entidad);
        Task<Sedes_ServiciosExtras?> Guardar(Sedes_ServiciosExtras? entidad);
        Task<Sedes_ServiciosExtras?> Modificar(Sedes_ServiciosExtras? entidad);
        Task<Sedes_ServiciosExtras?> Borrar(Sedes_ServiciosExtras? entidad);
    }
}