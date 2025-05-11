using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IDistribuidoresAplicacion
    {
        void Configurar(string StringConexion);
        List<Distribuidores> PorCodigo(Distribuidores? entidad);
        List<Distribuidores> Listar();
        Distribuidores? Guardar(Distribuidores? entidad);
        Distribuidores? Modificar(Distribuidores? entidad);
        Distribuidores? Borrar(Distribuidores? entidad);
    }
}