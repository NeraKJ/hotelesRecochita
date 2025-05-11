using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IHuespedesAplicacion
    {
        void Configurar(string StringConexion);
        List<Huespedes> PorCodigo(Huespedes? entidad);
        List<Huespedes> Listar();
        Huespedes? Guardar(Huespedes? entidad);
        Huespedes? Modificar(Huespedes? entidad);
        Huespedes? Borrar(Huespedes? entidad);
    }
}