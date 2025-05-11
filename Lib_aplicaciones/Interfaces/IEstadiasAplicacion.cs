using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEsatadiasAplicacion
    {
        void Configurar(string StringConexion);
        List<Estadias> PorCodigo(Estadias? entidad);
        List<Estadias> Listar();
        Estadias? Guardar(Estadias? entidad);
        Estadias? Modificar(Estadias? entidad);
        Estadias? Borrar(Estadias? entidad);
    }
   
}