using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEstadiasPresentacion
    {
        Task<List<Estadias>> Listar();
        Task<List<Estadias>> PorId(Estadias? entidad);
        Task<Estadias?> Guardar(Estadias? entidad);
        Task<Estadias?> Modificar(Estadias? entidad);
        Task<Estadias?> Borrar(Estadias? entidad);
    }
}