using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IDistribuidoresPresentacion
    {
        Task<List<Distribuidores>> Listar();
        Task<List<Distribuidores>> PorCodigo(Distribuidores? entidad);
        Task<Distribuidores?> Guardar(Distribuidores? entidad);
        Task<Distribuidores?> Modificar(Distribuidores? entidad);
        Task<Distribuidores?> Borrar(Distribuidores? entidad);
    }
}