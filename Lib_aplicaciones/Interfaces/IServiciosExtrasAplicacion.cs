using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IServiciosExtrasAplicacion
    {
        void Configurar(string StringConexion);
        List<ServiciosExtras> PorId(ServiciosExtras? entidad);
        List<ServiciosExtras> Listar();
        ServiciosExtras? Guardar(ServiciosExtras? entidad);
        ServiciosExtras? Modificar(ServiciosExtras? entidad);
        ServiciosExtras? Borrar(ServiciosExtras? entidad);
    }
}