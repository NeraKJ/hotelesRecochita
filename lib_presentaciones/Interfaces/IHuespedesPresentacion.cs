using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IHuespedesPresentacion
    {
        Task<List<Huespedes>> Listar();
        Task<List<Huespedes>> PorId(Huespedes? entidad);
        Task<Huespedes?> Guardar(Huespedes? entidad);
        Task<Huespedes?> Modificar(Huespedes? entidad);
        Task<Huespedes?> Borrar(Huespedes? entidad);
    }
}