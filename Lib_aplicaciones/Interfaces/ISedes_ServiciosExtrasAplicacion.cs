using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ISedes_ServiciosExtrasAplicacion
    {
        void Configurar(string StringConexion);
        List<Sedes_ServiciosExtras> PorId(Sedes_ServiciosExtras? entidad);
        List<Sedes_ServiciosExtras> Listar();
        Sedes_ServiciosExtras? Guardar(Sedes_ServiciosExtras? entidad);
        Sedes_ServiciosExtras? Modificar(Sedes_ServiciosExtras? entidad);
        Sedes_ServiciosExtras? Borrar(Sedes_ServiciosExtras? entidad);
    }
}