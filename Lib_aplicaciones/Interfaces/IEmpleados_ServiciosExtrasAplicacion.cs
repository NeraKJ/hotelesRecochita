using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEmpleados_ServiciosExtrasAplicacion
    {
        void Configurar(string StringConexion);
        List<Empleados_ServiciosExtras> PorId(Empleados_ServiciosExtras? entidad);
        List<Empleados_ServiciosExtras> Listar();
        Empleados_ServiciosExtras? Guardar(Empleados_ServiciosExtras? entidad);
        Empleados_ServiciosExtras? Modificar(Empleados_ServiciosExtras? entidad);
        Empleados_ServiciosExtras? Borrar(Empleados_ServiciosExtras? entidad);
    }
}